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
                    File = {  },   //FileLabel = "243-0001"
                    EnvironmentalInterest = { },
                    FacilityType = { },
                    OrganizationalUnit = { },
                    BudgetCode = { },
                    Name = "3 BRANCHES SUBDIVISION (CAPITAL DESIGN CONSTRUCT.)",
                    ComplianceOfficer = { },
                    FacilityStatus = { },
                    Location = "Description of Location",
                    Address = "102 THREE BRANCHES DR.",
                    City = "WOODSTOCK",
                    State = "GA",
                    PostalCode = 30188,
                    Latitude = 34.114309,
                    Longitude = -84.470057,
                    County = {  },   //Id = 243, Name = "Cherokee"
                    RetentionRecords = { }
                },
                new Facility
                {
                    Id = new Guid("AB44F9C7-C2EC-47BC-8886-60D72B5BD5EB"),
                    Active = true,
                    FacilityNumber = "",
                    File = { },
                    EnvironmentalInterest = { },
                    FacilityType = { },
                    OrganizationalUnit = { },
                    BudgetCode = { },
                    Name = "",
                    ComplianceOfficer = { },
                    FacilityStatus = { },
                    Location = "",
                    Address = "",
                    City = "",
                    State = "GA",
                    PostalCode = 30099,
                    Latitude = 25.37485,
                    Longitude = -84.46729,
                    County = { },
                    RetentionRecords = { }
                },
                new Facility
                {
                    Id = new Guid("50AEC751-D2FA-42D1-BE02-3EDF721787CA"),
                    Active = true,
                    FacilityNumber = "",
                    File = { },
                    EnvironmentalInterest = { },
                    FacilityType = { },
                    OrganizationalUnit = { },
                    BudgetCode = { },
                    Name = "",
                    ComplianceOfficer = { },
                    FacilityStatus = { },
                    Location = "",
                    Address = "",
                    City = "",
                    State = "GA",
                    PostalCode = 30099,
                    Latitude = 25.37485,
                    Longitude = -84.46729,
                    County = { },
                    RetentionRecords = { }
                },
                new Facility
                {
                    Id = new Guid("7B20DE98-4726-4789-9AEA-2D995FF6839A"),
                    Active = true,
                    FacilityNumber = "",
                    File = { },
                    EnvironmentalInterest = { },
                    FacilityType = { },
                    OrganizationalUnit = { },
                    BudgetCode = { },
                    Name = "",
                    ComplianceOfficer = { },
                    FacilityStatus = { },
                    Location = "",
                    Address = "",
                    City = "",
                    State = "GA",
                    PostalCode = 30099,
                    Latitude = 25.37485,
                    Longitude = -84.46729,
                    County = { },
                    RetentionRecords = { }
                },
                new Facility
                {
                    Id = new Guid("3A7457EC-E4A4-47D2-B47C-35078C3F5BF7"),
                    Active = true,
                    FacilityNumber = "",
                    File = { },
                    EnvironmentalInterest = { },
                    FacilityType = { },
                    OrganizationalUnit = { },
                    BudgetCode = { },
                    Name = "",
                    ComplianceOfficer = { },
                    FacilityStatus = { },
                    Location = "",
                    Address = "",
                    City = "",
                    State = "GA",
                    PostalCode = 30099,
                    Latitude = 25.37485,
                    Longitude = -84.46729,
                    County = { },
                    RetentionRecords = { }
                },
                new Facility
                {
                    Id = new Guid("E697F074-9C1C-4CEF-93F0-FCD9610ECCD3"),
                    Active = true,
                    FacilityNumber = "",
                    File = { },
                    EnvironmentalInterest = { },
                    FacilityType = { },
                    OrganizationalUnit = { },
                    BudgetCode = { },
                    Name = "",
                    ComplianceOfficer = { },
                    FacilityStatus = { },
                    Location = "",
                    Address = "",
                    City = "",
                    State = "GA",
                    PostalCode = 30099,
                    Latitude = 25.37485,
                    Longitude = -84.46729,
                    County = { },
                    RetentionRecords = { }
                },
                new Facility
                {
                    Id = new Guid("D6C596EA-0530-460F-A105-2FB772F8F0B2"),
                    Active = true,
                    FacilityNumber = "",
                    File = { },
                    EnvironmentalInterest = { },
                    FacilityType = { },
                    OrganizationalUnit = { },
                    BudgetCode = { },
                    Name = "",
                    ComplianceOfficer = { },
                    FacilityStatus = { },
                    Location = "",
                    Address = "",
                    City = "",
                    State = "GA",
                    PostalCode = 30099,
                    Latitude = 25.37485,
                    Longitude = -84.46729,
                    County = { },
                    RetentionRecords = { }
                },
                new Facility
                {
                    Id = new Guid("309436BC-F7E7-4BFD-8455-E868129D6F45"),
                    Active = true,
                    FacilityNumber = "",
                    File = { },
                    EnvironmentalInterest = { },
                    FacilityType = { },
                    OrganizationalUnit = { },
                    BudgetCode = { },
                    Name = "",
                    ComplianceOfficer = { },
                    FacilityStatus = { },
                    Location = "",
                    Address = "",
                    City = "",
                    State = "GA",
                    PostalCode = 30099,
                    Latitude = 25.37485,
                    Longitude = -84.46729,
                    County = { },
                    RetentionRecords = { }
                }
            }.ToArray();
        }
    }
}
