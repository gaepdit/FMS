using System;
using System.Collections.Generic;
using System.Linq;
using FMS.Domain.Data;
using FMS.Domain.Dto;
using FMS.Domain.Entities;

namespace TestHelpers
{
    public static partial class SimpleRepositoryData
    {
        public static List<CabinetSummaryDto> GetCabinetSummaries(bool includeInactive)
        {
            var cabinets = Cabinets
                .Where(e => e.Active || includeInactive)
                .OrderBy(e => e.FirstFileLabel)
                .ThenBy(e => e.Name)
                .Select(e => new CabinetSummaryDto(e)).ToList();

            // loop through all the cabinets except the last one and set last file label
            for (var i = 0; i < cabinets.Count - 1; i++)
            {
                cabinets[i].LastFileLabel = cabinets[i + 1].FirstFileLabel;
            }

            return cabinets;
        }

        public static CabinetSummaryDto GetCabinetSummary(Guid id) =>
            GetCabinetSummaries(true).Find(e => e.Id == id);

        public static FacilityDetailDto GetFacilityDetail(Guid id)
        {
            var facility = Facilities.Find(e => e.Id == id);
            facility.File = GetFile(facility.FileId);
            facility.County = GetCounty(facility.CountyId);
            facility.FacilityStatus = GetFacilityStatus(facility.FacilityStatusId);
            facility.FacilityType = GetFacilityType(facility.FacilityTypeId);
            facility.BudgetCode = GetBudgetCode(facility.BudgetCodeId);
            facility.OrganizationalUnit = GetOrganizationalUnit(facility.OrganizationalUnitId);

            var facilityDetail = new FacilityDetailDto(facility);
            facilityDetail.RetentionRecords.AddRange(GetRetentionRecordsForFacility(id));
            return facilityDetail;
        }

        // Item retrieval
        public static County GetCounty(int id) => Data.Counties.Find(e => e.Id == id);
        private static File GetFile(Guid? id) => Files.Find(e => e.Id == id);
        private static FacilityStatus GetFacilityStatus(Guid? id) => FacilityStatuses.Find(e => e.Id == id);
        private static FacilityType GetFacilityType(Guid? id) => FacilityTypes.Find(e => e.Id == id);
        private static BudgetCode GetBudgetCode(Guid? id) => BudgetCodes.Find(e => e.Id == id);
        private static OrganizationalUnit GetOrganizationalUnit(Guid? id) => OrganizationalUnits.Find(e => e.Id == id);

        private static IEnumerable<RetentionRecordDetailDto> GetRetentionRecordsForFacility(Guid id) =>
            RetentionRecords.Where(e => e.FacilityId == id)
                .Select(e => new RetentionRecordDetailDto(e)).ToList();
    }
}