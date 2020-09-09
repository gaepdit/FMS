using FMS.Domain.Dto;
using System;

namespace TestHelpers
{
    public static partial class DataHelpers
    {
        public static FacilityDetailDto GetFacilityDetail(Guid id)
        {
            var facility = Array.Find(Facilities, e => e.Id == id);
            facility.File = GetFile(facility.FileId);
            facility.County = GetCounty(facility.CountyId);
            facility.FacilityStatus = GetFacilityStatus(facility.FacilityStatusId);
            facility.FacilityType = GetFacilityType(facility.FacilityTypeId);
            facility.BudgetCode = GetBudgetCode(facility.BudgetCodeId);
            facility.OrganizationalUnit = GetOrganizationalUnit(facility.OrganizationalUnitId);
            facility.EnvironmentalInterest = GetEnvironmentalInterest(facility.EnvironmentalInterestId);
            facility.ComplianceOfficer = GetComplianceOfficer(facility.ComplianceOfficerId);

            return new FacilityDetailDto(facility);
        }
    }
}
