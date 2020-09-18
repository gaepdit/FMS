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
            facility.EnvironmentalInterest = GetEnvironmentalInterest(facility.EnvironmentalInterestId);
            facility.ComplianceOfficer = GetComplianceOfficer(facility.ComplianceOfficerId);

            return new FacilityDetailDto(facility)
            {
                Cabinets = GetCabinetsForFile(facility.FileId),
                RetentionRecords = GetRetentionRecordDetailsForFacility(id)
            };
        }

        public static FacilitySummaryDto GetFacilitySummary(Guid id)
        {
            var facility = Facilities.Find(e => e.Id == id);
            facility.File = GetFile(facility.FileId);

            return new FacilitySummaryDto(facility)
            {
                Cabinets = GetCabinetsForFile(facility.FileId),
                RetentionRecords = GetRetentionRecordSummaryForFacility(id)
            };
        }

        public static List<RetentionRecordSummaryDto> GetRetentionRecordSummaryForFacility(Guid id) =>
            RetentionRecords.Where(e => e.FacilityId == id)
            .Select(e => new RetentionRecordSummaryDto(e)).ToList();

        public static List<RetentionRecordDetailDto> GetRetentionRecordDetailsForFacility(Guid id) =>
            RetentionRecords.Where(e => e.FacilityId == id)
            .Select(e => new RetentionRecordDetailDto(e)).ToList();

    }
}
