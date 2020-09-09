using FMS.Domain.Data;
using FMS.Domain.Entities;
using FMS.Infrastructure.SeedData;
using System;
using System.Collections.Generic;

namespace TestHelpers
{
    public static partial class DataHelpers
    {
        // Data collections
        public static Facility[] Facilities = DevSeedData.GetFacilities();
        public static List<County> Counties = Data.Counties;
        public static FacilityStatus[] FacilityStatuses = DevSeedData.GetFacilityStatuses();
        public static FacilityType[] FacilityTypes = DevSeedData.GetFacilityTypes();
        public static BudgetCode[] BudgetCodes = DevSeedData.GetBudgetCodes();
        public static OrganizationalUnit[] OrganizationalUnits = DevSeedData.GetOrganizationalUnits();
        public static EnvironmentalInterest[] EnvironmentalInterests = DevSeedData.GetEnvironmentalInterests();
        public static ComplianceOfficer[] ComplianceOfficers = DevSeedData.GetComplianceOfficers();
        public static File[] Files = DevSeedData.GetFiles();
        public static List<Cabinet> Cabinets = DevSeedData.GetCabinets();

        // Item retrieval
        public static County GetCounty(int id) => Counties.Find(e => e.Id == id);
        public static FacilityStatus GetFacilityStatus(Guid id) => Array.Find(FacilityStatuses, e => e.Id == id);
        public static FacilityType GetFacilityType(Guid id) => Array.Find(FacilityTypes, e => e.Id == id);
        public static BudgetCode GetBudgetCode(Guid id) => Array.Find(BudgetCodes, e => e.Id == id);
        public static OrganizationalUnit GetOrganizationalUnit(Guid id) => Array.Find(OrganizationalUnits, e => e.Id == id);
        public static EnvironmentalInterest GetEnvironmentalInterest(Guid id) => Array.Find(EnvironmentalInterests, e => e.Id == id);
        public static ComplianceOfficer GetComplianceOfficer(Guid id) => Array.Find(ComplianceOfficers, e => e.Id == id);
        public static File GetFile(Guid id) => Array.Find(Files, e => e.Id == id);
        public static Cabinet GetCabinet(Guid id) => Cabinets.Find(e => e.Id == id);
    }
}
