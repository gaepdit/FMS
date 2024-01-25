using System.Threading.Tasks;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FMS.Infrastructure.DbScripts
{
    public static class StoredProcedures
    {
        public static Task CreateStoredProceduresAsync(this FmsDbContext context) =>
            context.Database.ExecuteSqlRawAsync(CreateSpGetNearbyFacilities);

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
            INNER JOIN [dbo].[Files] as d
            ON a.FileId = d.Id
        where (a.Active = 1 AND ( @FacilityTypeId IS NULL OR a.FacilityTypeId = @FacilityTypeId ))
           or @Active = 0
    ) AS T
    WHERE T.distance <= @Radius
    ORDER BY distance;

END;
";
    }
}