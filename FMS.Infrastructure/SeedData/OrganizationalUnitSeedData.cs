using FMS.Domain.Entities;
using System;
using System.Collections.Generic;

namespace FMS.Infrastructure.SeedData
{
    public static partial class DevSeedData
    {
        public static List<OrganizationalUnit> GetOrganizationalUnits()
        {
            return new List<OrganizationalUnit>
            {
                new OrganizationalUnit
                {
                    Id = new Guid("1845DB88-B57C-42B7-B954-7EAD37A499AC"),
                    Active = true,
                    Name = "Brownfields Management",
                    ComplianceOfficers = { }
                },
                new OrganizationalUnit
                {
                    Id = new Guid("8B0D0573-1F6D-42FB-B953-2AD6F874DD69"),
                    Active = true,
                    Name = "Combustion & Treatment",
                    ComplianceOfficers = { }
                },
                new OrganizationalUnit
                {
                    Id = new Guid("7C2224E3-5E60-4459-9BCB-8338A65C485E"),
                    Active = true,
                    Name = "DOD Facilities",
                    ComplianceOfficers = { }
                },
                new OrganizationalUnit
                {
                    Id = new Guid("B251EDD9-8C77-4884-B9A6-4C720782F98F"),
                    Active = true,
                    Name = "Facility Restoration",
                    ComplianceOfficers = { }
                },
                new OrganizationalUnit
                {
                    Id = new Guid("57B8BEB5-368A-4056-872D-0DB0ADE175E3"),
                    Active = true,
                    Name = "Generator Compliance",
                    ComplianceOfficers = { }
                },
                new OrganizationalUnit
                {
                    Id = new Guid("6B86B16E-F2FA-4AF2-9468-A45EE5F62E3D"),
                    Active = true,
                    Name = "PA/SI Sub-Unit under Release Notification",
                    ComplianceOfficers = { }
                },
                new OrganizationalUnit
                {
                    Id = new Guid("3FF12EE9-7295-45F9-A12D-766BCFB6AADC"),
                    Active = true,
                    Name = "Release Notification",
                    ComplianceOfficers = { }
                },
                new OrganizationalUnit
                {
                    Id = new Guid("C78BF1F0-C10D-4130-A68F-FC636CE60277"),
                    Active = true,
                    Name = "Remedial 1",
                    ComplianceOfficers = { }
                },
                new OrganizationalUnit
                {
                    Id = new Guid("5C5B0346-8954-4352-A540-5D7FA5334985"),
                    Active = true,
                    Name = "Remedial 2",
                    ComplianceOfficers = { }
                },
                new OrganizationalUnit
                {
                    Id = new Guid("E4B8988B-7E5C-4938-84B4-87E2D5F54176"),
                    Active = true,
                    Name = "Remedial 3",
                    ComplianceOfficers = { }
                },
                new OrganizationalUnit
                {
                    Id = new Guid("513A1958-0506-415E-A551-431E318ABB34"),
                    Active = true,
                    Name = "Remedial Sites",
                    ComplianceOfficers = { }
                },
                new OrganizationalUnit
                {
                    Id = new Guid("3DAD8120-E972-46D6-869A-2B1AB2BC87F7"),
                    Active = true,
                    Name = "Response Administration",
                    ComplianceOfficers = { }
                },
                new OrganizationalUnit
                {
                    Id = new Guid("8144D1A7-AD3B-4122-8A7F-AE1AED88EBB8"),
                    Active = true,
                    Name = "Response Development",
                    ComplianceOfficers = { }
                },
                new OrganizationalUnit
                {
                    Id = new Guid("803C846C-E681-42BA-9A12-F6A63C7E4FD8"),
                    Active = true,
                    Name = "Response Management",
                    ComplianceOfficers = { }
                },
                new OrganizationalUnit
                {
                    Id = new Guid("49BCBE4B-3E98-47ED-A31C-147399153A48"),
                    Active = true,
                    Name = "Risk Assessment",
                    ComplianceOfficers = { }
                },
                new OrganizationalUnit
                {
                    Id = new Guid("4CCBE72A-09E6-4EE4-9E6B-381D2F18EDC2"),
                    Active = true,
                    Name = "Superfund Management",
                    ComplianceOfficers = { }
                },
                new OrganizationalUnit
                {
                    Id = new Guid("DBF20D94-0C1E-4141-A034-DA29507AD9A3"),
                    Active = true,
                    Name = "Treatment & Storage",
                    ComplianceOfficers = { }
                },
                new OrganizationalUnit
                {
                    Id = new Guid("00B39C4D-374C-44CF-8701-F9B7E73FEA5D"),
                    Active = true,
                    Name = "NA",
                    ComplianceOfficers = { }
                }
            };
        }
    }
}
