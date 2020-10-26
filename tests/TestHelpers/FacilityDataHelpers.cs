using FMS.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestHelpers
{
    public static partial class DataHelpers
    {
        public static FacilityDetailDto GetFacilityDetail(Guid id)
        {
            var facility = Facilities.Find(e => e.Id == id);
            facility.File = GetFile(facility.FileId);
            facility.County = GetCounty(facility.CountyId);
            facility.FacilityStatus = GetFacilityStatus(facility.FacilityStatusId);
            facility.FacilityType = GetFacilityType(facility.FacilityTypeId);
            facility.BudgetCode = GetBudgetCode(facility.BudgetCodeId);
            facility.OrganizationalUnit = GetOrganizationalUnit(facility.OrganizationalUnitId);
            facility.ComplianceOfficer = GetComplianceOfficer(facility.ComplianceOfficerId);

            var facilityDetail = new FacilityDetailDto(facility);
            facilityDetail.Cabinets.AddRange(GetCabinetsForFile(facility.FileId));
            facilityDetail.RetentionRecords.AddRange(GetRetentionRecordDetailsForFacility(id));

            return facilityDetail;
        }

        public static FacilitySummaryDto GetFacilitySummary(Guid id)
        {
            var facility = Facilities.Find(e => e.Id == id);
            facility.File = GetFile(facility.FileId);

            var facilitySummary = new FacilitySummaryDto(facility);
            facilitySummary.Cabinets.AddRange(GetCabinetsForFile(facility.FileId));
            facilitySummary.RetentionRecords.AddRange(GetRetentionRecordSummaryForFacility(id));

            return facilitySummary;
        }

        public static List<RetentionRecordSummaryDto> GetRetentionRecordSummaryForFacility(Guid id) =>
            RetentionRecords.Where(e => e.FacilityId == id)
                .Select(e => new RetentionRecordSummaryDto(e)).ToList();

        public static List<RetentionRecordDetailDto> GetRetentionRecordDetailsForFacility(Guid id) =>
            RetentionRecords.Where(e => e.FacilityId == id)
                .Select(e => new RetentionRecordDetailDto(e)).ToList();
    }
}