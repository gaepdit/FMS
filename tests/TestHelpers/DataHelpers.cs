using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Infrastructure.SeedData;
using System;
using System.IO;

namespace TestHelpers
{
    public static class DataHelpers
    {
        public static Facility[] Facilities = DevSeedData.GetFacilities();
        public static County[] Counties = ProdSeedData.GetCounties();
        public static FacilityStatus[] FacilityStatuses = DevSeedData.GetFacilityStatuses();
        public static FacilityType[] FacilityTypes = DevSeedData.GetFacilityTypes();
        public static BudgetCode[] BudgetCodes = DevSeedData.GetBudgetCodes();
        public static OrganizationalUnit[] OrganizationalUnits = DevSeedData.GetOrganizationalUnits();
        public static EnvironmentalInterest[] EnvironmentalInterests = DevSeedData.GetEnvironmentalInterests();
        public static ComplianceOfficer[] ComplianceOfficers = DevSeedData.GetComplianceOfficers();

        public static FacilityDetailDto GetFacilityDetail(Guid id)
        {
            var facility = Array.Find(Facilities, e => e.Id == id);

            return new FacilityDetailDto(facility)
            {
                County = GetCounty(facility.CountyId),
                FacilityStatus = GetFacilityStatus(facility.FacilityStatusId),
                FacilityType = GetFacilityType(facility.FacilityTypeId),
                BudgetCode = GetBudgetCode(facility.BudgetCodeId),
                OrganizationalUnit = GetOrganizationalUnit(facility.OrganizationalUnitId),
                EnvironmentalInterest = GetEnvironmentalInterest(facility.EnvironmentalInterestId),
                ComplianceOfficer = GetComplianceOfficer(facility.ComplianceOfficerId)
            };
        }

        public static County GetCounty(int id) => Array.Find(Counties, e => e.Id == id);
        public static FacilityStatus GetFacilityStatus(Guid id) => Array.Find(FacilityStatuses, e => e.Id == id);
        public static FacilityType GetFacilityType(Guid id) => Array.Find(FacilityTypes, e => e.Id == id);
        public static BudgetCode GetBudgetCode(Guid id) => Array.Find(BudgetCodes, e => e.Id == id);
        public static OrganizationalUnit GetOrganizationalUnit(Guid id) => Array.Find(OrganizationalUnits, e => e.Id == id);
        public static EnvironmentalInterest GetEnvironmentalInterest(Guid id) => Array.Find(EnvironmentalInterests, e => e.Id == id);
        public static ComplianceOfficer GetComplianceOfficer(Guid id) => Array.Find(ComplianceOfficers, e => e.Id == id);
    }
}
