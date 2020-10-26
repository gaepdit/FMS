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
        public static List<Facility> Facilities = DevSeedData.GetFacilities();
        public static List<County> Counties = Data.Counties;
        public static List<FacilityStatus> FacilityStatuses = DevSeedData.GetFacilityStatuses();
        public static List<FacilityType> FacilityTypes = DevSeedData.GetFacilityTypes();
        public static List<BudgetCode> BudgetCodes = DevSeedData.GetBudgetCodes();
        public static List<OrganizationalUnit> OrganizationalUnits = DevSeedData.GetOrganizationalUnits();
        public static List<ComplianceOfficer> ComplianceOfficers = DevSeedData.GetComplianceOfficers();
        public static List<File> Files = DevSeedData.GetFiles();
        public static List<Cabinet> Cabinets = DevSeedData.GetCabinets();
        public static List<CabinetFile> CabinetFiles = DevSeedData.GetCabinetFiles();
        public static List<RetentionRecord> RetentionRecords = DevSeedData.GetRetentionRecords();

        // Item retrieval
        public static County GetCounty(int id) =>
            Counties.Find(e => e.Id == id);
        public static FacilityStatus GetFacilityStatus(Guid? id) =>
            !id.HasValue ? null : FacilityStatuses.Find(e => e.Id == id);
        public static FacilityType GetFacilityType(Guid? id) =>
            !id.HasValue ? null : FacilityTypes.Find(e => e.Id == id);
        public static BudgetCode GetBudgetCode(Guid? id) =>
            !id.HasValue ? null : BudgetCodes.Find(e => e.Id == id);
        public static OrganizationalUnit GetOrganizationalUnit(Guid? id) =>
            !id.HasValue ? null : OrganizationalUnits.Find(e => e.Id == id);
        public static ComplianceOfficer GetComplianceOfficer(Guid? id) =>
            !id.HasValue ? null : ComplianceOfficers.Find(e => e.Id == id);
    }
}
