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
                },
                new OrganizationalUnit
                {
                    Id = new Guid("8B0D0573-1F6D-42FB-B953-2AD6F874DD69"),
                    Active = true,
                    Name = "Combustion & Treatment",
                },
                new OrganizationalUnit
                {
                    Id = new Guid("7C2224E3-5E60-4459-9BCB-8338A65C485E"),
                    Active = true,
                    Name = "DOD Facilities",
                },
                new OrganizationalUnit
                {
                    Id = new Guid("B251EDD9-8C77-4884-B9A6-4C720782F98F"),
                    Active = true,
                    Name = "Facility Restoration",
                },
                new OrganizationalUnit
                {
                    Id = new Guid("57B8BEB5-368A-4056-872D-0DB0ADE175E3"),
                    Active = true,
                    Name = "Generator Compliance",
                },
                new OrganizationalUnit
                {
                    Id = new Guid("A8FDECB3-DD12-4985-B6CE-7BAAC7B246A4"),
                    Active = true,
                    Name = "Government Facilities",
                },
                new OrganizationalUnit
                {
                    Id = new Guid("7ABBD134-3792-4374-8AE5-D2B5E6FE6E2B"),
                    Active = true,
                    Name = "Lead/Asbestos",
                },
                new OrganizationalUnit
                {
                    Id = new Guid("6B86B16E-F2FA-4AF2-9468-A45EE5F62E3D"),
                    Active = true,
                    Name = "PA/SI Sub-Unit under Release Notification",
                },
                new OrganizationalUnit
                {
                    Id = new Guid("0989C0BC-C3CE-4A77-83C6-F86E3BB70B9F"),
                    Active = true,
                    Name = "Regulatory Compliance ",
                },
                new OrganizationalUnit
                {
                    Id = new Guid("3FF12EE9-7295-45F9-A12D-766BCFB6AADC"),
                    Active = true,
                    Name = "Release Notification",
                },
                new OrganizationalUnit
                {
                    Id = new Guid("C78BF1F0-C10D-4130-A68F-FC636CE60277"),
                    Active = true,
                    Name = "Remedial 1",
                },
                new OrganizationalUnit
                {
                    Id = new Guid("5C5B0346-8954-4352-A540-5D7FA5334985"),
                    Active = true,
                    Name = "Remedial 2",
                },
                new OrganizationalUnit
                {
                    Id = new Guid("E4B8988B-7E5C-4938-84B4-87E2D5F54176"),
                    Active = true,
                    Name = "Remedial 3",
                },
                new OrganizationalUnit
                {
                    Id = new Guid("513A1958-0506-415E-A551-431E318ABB34"),
                    Active = true,
                    Name = "Remedial Sites",
                },
                new OrganizationalUnit
                {
                    Id = new Guid("3DAD8120-E972-46D6-869A-2B1AB2BC87F7"),
                    Active = true,
                    Name = "Response Administration",
                },
                new OrganizationalUnit
                {
                    Id = new Guid("8144D1A7-AD3B-4122-8A7F-AE1AED88EBB8"),
                    Active = true,
                    Name = "Response Development",
                },
                new OrganizationalUnit
                {
                    Id = new Guid("803C846C-E681-42BA-9A12-F6A63C7E4FD8"),
                    Active = true,
                    Name = "Response Management",
                },
                new OrganizationalUnit
                {
                    Id = new Guid("B37FBE15-DC40-47E5-9AB1-953B46F20498"),
                    Active = true,
                    Name = "PIRT",
                },
                new OrganizationalUnit
                {
                    Id = new Guid("49BCBE4B-3E98-47ED-A31C-147399153A48"),
                    Active = true,
                    Name = "Risk Assessment",
                },
                new OrganizationalUnit
                {
                    Id = new Guid("4CCBE72A-09E6-4EE4-9E6B-381D2F18EDC2"),
                    Active = true,
                    Name = "Superfund Management",
                },
                new OrganizationalUnit
                {
                    Id = new Guid("628F1EE7-3B14-45CB-80E6-316F7CF0E11F"),
                    Active = true,
                    Name = "Surface Mining",
                },
                new OrganizationalUnit
                {
                    Id = new Guid("DBF20D94-0C1E-4141-A034-DA29507AD9A3"),
                    Active = true,
                    Name = "Treatment & Storage",
                },
                new OrganizationalUnit
                {
                    Id = new Guid("00B39C4D-374C-44CF-8701-F9B7E73FEA5D"),
                    Active = true,
                    Name = "NA",
                }
            };
        }
    }
}
