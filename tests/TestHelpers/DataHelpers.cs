using FMS.Domain.Data;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Entities.Users;
using FMS.Infrastructure.SeedData;
using System;
using System.Collections.Generic;

namespace TestHelpers
{
    public static class DataHelpers
    {
        public static Facility[] Facilities = DevSeedData.GetFacilities();
        public static List<County> Counties = Data.Counties;
        public static FacilityStatus[] FacilityStatuses = DevSeedData.GetFacilityStatuses();
        public static FacilityType[] FacilityTypes = DevSeedData.GetFacilityTypes();
        public static BudgetCode[] BudgetCodes = DevSeedData.GetBudgetCodes();
        public static OrganizationalUnit[] OrganizationalUnits = DevSeedData.GetOrganizationalUnits();
        public static EnvironmentalInterest[] EnvironmentalInterests = DevSeedData.GetEnvironmentalInterests();
        public static ComplianceOfficer[] ComplianceOfficers = DevSeedData.GetComplianceOfficers();
        public static File[] Files = DevSeedData.GetFiles();

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

        public static County GetCounty(int id) => Counties.Find(e => e.Id == id);
        public static FacilityStatus GetFacilityStatus(Guid id) => Array.Find(FacilityStatuses, e => e.Id == id);
        public static FacilityType GetFacilityType(Guid id) => Array.Find(FacilityTypes, e => e.Id == id);
        public static BudgetCode GetBudgetCode(Guid id) => Array.Find(BudgetCodes, e => e.Id == id);
        public static OrganizationalUnit GetOrganizationalUnit(Guid id) => Array.Find(OrganizationalUnits, e => e.Id == id);
        public static EnvironmentalInterest GetEnvironmentalInterest(Guid id) => Array.Find(EnvironmentalInterests, e => e.Id == id);
        public static ComplianceOfficer GetComplianceOfficer(Guid id) => Array.Find(ComplianceOfficers, e => e.Id == id);
        public static File GetFile(Guid id) => Array.Find(Files, e => e.Id == id);

        public static List<ApplicationUser> GetApplicationUsers()
        {
            return new List<ApplicationUser> {
                new ApplicationUser
                {
                    Id = new Guid("06bca04c-19bb-4c41-b554-e57a56a2c6b7"),
                    Email = "example.one@example.com",
                    GivenName = "Sample",
                    FamilyName = "User"
                },
                new ApplicationUser
                {
                    Id = new Guid("43a21a8a-1fc6-4348-9004-e1aec42392b7"),
                    Email = "example.two@example.com",
                    GivenName = "Another",
                    FamilyName = "Sample"
                }
            };
        }
    }
}
