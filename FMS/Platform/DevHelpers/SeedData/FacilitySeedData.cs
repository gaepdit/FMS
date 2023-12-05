using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.Platform.Extensions.DevHelpers.SeedData
{
    public static partial class SeedData
    {
        private static IEnumerable<Facility> GetFacilities()
        {
            return new List<Facility>
            {
                new()
                {
                    Id = new Guid("3FF8B38C-B2A0-4A32-B703-BEAB9138B7F0"),
                    Active = true,
                    FacilityNumber = "BRF1211825",
                    FileId = new Guid("5019EBBC-8F99-469A-BCDC-256823EDD9A2"), //FileLabel = "243-0001"
                    FacilityTypeId = new Guid("3FE94D7D-563E-4CA1-A094-BB6E217990D2"), // VRP
                    OrganizationalUnitId = new Guid("57B8BEB5-368A-4056-872D-0DB0ADE175E3"), //Org Unit
                    BudgetCodeId = new Guid("457D191A-D2B1-4C38-8633-9061C4268E37"), //HWRCRA
                    Name = "3 BRANCHES SUBDIVISION (CAPITAL DESIGN CONSTRUCT.)",
                    ComplianceOfficerId = new Guid("FCE1195E-BF17-4513-B617-029EE8766A6E"), //01069946 
                    FacilityStatusId = new Guid("0FF0A063-2D11-4305-BADA-E9A4414EDDF1"), 
                    Location = "Description of Location",
                    Address = "102 THREE BRANCHES DR.",
                    City = "WOODSTOCK",
                    State = "Georgia",
                    PostalCode = "30188",
                    Latitude = 32.662793m,
                    Longitude = -83.180553m,
                    CountyId = 243, //Id = 243, Name = "Cherokee"
                    IsRetained = true,
                },
                new()
                {
                    Id = new Guid("AB44F9C7-C2EC-47BC-8886-60D72B5BD5EB"),
                    Active = true,
                    FacilityNumber = "BRF3191858",
                    FileId = new Guid("790B04E8-F5F5-412E-95E2-B785E630A2A7"), //248-0001
                    FacilityTypeId = new Guid("C8F61579-46F8-47FD-B7C4-FEB17F26384B"), //BROWN
                    OrganizationalUnitId = new Guid("1845DB88-B57C-42B7-B954-7EAD37A499AC"), //Org Unit
                    BudgetCodeId = new Guid("0B1B88EB-9957-4BBA-87A7-F599FA88D725"), //HWBRVRP
                    Name = "TOONIGH VILLAGE BUILDING C",
                    ComplianceOfficerId = new Guid("468F746A-270F-4584-8B04-71CD5271A40F"), //00943668
                    FacilityStatusId = new Guid("0FF0A063-2D11-4305-BADA-E9A4414EDDF1"),
                    Location = "Some location description here",
                    Address = "5335 HOLLY SPRINGS PARKWAY",
                    City = "WOODSTOCK",
                    State = "Georgia",
                    PostalCode = "30188",
                    Latitude = 34.1416484m,
                    Longitude = -84.5047357m,
                    CountyId = 261, // new County{ },  // CHEROKEE
                    IsRetained = true,
                },
                new()
                {
                    Id = new Guid("50AEC751-D2FA-42D1-BE02-3EDF721787CA"),
                    Active = true,
                    FacilityNumber = "BRF3471841",
                    FileId = new Guid("5019EBBC-8F99-469A-BCDC-256823EDD9A2"), //243-0085
                    FacilityTypeId = new Guid("6F19934A-6AF2-438B-8858-03FA6AC4E78A"), // GEN
                    OrganizationalUnitId = new Guid("1845DB88-B57C-42B7-B954-7EAD37A499AC"), //Org Unit
                    BudgetCodeId = new Guid("0B1B88EB-9957-4BBA-87A7-F599FA88D725"), //HWBRVRP
                    Name = "WOODSTOCK SHOPPING CENTER",
                    ComplianceOfficerId = new Guid("B87CADC7-AD43-40CD-A1B6-C906883E386B"), //00945102
                    FacilityStatusId = new Guid("0FF0A063-2D11-4305-BADA-E9A4414EDDF1"),
                    Location = "Description of Location in Woodstock",
                    Address = "9980, 10010 AND 10020 HIGHWAY 92 AND 1906 PROFESSIONAL WAY",
                    City = "WOODSTOCK",
                    State = "Georgia",
                    PostalCode = "30188",
                    Latitude = 34.084926m,
                    Longitude = -84.521794m,
                    CountyId = 243, // CHEROKEE
                    IsRetained = true,
                },
                new()
                {
                    Id = new Guid("7B20DE98-4726-4789-9AEA-2D995FF6839A"),
                    Active = true,
                    FacilityNumber = "BRF341436",
                    FileId = new Guid("015A39B3-A522-4C13-9479-B17626247313"), //243-0068
                    FacilityTypeId = new Guid("3FE54579-1762-4A77-9AD7-9D185E000A79"), // TSD
                    OrganizationalUnitId = new Guid("1845DB88-B57C-42B7-B954-7EAD37A499AC"), //Org Unit
                    BudgetCodeId = new Guid("0B1B88EB-9957-4BBA-87A7-F599FA88D725"), //HWBRVRP
                    Name = "106 ARNOLD MILL ROAD",
                    ComplianceOfficerId = new Guid("255ACC97-1C23-4621-A08A-FE77B500BDD0"), //00285148
                    FacilityStatusId = new Guid("0FF0A063-2D11-4305-BADA-E9A4414EDDF1"),
                    Location = "Somewhere in Woodstock",
                    Address = "106 ARNOLD MILL RD.",
                    City = "WOODSTOCK",
                    State = "Georgia",
                    PostalCode = "30188",
                    Latitude = 34.100926m,
                    Longitude = -84.516605m,
                    CountyId = 243, // CHEROKEE
                    IsRetained = true,
                },
                new()
                {
                    Id = new Guid("3A7457EC-E4A4-47D2-B47C-35078C3F5BF7"),
                    Active = true,
                    FacilityNumber = "BRF691433",
                    FileId = new Guid("47B44DDE-D9D0-4799-AB32-20E829F9D53C"), //243-0069
                    FacilityTypeId = new Guid("C8F61579-46F8-47FD-B7C4-FEB17F26384B"), //BROWN
                    OrganizationalUnitId = new Guid("1845DB88-B57C-42B7-B954-7EAD37A499AC"), //Org Unit
                    BudgetCodeId = new Guid("0B1B88EB-9957-4BBA-87A7-F599FA88D725"), //HWBRVRP
                    Name = "WOODSTOCK CROSSING",
                    ComplianceOfficerId = new Guid("C505460A-1AFF-4A9C-9637-3FF5CC09878D"), //01080246
                    FacilityStatusId = new Guid("0FF0A063-2D11-4305-BADA-E9A4414EDDF1"),
                    Location = "Some place in Woodstock",
                    Address = "12050 HWY 92",
                    City = "WOODSTOCK",
                    State = "Georgia",
                    PostalCode = "30188",
                    Latitude = 34.086774m,
                    Longitude = -84.485922m,
                    CountyId = 243, // CHEROKEE
                    IsRetained = true,
                },
                new()
                {
                    Id = new Guid("E697F074-9C1C-4CEF-93F0-FCD9610ECCD3"),
                    Active = true,
                    FacilityNumber = "GAR000068791",
                    FileId = new Guid("5a7ca0e7-e767-4583-98fe-6def04eebb68"), //243-0075
                    FacilityTypeId = new Guid("3FE94D7D-563E-4CA1-A094-BB6E217990D2"), // VRP
                    OrganizationalUnitId = new Guid("57B8BEB5-368A-4056-872D-0DB0ADE175E3"), //Org Unit
                    BudgetCodeId = new Guid("457D191A-D2B1-4C38-8633-9061C4268E37"), //HWRCRA
                    Name = "RITE AID #11757",
                    ComplianceOfficerId = new Guid("63B66867-0961-4ADE-99D2-85D6D4FED985"), //00945809
                    FacilityStatusId = new Guid("0FF0A063-2D11-4305-BADA-E9A4414EDDF1"),
                    Location = "Highway 92 Woodstock",
                    Address = "12075 HWY 92",
                    City = "WOODSTOCK",
                    State = "Georgia",
                    PostalCode = "30188",
                    Latitude = 34.0887669m,
                    Longitude = -84.4851441m,
                    CountyId = 243, // CHEROKEE
                    IsRetained = true,
                },
                new()
                {
                    Id = new Guid("D6C596EA-0530-460F-A105-2FB772F8F0B2"),
                    Active = true,
                    FacilityNumber = "GAR000077271",
                    FileId = new Guid("EF5FB128-D3BF-4CFF-9931-9F114D25D8A1"), // 180-0001
                    FacilityTypeId = new Guid("3FE94D7D-563E-4CA1-A094-BB6E217990D2"), // VRP
                    OrganizationalUnitId = new Guid("57B8BEB5-368A-4056-872D-0DB0ADE175E3"), //Org Unit
                    BudgetCodeId = new Guid("457D191A-D2B1-4C38-8633-9061C4268E37"), //HWRCRA
                    Name = "KROGER #011-419",
                    ComplianceOfficerId = new Guid("FCE1195E-BF17-4513-B617-029EE8766A6E"), //01069946
                    FacilityStatusId = new Guid("0FF0A063-2D11-4305-BADA-E9A4414EDDF1"),
                    Location = "Kroger Plaza, Woodstock",
                    Address = "12050 HWY 92 SUITE 112",
                    City = "WOODSTOCK",
                    State = "Georgia",
                    PostalCode = "30188",
                    Latitude = 34.0877275m,
                    Longitude = -84.4858512m,
                    CountyId = 243, // CHEROKEE
                    IsRetained = true,
                },
                new()
                {
                    Id = new Guid("309436BC-F7E7-4BFD-8455-E868129D6F45"),
                    Active = true,
                    FacilityNumber = "GAR000068759",
                    FileId = new Guid("5a7ca0e7-e767-4583-98fe-6def04eebb68"), // 180-0001
                    FacilityTypeId = new Guid("3FE94D7D-563E-4CA1-A094-BB6E217990D2"), // VRP
                    OrganizationalUnitId = new Guid("57B8BEB5-368A-4056-872D-0DB0ADE175E3"), //Org Unit
                    BudgetCodeId = new Guid("457D191A-D2B1-4C38-8633-9061C4268E37"), //HWRCRA
                    Name = "RITE AID #11758",
                    ComplianceOfficerId = new Guid("468F746A-270F-4584-8B04-71CD5271A40F"), //00943668
                    FacilityStatusId = new Guid("0FF0A063-2D11-4305-BADA-E9A4414EDDF1"),
                    Location = "Shopping center parking lot",
                    Address = "5329 OLD HWY 5",
                    City = "WOODSTOCK",
                    State = "Georgia",
                    PostalCode = "30188",
                    Latitude = 34.141353m,
                    Longitude = -84.505629m,
                    CountyId = 243, // CHEROKEE
                    IsRetained = false,
                },
                new()
                {
                    Id = new Guid("109436BC-F7E7-4BFD-8455-E868129D6F45"),
                    Active = false,
                    FacilityNumber = "DELETED123",
                    FileId = new Guid("EF5FB128-D3BF-4CFF-9931-9F114D25D8A1"), // 180-0001
                    FacilityTypeId = new Guid("3FE94D7D-563E-4CA1-A094-BB6E217990D2"), // VRP
                    OrganizationalUnitId = new Guid("57B8BEB5-368A-4056-872D-0DB0ADE175E3"), //Org Unit
                    BudgetCodeId = new Guid("457D191A-D2B1-4C38-8633-9061C4268E37"), //HWRCRA
                    Name = "Facility Name",
                    ComplianceOfficerId = new Guid("468F746A-270F-4584-8B04-71CD5271A40F"), //00943668
                    FacilityStatusId = new Guid("0FF0A063-2D11-4305-BADA-E9A4414EDDF1"),
                    Location = "Shopping center parking lot",
                    Address = "5400 OLD HWY 5",
                    City = "WOODSTOCK",
                    State = "Georgia",
                    PostalCode = "30188",
                    Latitude = 34.1396m,
                    Longitude = -84.5040m,
                    CountyId = 243, // CHEROKEE
                    IsRetained = true,
                },
                new()
                {
                    Id = new Guid("810DDE72-5459-4ECC-81D8-A51554C9FF3F"),
                    Active = true,
                    FacilityNumber = "UTF8",
                    FileId = new Guid("EF5FB128-D3BF-4CFF-9931-9F114D25D8A1"), // 180-0001
                    FacilityTypeId = new Guid("3FE94D7D-563E-4CA1-A094-BB6E217990D2"), // VRP
                    OrganizationalUnitId = new Guid("57B8BEB5-368A-4056-872D-0DB0ADE175E3"), //Org Unit
                    BudgetCodeId = new Guid("457D191A-D2B1-4C38-8633-9061C4268E37"), //HWRCRA
                    Name = "Unicode’s fun 🐛👍😜",
                    ComplianceOfficerId = new Guid("468F746A-270F-4584-8B04-71CD5271A40F"), //00943668
                    FacilityStatusId = new Guid("0FF0A063-2D11-4305-BADA-E9A4414EDDF1"),
                    Location = "Parking lot",
                    Address = "123 Street",
                    City = "Town",
                    State = "Georgia",
                    PostalCode = "30188",
                    Latitude = 34.1m,
                    Longitude = -84.5m,
                    CountyId = 243,
                    IsRetained = false,
                },
                new()
                {
                    Id = new Guid("BF25C413-0EE1-4280-84BD-0B2631F4EEC7"),
                    Active = true,
                    FacilityNumber = "ADD1",
                    FileId = new Guid("EF5FB128-D3BF-4CFF-9931-9F114D25D8A1"), // 180-0001
                    FacilityTypeId = new Guid("3FE94D7D-563E-4CA1-A094-BB6E217990D2"), // VRP
                    OrganizationalUnitId = new Guid("57B8BEB5-368A-4056-872D-0DB0ADE175E3"), //Org Unit
                    BudgetCodeId = new Guid("457D191A-D2B1-4C38-8633-9061C4268E37"), //HWRCRA
                    Name = "No address",
                    ComplianceOfficerId = new Guid("468F746A-270F-4584-8B04-71CD5271A40F"), //00943668
                    FacilityStatusId = new Guid("0FF0A063-2D11-4305-BADA-E9A4414EDDF1"),
                    Location = "Parking lot",
                    Address = "",
                    City = "A-Town",
                    State = "Georgia",
                    PostalCode = "30188",
                    Latitude = 34.1m,
                    Longitude = -84.5m,
                    CountyId = 243,
                    IsRetained = true,
                },
                new()
                {
                    Id = new Guid("754FEEE0-5167-4909-A587-51C0CE9DFEB6"),
                    Active = true,
                    FacilityNumber = "ADD2",
                    FileId = new Guid("EF5FB128-D3BF-4CFF-9931-9F114D25D8A1"), // 180-0001
                    FacilityTypeId = new Guid("C8F61579-46F8-47FD-B7C4-FEB17F26384B"), //BROWN
                    OrganizationalUnitId = new Guid("57B8BEB5-368A-4056-872D-0DB0ADE175E3"), //Org Unit
                    BudgetCodeId = new Guid("457D191A-D2B1-4C38-8633-9061C4268E37"), //HWRCRA
                    Name = "No address 2",
                    ComplianceOfficerId = new Guid("468F746A-270F-4584-8B04-71CD5271A40F"), //00943668
                    FacilityStatusId = new Guid("0FF0A063-2D11-4305-BADA-E9A4414EDDF1"),
                    Location = "Parking lot",
                    Address = "",
                    City = "B-Town",
                    State = "Georgia",
                    PostalCode = "30188",
                    Latitude = 34.1m,
                    Longitude = -84.5m,
                    CountyId = 243,
                    IsRetained = true,
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Active = true,
                    FacilityNumber = "FMS-183",
                    FileId = new Guid("EF5FB128-D3BF-4CFF-9931-9F114D25D8A1"), // 180-0001
                    FacilityTypeId = new Guid("3FE94D7D-563E-4CA1-A094-BB6E217990D2"), // VRP
                    OrganizationalUnitId = new Guid("57B8BEB5-368A-4056-872D-0DB0ADE175E3"), //Org Unit
                    BudgetCodeId = new Guid("457D191A-D2B1-4C38-8633-9061C4268E37"), //HWRCRA
                    Name = "Facility for testing Stored Procedure math error",
                    ComplianceOfficerId = new Guid("468F746A-270F-4584-8B04-71CD5271A40F"), //00943668
                    FacilityStatusId = new Guid("0FF0A063-2D11-4305-BADA-E9A4414EDDF1"),
                    Location = "N/A",
                    Address = "N/A",
                    City = "N/A",
                    State = "Georgia",
                    PostalCode = "12345",
                    Latitude = 33.1m,
                    Longitude = -83.5m,
                    CountyId = 243,
                    IsRetained = true,
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Active = true,
                    FacilityNumber = "RN10235",
                    FileId = new Guid("5a7ca0e7-e767-4583-98fe-6def04eebb68"),
                    FacilityTypeId = new Guid("B7224976-5D67-40F8-8112-273AE3B91419"),
                    OrganizationalUnitId = new Guid("3FF12EE9-7295-45F9-A12D-766BCFB6AADC"),
                    BudgetCodeId = new Guid("5B4D0049-3AA3-4FC7-A8FE-59A771D0F7F8"),
                    Name = "New Business in Woodstock Release Notification",
                    ComplianceOfficerId = new Guid("468F746A-270F-4584-8B04-71CD5271A40F"),
                    FacilityStatusId = new Guid("0FF0A063-2D11-4305-BADA-E9A4414EDDF1"),
                    Location = "A spot in a strip mall",
                    Address = "10917 HWY 92",
                    City = "Woodstock",
                    State = "Georgia",
                    PostalCode = "30188",
                    Latitude = 34.086774m,
                    Longitude = -84.505922m,
                    CountyId = 243,
                    IsRetained = true,
                    HSInumber = "10234",
                    NonHSILetterDate = new(2018, 2, 13),
                    Comments = "Just some comments about this facility",
                    PreRQSMcleanup = false,
                    ImageChecked = true,
                    DeferredOnSiteScoring = true,
                    AdditionalDataRequested = false,
                    VRPReferral = false
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Active = true,
                    FacilityNumber = "RN102412",
                    FileId = new Guid("EF5FB128-D3BF-4CFF-9931-9F114D25D8A1"),
                    FacilityTypeId = new Guid("B7224976-5D67-40F8-8112-273AE3B91419"),
                    OrganizationalUnitId = new Guid("3FF12EE9-7295-45F9-A12D-766BCFB6AADC"),
                    BudgetCodeId = new Guid("5B4D0049-3AA3-4FC7-A8FE-59A771D0F7F8"),
                    Name = "Facility Test-4 Release Notification",
                    ComplianceOfficerId = new Guid("468F746A-270F-4584-8B04-71CD5271A40F"),
                    FacilityStatusId = new Guid("0FF0A063-2D11-4305-BADA-E9A4414EDDF1"),
                    Location = "Some Random Strip Mall",
                    Address = "10919 HWY 92",
                    City = "Woodstock",
                    State = "Georgia",
                    PostalCode = "30188",
                    Latitude = 34.086774m,
                    Longitude = -84.506122m,
                    CountyId = 243,
                    IsRetained = true,
                    HSInumber = "10251",
                    NonHSILetterDate = new DateOnly(2020, 11, 17),
                    Comments = "Strip that looks like every other strip mall",
                    PreRQSMcleanup = false,
                    ImageChecked = false,
                    DeferredOnSiteScoring = true,
                    AdditionalDataRequested = true,
                    VRPReferral = false
                }
            };
        }
    }
}