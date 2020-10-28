using System;
using System.Collections.Generic;
using System.Linq;
using FMS.Domain.Data;
using FMS.Domain.Entities;
using FMS.Infrastructure.SeedData;
using FMS.Infrastructure.SeedData.TestData;

namespace TestHelpers
{
    public static partial class DataHelpers
    {
        // Data collections
        public static readonly List<Facility> Facilities = TestData.GetFacilities();
        public static readonly List<FacilityStatus> FacilityStatuses = ProdData.GetFacilityStatuses().ToList();
        public static readonly List<FacilityType> FacilityTypes = ProdData.GetFacilityTypes().ToList();
        public static readonly List<BudgetCode> BudgetCodes = TestData.GetBudgetCodes();
        public static readonly List<OrganizationalUnit> OrganizationalUnits = ProdData.GetOrganizationalUnits().ToList();
        public static readonly List<ComplianceOfficer> ComplianceOfficers = TestData.GetComplianceOfficers();
        public static readonly List<File> Files = TestData.GetFiles();
        public static readonly List<Cabinet> Cabinets = TestData.GetCabinets();
        public static readonly List<CabinetFile> CabinetFiles = TestData.GetCabinetFiles();
        public static readonly List<RetentionRecord> RetentionRecords = TestData.GetRetentionRecords();

        // Item retrieval
        public static County GetCounty(int id) =>
            Data.Counties.Find(e => e.Id == id);
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