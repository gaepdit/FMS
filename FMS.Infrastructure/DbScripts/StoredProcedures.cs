using System.Threading.Tasks;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FMS.Infrastructure.DbScripts
{
    public static class StoredProcedures
    {
        public static Task CreateStoredProceduresAsync(this FmsDbContext context)
        {
            // Execute both stored procedure creation scripts sequentially
            return CreateStoredProceduresInternalAsync(context);
        }

        private static async Task CreateStoredProceduresInternalAsync(FmsDbContext context)
        {
            await context.Database.ExecuteSqlRawAsync(CreateSpGetNearbyFacilities).ConfigureAwait(false);
            await context.Database.ExecuteSqlRawAsync(CreateSpPAFReportProcedure).ConfigureAwait(false);
        }

        public const string CreateSpGetNearbyFacilities = @"
CREATE OR
ALTER PROCEDURE [dbo].[getNearbyFacilities]
    @Active    bit = 1,
    @Latitude  decimal(8, 6) = NULL,
    @Longitude decimal(9, 6) = NULL,
    @Radius    decimal(3, 2) = NULL,
    @FacilityTypeId UNIQUEIDENTIFIER = null 
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM (
        SELECT a.Id,
               a.Active,
               a.FacilityNumber,
               a.Name,
               a.Address,
               a.City,
               a.State,
               a.PostalCode,
               a.Latitude,
               a.Longitude,
               b.Status                                  as FacilityStatus,
               c.Name                                    as FacilityType,
               a.FileId,
               d.FileLabel                               as FileLabel,
               CONCAT(a.Address, ', ', a.City, ', ', a.State, ' ', a.PostalCode)
                                                         as FullAddress,
               IIF(@Latitude = a.Latitude and @Longitude = a.Longitude, 0,
                   ROUND((ACOS(SIN(@Latitude * PI() / 180) * SIN(a.Latitude * PI() / 180) +
                               COS(@Latitude * PI() / 180) * COS(a.Latitude * PI() / 180) *
                               COS((@Longitude - a.Longitude) * PI() / 180)) *
                          180 / PI()) * 60 * 1.1515, 2)) AS Distance
        FROM [dbo].[Facilities] as a
            INNER JOIN [dbo].[FacilityStatuses] as b
            ON a.FacilityStatusId = b.Id
            INNER JOIN [dbo].[FacilityTypes] as c
            ON a.FacilityTypeId = c.Id
            LEFT JOIN [dbo].[Files] as d
            ON a.FileId = d.Id
        where (a.Active = 1 AND ( @FacilityTypeId IS NULL OR a.FacilityTypeId = @FacilityTypeId ))
           or (a.Active = @Active AND ( @FacilityTypeId IS NULL OR a.FacilityTypeId = @FacilityTypeId ))
    ) AS T
    WHERE T.distance <= @Radius
    ORDER BY distance;

END;
";

        public const string CreateSpPAFReportProcedure = @"
    CREATE OR ALTER    PROCEDURE [dbo].[PAF_Report]
AS
BEGIN
    SET NOCOUNT ON;

    IF OBJECT_ID('tempdb..#PAF_TEMP') IS NOT NULL
        DROP TABLE #PAF_TEMP;

    SELECT
        F.FacilityNumber AS [HSI ID],
        F.Name AS [Site Name],
        CAST(E.StartDate AS DATE) AS [PAF Issue Date],
        ISNULL(E.EventAmount, 0) AS [PAF Amount],
        COALESCE(CO.GivenName + ' ' + CO.FamilyName, '') AS [Project Officer],
        COALESCE(EC.Name, '') AS [Contractor],

        /* RAW */
        CASE
            WHEN W.CompletionDate IS NULL
                 OR W.CompletionDate > E.StartDate
                THEN CAST(W.StartDate AS DATE)
            ELSE NULL
        END AS [RAW Received],
        CASE
            WHEN W.CompletionDate IS NULL
                 OR W.CompletionDate > E.StartDate
                THEN CAST(W.DueDate AS DATE)
            ELSE NULL
        END AS [RAW Due],
        CASE
            WHEN W.CompletionDate IS NULL
                 OR W.CompletionDate > E.StartDate
                THEN CAST(W.CompletionDate AS DATE)
            ELSE NULL
        END AS [RAW Approved],

        /* RAR */
        CASE
            WHEN R.CompletionDate IS NULL
                 OR R.CompletionDate > E.StartDate
                THEN CAST(R.StartDate AS DATE)
            ELSE NULL
        END AS [RAR Received],
        CASE
            WHEN R.CompletionDate IS NULL
                 OR R.CompletionDate > E.StartDate
                THEN CAST(R.DueDate AS DATE)
            ELSE NULL
        END AS [RAR Due],
        CASE
            WHEN R.CompletionDate IS NULL
                 OR R.CompletionDate > E.StartDate
                THEN CAST(R.CompletionDate AS DATE)
            ELSE NULL
        END AS [RAR Approved],

        CAST(E.DueDate AS DATE) AS [Project Complete Due],
        CAST(E.CompletionDate AS DATE) AS [Project Complete Actual],
        E.Comment AS [Project Comments]
    INTO #PAF_TEMP
    FROM FMS.dbo.[Events] E
        JOIN dbo.EventTypes ET
            ON E.EventTypeId = ET.Id
           AND ET.Name = 'HWTF Master Project'
        JOIN dbo.Facilities F
            ON E.FacilityId = F.Id
        LEFT JOIN dbo.ComplianceOfficers CO
            ON E.ComplianceOfficerId = CO.Id
        LEFT JOIN dbo.EventContractors EC
            ON E.EventContractorId = EC.Id

        /* RAW */
        LEFT JOIN (
            SELECT FacilityId, StartDate, DueDate, CompletionDate
            FROM (
                SELECT
                    E.FacilityId,
                    E.StartDate,
                    E.DueDate,
                    E.CompletionDate,
                    ROW_NUMBER() OVER (
                        PARTITION BY E.FacilityId
                        ORDER BY
                            CASE WHEN E.CompletionDate IS NULL THEN 0 ELSE 1 END,
                            E.CompletionDate DESC
                    ) AS RN
                FROM FMS.dbo.[Events] E
                    JOIN dbo.EventTypes ET ON E.EventTypeId = ET.Id
                WHERE ET.Name = 'Removal Work Plan'
                  AND E.Active = 1
            ) X
            WHERE RN = 1
        ) W ON W.FacilityId = E.FacilityId

        /* RAR */
        LEFT JOIN (
            SELECT FacilityId, StartDate, DueDate, CompletionDate
            FROM (
                SELECT
                    E.FacilityId,
                    E.StartDate,
                    E.DueDate,
                    E.CompletionDate,
                    ROW_NUMBER() OVER (
                        PARTITION BY E.FacilityId
                        ORDER BY
                            CASE WHEN E.CompletionDate IS NULL THEN 0 ELSE 1 END,
                            E.CompletionDate DESC
                    ) AS RN
                FROM FMS.dbo.[Events] E
                    JOIN dbo.EventTypes ET ON E.EventTypeId = ET.Id
                WHERE ET.Name = 'Response Activities Report'
                  AND E.Active = 1
            ) X
            WHERE RN = 1
        ) R ON R.FacilityId = E.FacilityId;

    /* FINAL RESULT */
    SELECT
        [HSI ID] AS [HSIId],
        [Site Name] AS [SiteName],
        COALESCE(CONVERT(VARCHAR(10), [PAF Issue Date], 23), '') AS [PAFIssueDate],
        COALESCE(CONVERT(VARCHAR(20), [PAF Amount]), '') AS [PAFAmount],
        COALESCE([Project Officer], '') AS [ProjectOfficer],
        COALESCE([Contractor], '') AS [Contractor],
        COALESCE(CONVERT(VARCHAR(10), [RAW Received], 23), '') AS [RAWReceived],
        COALESCE(CONVERT(VARCHAR(10), [RAW Due], 23), '') AS [RAWDue],
        COALESCE(CONVERT(VARCHAR(10), [RAW Approved], 23), '') AS [RAWApproved],
        COALESCE(CONVERT(VARCHAR(10), [RAR Received], 23), '') AS [RARReceived],
        COALESCE(CONVERT(VARCHAR(10), [RAR Due], 23), '') AS [RARDue],
        COALESCE(CONVERT(VARCHAR(10), [RAR Approved], 23), '') AS [RARApproved],
        COALESCE(CONVERT(VARCHAR(10), [Project Complete Due], 23), '') AS [ProjectCompleteDue],
        COALESCE(CONVERT(VARCHAR(10), [Project Complete Actual], 23), '') AS [ProjectCompleteActual],
        COALESCE([Project Comments], '') AS [ProjectComments]
    FROM #PAF_TEMP
    ORDER BY [PAF Issue Date];

END;
";
    }
}