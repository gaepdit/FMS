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
    @Latitude  float = NULL,
    @Longitude float = NULL,
    @Radius    float = NULL
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
               B.Status                             as FacilityStatus,
               C.Name                               as FacilityType,
               a.FileId,
               d.FileLabel                          as FileLabel,
               CONCAT(a.Address, ', ', a.City, ', ', a.State, ' ', a.PostalCode)
                                                    as FullAddress,
               ROUND((ACOS(SIN(@Latitude * PI() / 180) * SIN(a.Latitude * PI() / 180) +
                           COS(@Latitude * PI() / 180) * COS(a.Latitude * PI() / 180) *
                           COS((@Longitude - a.Longitude) * PI() / 180)) *
                      180 / PI()) * 60 * 1.1515, 2) AS Distance
        FROM [dbo].[Facilities] as A
            INNER JOIN [dbo].[FacilityStatuses] as B
            ON A.FacilityStatusId = B.Id
            INNER JOIN [dbo].[FacilityTypes] as C
            ON A.FacilityTypeId = C.Id
            INNER JOIN [dbo].[Files] as D
            ON A.FileId = D.Id
        where a.Active = 1
           or @Active = 0
    ) AS T
    WHERE T.distance <= @Radius
    ORDER BY distance;

END;
";
    }
}