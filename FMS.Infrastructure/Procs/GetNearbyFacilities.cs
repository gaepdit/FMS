﻿using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FMS.Infrastructure.Procs
{
    public static class StoredProcedures
    {
        public static void CreateStoredProcedures(this FmsDbContext context)
        {
            context.Database.ExecuteSqlRaw(GetNearbyFacilities);
        }

        private const string GetNearbyFacilities = @"
CREATE PROCEDURE [dbo].[getNearbyFacilities] 
    @active bit = null,
    @Lat float = NULL,
    @Lng float = NULL,
    @radius float = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM (
        SELECT a.Id, a.Active, a.FacilityNumber, a.Name, a.Address, a.City,
               a.State, a.PostalCode, a.Latitude, a.Longitude,
               B.Status                   as FacilityStatus,
               C.Name                     as FacilityType,
               a.FileId,
               d.FileLabel                as FileLabel,
               CONCAT(a.Address, ', ', a.City, ', ', a.State, ' ', a.PostalCode)
                                          as FullAddress,
               ROUND((ACOS(SIN(@Lat * PI() / 180) * SIN(a.Latitude * PI() / 180) +
                     COS(@Lat * PI() / 180) * COS(a.Latitude * PI() / 180) *
                     COS((@Lng - a.Longitude) * PI() / 180)) *
                180 / PI()) * 60 * 1.1515, 2) AS Distance
        FROM [dbo].[Facilities] as A
            INNER JOIN [dbo].[FacilityStatuses] as B
            ON A.FacilityStatusId = B.Id
            INNER JOIN [dbo].[FacilityTypes] as C
            ON A.FacilityTypeId = C.Id
            INNER JOIN [dbo].[Files] as D
            ON A.FileId = D.Id
        where (@active = 1 AND a.Active = @active)
           OR @active = 0
    ) AS T
    WHERE T.distance <= @radius
    ORDER BY distance ASC;

END;
";
    }
}