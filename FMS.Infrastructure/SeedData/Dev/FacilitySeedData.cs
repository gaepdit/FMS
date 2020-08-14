using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static FMS.Domain.Entities.Facility;

namespace FMS.Infrastructure.SeedData
{
    public static partial class DevSeedData
    {
        public static Facility[] GetFacilities()
        {
            return new List<Facility>
            {
                new Facility
                {
                    Id = new Guid("3FF8B38C-B2A0-4A32-B703-BEAB9138B7F0"),
                    Active = true,
                    FacilityNumber = "BRF1211825",
                    File = new File{ },   //FileLabel = "243-0001"
                    EnvironmentalInterest = new EnvironmentalInterest{ },   //RCRA
                    FacilityType = new FacilityType{ },   //gen
                    OrganizationalUnit = new OrganizationalUnit{ },   //Org Unit
                    BudgetCode = new BudgetCode{ },   //HWRCRA
                    Name = "3 BRANCHES SUBDIVISION (CAPITAL DESIGN CONSTRUCT.)",
                    ComplianceOfficer = new ComplianceOfficer{ },   //00360513
                    FacilityStatus = new FacilityStatus{ },   //NON-RCRA
                    Location = "Description of Location",
                    Address = "102 THREE BRANCHES DR.",
                    City = "WOODSTOCK",
                    State = "GA",
                    PostalCode = "30188",
                    Latitude = "34.114309",
                    Longitude = "-84.470057",
                    County =  new County{ },   //Id = 243, Name = "Cherokee"
                    RetentionRecords = new List<RetentionRecord>{ }   //0
                },
                new Facility
                {
                    Id = new Guid("AB44F9C7-C2EC-47BC-8886-60D72B5BD5EB"),
                    Active = true,
                    FacilityNumber = "BRF3191858",
                    File = new File{ },   //243-0084
                    EnvironmentalInterest = new EnvironmentalInterest{ },   //BROWN
                    FacilityType = new FacilityType{ },   //BROWN
                    OrganizationalUnit = new OrganizationalUnit{ },   //Org Unit
                    BudgetCode = new BudgetCode{ },   //HWBRVRP
                    Name = "TOONIGH VILLAGE BUILDING C",
                    ComplianceOfficer = new ComplianceOfficer{ },   //01049142
                    FacilityStatus = new FacilityStatus{ },   //ACTIVE
                    Location = "Some location description here",
                    Address = "5335 HOLLY SPRINGS PARKWAY",
                    City = "WOODSTOCK",
                    State = "GA",
                    PostalCode = "30188",
                    Latitude = "34.1416484",
                    Longitude = "-84.5047357",
                    County = new County{ },  // CHEROKEE
                    RetentionRecords = new List<RetentionRecord>{ }   //1
                },
                new Facility
                {
                    Id = new Guid("50AEC751-D2FA-42D1-BE02-3EDF721787CA"),
                    Active = true,
                    FacilityNumber = "BRF3471841",
                    File = { },    //243-0085
                    EnvironmentalInterest = { },   //BROWN
                    FacilityType = { },   //BROWN
                    OrganizationalUnit = { },   //Org Unit
                    BudgetCode = { },   //HWBRVRP
                    Name = "WOODSTOCK SHOPPING CENTER",
                    ComplianceOfficer = { },   //01035970
                    FacilityStatus = { },   //ACTIVE
                    Location = "Description of Location in Woodstock",
                    Address = "9980, 10010 AND 10020 HIGHWAY 92 AND 1906 PROFESSIONAL WAY",
                    City = "WOODSTOCK",
                    State = "GA",
                    PostalCode = "30188",
                    Latitude = "34.084926",
                    Longitude = "-84.521794",
                    County = { },  // CHEROKEE
                    RetentionRecords = { }   //1
                },
                new Facility
                {
                    Id = new Guid("7B20DE98-4726-4789-9AEA-2D995FF6839A"),
                    Active = true,
                    FacilityNumber = "BRF341436",
                    File = { },   //243-0068
                    EnvironmentalInterest = { },   //BROWN
                    FacilityType = { },   // BROWN
                    OrganizationalUnit = { }, //Org Unit
                    BudgetCode = { },   //HWBRVRP
                    Name = "106 ARNOLD MILL ROAD",
                    ComplianceOfficer = { },   //00767488
                    FacilityStatus = { },   //ACTIVE
                    Location = "Somewhere in Woodstock",
                    Address = "106 ARNOLD MILL RD.",
                    City = "WOODSTOCK",
                    State = "GA",
                    PostalCode = "30188",
                    Latitude = "34.100926",
                    Longitude = "-84.516605",
                    County = { },  // CHEROKEE
                    RetentionRecords = { }   //1
                },
                new Facility
                {
                    Id = new Guid("3A7457EC-E4A4-47D2-B47C-35078C3F5BF7"),
                    Active = true,
                    FacilityNumber = "BRF691433",
                    File = { },   //243-0069
                    EnvironmentalInterest = { },   //BROWN
                    FacilityType = { },   //BROWN
                    OrganizationalUnit = { },   //Org Unit
                    BudgetCode = { },   //HWBRVRP
                    Name = "WOODSTOCK CROSSING",
                    ComplianceOfficer = { },   //00299456
                    FacilityStatus = { },    //ACTIVE
                    Location = "Some place in Woodstock",
                    Address = "12050 HWY 92",
                    City = "WOODSTOCK",
                    State = "GA",
                    PostalCode = "30188",
                    Latitude = "34.086774",
                    Longitude = "-84.485922",
                    County = { },   // CHEROKEE
                    RetentionRecords = { }  //1
                },
                new Facility
                {
                    Id = new Guid("E697F074-9C1C-4CEF-93F0-FCD9610ECCD3"),
                    Active = true,
                    FacilityNumber = "GAR000068791",
                    File = { },   //243-0075
                    EnvironmentalInterest = { },   //RCRA
                    FacilityType = { },   //gen
                    OrganizationalUnit = { },   //Org Unit
                    BudgetCode = { },   //HWRCRA
                    Name = "RITE AID #11757",
                    ComplianceOfficer = { },   //360513
                    FacilityStatus = { },   //ACTIVE
                    Location = "Highway 92 Woodstock",
                    Address = "12075 HWY 92",
                    City = "WOODSTOCK",
                    State = "GA",
                    PostalCode = "30188",
                    Latitude = "34.0887669",
                    Longitude = "-84.4851441",
                    County = { },   //CHEROKEE
                    RetentionRecords = { }   //1
                },
                new Facility
                {
                    Id = new Guid("D6C596EA-0530-460F-A105-2FB772F8F0B2"),
                    Active = true,
                    FacilityNumber = "GAR000077271",
                    File = { },   //243-0079
                    EnvironmentalInterest = { },   //RCRA
                    FacilityType = { },   //gen
                    OrganizationalUnit = { },   //Org Unit
                    BudgetCode = { },   //HWRCRA
                    Name = "KROGER #011-419",
                    ComplianceOfficer = { },   //360513
                    FacilityStatus = { },   //ACTIVE
                    Location = "Kroger Plaza, Woodstock",
                    Address = "12050 HWY 92 SUITE 112",
                    City = "WOODSTOCK",
                    State = "GA",
                    PostalCode = "30188",
                    Latitude = "34.0877275",
                    Longitude = "-84.4858512",
                    County = { },   //CHEROKEE
                    RetentionRecords = { }   //1
                },
                new Facility
                {
                    Id = new Guid("309436BC-F7E7-4BFD-8455-E868129D6F45"),
                    Active = true,
                    FacilityNumber = "GAR000068759",
                    File = { },   //243-0071
                    EnvironmentalInterest = { },   //RCRA
                    FacilityType = { },   //gen
                    OrganizationalUnit = { },   //Org Unit
                    BudgetCode = { },   //HWRCRA
                    Name = "RITE AID #11758",
                    ComplianceOfficer = { },   //00360513
                    FacilityStatus = { },   //ACTIVE
                    Location = "Shopping center parking lot",
                    Address = "5329 OLD HWY 5",
                    City = "WOODSTOCK",
                    State = "GA",
                    PostalCode = "30188",
                    Latitude = "34.141353",
                    Longitude = "-84.505629",
                    County = { },   //CHEROKEE
                    RetentionRecords = { }   //1
                }
            }.ToArray();
        }
    }
}
