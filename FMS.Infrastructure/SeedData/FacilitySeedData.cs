using FMS.Domain.Entities;
using System;
using System.Collections.Generic;

namespace FMS.Infrastructure.SeedData
{
    public static partial class DevSeedData
    {
        public static List<Facility> GetFacilities()
        {
            return new List<Facility>
            {
                new Facility
                {
                    Id = new Guid("3FF8B38C-B2A0-4A32-B703-BEAB9138B7F0"),
                    Active = true,
                    FacilityNumber = "BRF1211825",
                    FileId = new Guid("5019EBBC-8F99-469A-BCDC-256823EDD9A2"),   //FileLabel = "243-0001"
                    EnvironmentalInterestId = new Guid("FC2A0444-6287-432F-9285-6BA0E7AA73C6"),  //RCRA
                    FacilityTypeId = new Guid("3FE94D7D-563E-4CA1-A094-BB6E217990D2"),   //GEN
                    OrganizationalUnitId = new Guid("57B8BEB5-368A-4056-872D-0DB0ADE175E3"),    //Org Unit
                    BudgetCodeId = new Guid("457D191A-D2B1-4C38-8633-9061C4268E37"),  //HWRCRA
                    Name = "3 BRANCHES SUBDIVISION (CAPITAL DESIGN CONSTRUCT.)",
                    ComplianceOfficerId = new Guid("FCE1195E-BF17-4513-B617-029EE8766A6E"),  //01069946 
                    FacilityStatusId = new Guid("0FF0A063-2D11-4305-BADA-E9A4414EDDF1"), // // new  FacilityStatus{ },   //NON-RCRA
                    Location = "Description of Location",
                    Address = "102 THREE BRANCHES DR.",
                    City = "WOODSTOCK",
                    State = "Georgia",
                    PostalCode = "30188",
                    Latitude = 34.114309m,
                    Longitude = -84.470057m,
                    CountyId =  243,   //Id = 243, Name = "Cherokee"
                },
                new Facility
                {
                    Id = new Guid("AB44F9C7-C2EC-47BC-8886-60D72B5BD5EB"),
                    Active = false,
                    FacilityNumber = "BRF3191858",
                    FileId = new Guid("790B04E8-F5F5-412E-95E2-B785E630A2A7"),  //248-0001
                    EnvironmentalInterestId = new Guid("C68D44B3-7283-40B1-8105-0B999CED87C5"),    //BROWN
                    FacilityTypeId = new Guid("4C30CECF-B53E-4D09-B919-A9E07E4E9782"),   //BROWN
                    OrganizationalUnitId = new Guid("1845DB88-B57C-42B7-B954-7EAD37A499AC"),  //Org Unit
                    BudgetCodeId = new Guid("0B1B88EB-9957-4BBA-87A7-F599FA88D725"),   //HWBRVRP
                    Name = "TOONIGH VILLAGE BUILDING C",
                    ComplianceOfficerId = new Guid("468F746A-270F-4584-8B04-71CD5271A40F"),  //00943668
                    FacilityStatusId = new Guid("0FF0A063-2D11-4305-BADA-E9A4414EDDF1"),
                    Location = "Some location description here",
                    Address = "5335 HOLLY SPRINGS PARKWAY",
                    City = "WOODSTOCK",
                    State = "Georgia",
                    PostalCode = "30188",
                    Latitude = 34.1416484m,
                    Longitude = -84.5047357m,
                    CountyId = 261, // new County{ },  // CHEROKEE
                },
                new Facility
                {
                    Id = new Guid("50AEC751-D2FA-42D1-BE02-3EDF721787CA"),
                    Active = true,
                    FacilityNumber = "BRF3471841",
                    FileId = new Guid("5019EBBC-8F99-469A-BCDC-256823EDD9A2"),    //243-0085
                    EnvironmentalInterestId = new Guid("C68D44B3-7283-40B1-8105-0B999CED87C5"),   //BROWN
                    FacilityTypeId = new Guid("4C30CECF-B53E-4D09-B919-A9E07E4E9782"),   //BROWN
                    OrganizationalUnitId = new Guid("1845DB88-B57C-42B7-B954-7EAD37A499AC"),   //Org Unit
                    BudgetCodeId = new Guid("0B1B88EB-9957-4BBA-87A7-F599FA88D725"),   //HWBRVRP
                    Name = "WOODSTOCK SHOPPING CENTER",
                    ComplianceOfficerId = new Guid("B87CADC7-AD43-40CD-A1B6-C906883E386B"),   //00945102
                    FacilityStatusId = new Guid("0FF0A063-2D11-4305-BADA-E9A4414EDDF1"),
                    Location = "Description of Location in Woodstock",
                    Address = "9980, 10010 AND 10020 HIGHWAY 92 AND 1906 PROFESSIONAL WAY",
                    City = "WOODSTOCK",
                    State = "Georgia",
                    PostalCode = "30188",
                    Latitude = 34.084926m,
                    Longitude = -84.521794m,
                    CountyId = 243,  // CHEROKEE
                },
                new Facility
                {
                    Id = new Guid("7B20DE98-4726-4789-9AEA-2D995FF6839A"),
                    Active = false,
                    FacilityNumber = "BRF341436",
                    FileId = new Guid("015A39B3-A522-4C13-9479-B17626247313"),   //243-0068
                    EnvironmentalInterestId = new Guid("C68D44B3-7283-40B1-8105-0B999CED87C5"),   //BROWN
                    FacilityTypeId = new Guid("4C30CECF-B53E-4D09-B919-A9E07E4E9782"),   // BROWN
                    OrganizationalUnitId = new Guid("1845DB88-B57C-42B7-B954-7EAD37A499AC"), //Org Unit
                    BudgetCodeId = new Guid("0B1B88EB-9957-4BBA-87A7-F599FA88D725"),   //HWBRVRP
                    Name = "106 ARNOLD MILL ROAD",
                    ComplianceOfficerId = new Guid("255ACC97-1C23-4621-A08A-FE77B500BDD0"),   //00285148
                    FacilityStatusId = new Guid("0FF0A063-2D11-4305-BADA-E9A4414EDDF1"),
                    Location = "Somewhere in Woodstock",
                    Address = "106 ARNOLD MILL RD.",
                    City = "WOODSTOCK",
                    State = "Georgia",
                    PostalCode = "30188",
                    Latitude = 34.100926m,
                    Longitude = -84.516605m,
                    CountyId = 243,  // CHEROKEE
                },
                new Facility
                {
                    Id = new Guid("3A7457EC-E4A4-47D2-B47C-35078C3F5BF7"),
                    Active = true,
                    FacilityNumber = "BRF691433",
                    FileId = new Guid("47B44DDE-D9D0-4799-AB32-20E829F9D53C"),   //243-0069
                    EnvironmentalInterestId = new Guid("C68D44B3-7283-40B1-8105-0B999CED87C5"),   //BROWN
                    FacilityTypeId = new Guid("4C30CECF-B53E-4D09-B919-A9E07E4E9782"),   //BROWN
                    OrganizationalUnitId = new Guid("1845DB88-B57C-42B7-B954-7EAD37A499AC"),   //Org Unit
                    BudgetCodeId = new Guid("0B1B88EB-9957-4BBA-87A7-F599FA88D725"),   //HWBRVRP
                    Name = "WOODSTOCK CROSSING",
                    ComplianceOfficerId = new Guid("C505460A-1AFF-4A9C-9637-3FF5CC09878D"),   //01080246
                    FacilityStatusId = new Guid("0FF0A063-2D11-4305-BADA-E9A4414EDDF1"),
                    Location = "Some place in Woodstock",
                    Address = "12050 HWY 92",
                    City = "WOODSTOCK",
                    State = "Georgia",
                    PostalCode = "30188",
                    Latitude = 34.086774m,
                    Longitude = -84.485922m,
                    CountyId = 243,  // CHEROKEE
                },
                new Facility
                {
                    Id = new Guid("E697F074-9C1C-4CEF-93F0-FCD9610ECCD3"),
                    Active = true,
                    FacilityNumber = "GAR000068791",
                    FileId = new Guid("5019EBBC-8F99-469A-BCDC-256823EDD9A2"),   //243-0075
                    EnvironmentalInterestId = new Guid("FC2A0444-6287-432F-9285-6BA0E7AA73C6"),   //RCRA
                    FacilityTypeId = new Guid("3FE94D7D-563E-4CA1-A094-BB6E217990D2"),   //gen
                    OrganizationalUnitId = new Guid("57B8BEB5-368A-4056-872D-0DB0ADE175E3"),   //Org Unit
                    BudgetCodeId = new Guid("457D191A-D2B1-4C38-8633-9061C4268E37"),   //HWRCRA
                    Name = "RITE AID #11757",
                    ComplianceOfficerId = new Guid("63B66867-0961-4ADE-99D2-85D6D4FED985"),   //00945809
                    FacilityStatusId = new Guid("0FF0A063-2D11-4305-BADA-E9A4414EDDF1"),
                    Location = "Highway 92 Woodstock",
                    Address = "12075 HWY 92",
                    City = "WOODSTOCK",
                    State = "Georgia",
                    PostalCode = "30188",
                    Latitude = 34.0887669m,
                    Longitude = -84.4851441m,
                    CountyId = 243,  // CHEROKEE
                },
                new Facility
                {
                    Id = new Guid("D6C596EA-0530-460F-A105-2FB772F8F0B2"),
                    Active = true,
                    FacilityNumber = "GAR000077271",
                    FileId = new Guid("EF5FB128-D3BF-4CFF-9931-9F114D25D8A1"), // 180-0001
                    EnvironmentalInterestId = new Guid("FC2A0444-6287-432F-9285-6BA0E7AA73C6"),   //RCRA
                    FacilityTypeId = new Guid("3FE94D7D-563E-4CA1-A094-BB6E217990D2"),   //gen
                    OrganizationalUnitId = new Guid("57B8BEB5-368A-4056-872D-0DB0ADE175E3"),   //Org Unit
                    BudgetCodeId = new Guid("457D191A-D2B1-4C38-8633-9061C4268E37"),   //HWRCRA
                    Name = "KROGER #011-419",
                    ComplianceOfficerId = new Guid("FCE1195E-BF17-4513-B617-029EE8766A6E"),   //01069946
                    FacilityStatusId = new Guid("0FF0A063-2D11-4305-BADA-E9A4414EDDF1"),
                    Location = "Kroger Plaza, Woodstock",
                    Address = "12050 HWY 92 SUITE 112",
                    City = "WOODSTOCK",
                    State = "Georgia",
                    PostalCode = "30188",
                    Latitude = 34.0877275m,
                    Longitude = -84.4858512m,
                    CountyId = 243,  // CHEROKEE
                },
                new Facility
                {
                    Id = new Guid("309436BC-F7E7-4BFD-8455-E868129D6F45"),
                    Active = true,
                    FacilityNumber = "GAR000068759",
                    FileId = new Guid("EF5FB128-D3BF-4CFF-9931-9F114D25D8A1"), // 180-0001
                    EnvironmentalInterestId = new Guid("FC2A0444-6287-432F-9285-6BA0E7AA73C6"),   //RCRA
                    FacilityTypeId = new Guid("3FE94D7D-563E-4CA1-A094-BB6E217990D2"),   //gen
                    OrganizationalUnitId = new Guid("57B8BEB5-368A-4056-872D-0DB0ADE175E3"),   //Org Unit
                    BudgetCodeId = new Guid("457D191A-D2B1-4C38-8633-9061C4268E37"),   //HWRCRA
                    Name = "RITE AID #11758",
                    ComplianceOfficerId = new Guid("468F746A-270F-4584-8B04-71CD5271A40F"),   //00943668
                    FacilityStatusId = new Guid("0FF0A063-2D11-4305-BADA-E9A4414EDDF1"),
                    Location = "Shopping center parking lot",
                    Address = "5329 OLD HWY 5",
                    City = "WOODSTOCK",
                    State = "Georgia",
                    PostalCode = "30188",
                    Latitude = 34.141353m,
                    Longitude = -84.505629m,
                    CountyId = 243,  // CHEROKEE
                }
            };
        }
    }
}
