using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.Platform.Extensions.DevHelpers.SeedData
{
    public static partial class SeedData
    {
        private static IEnumerable<EventType> GetEventTypes()
        {
            return new List<EventType>()
            {
                new()
                {
                    Id = new Guid("86623A7E-DBB0-4364-B40A-6BBA1A473ADD"),
                    Active = true,
                    Name = "(Legacy) Annual Cert / FA",
                },
                new()
                {
                    Id = new Guid("DA7C8970-2C76-4860-8637-79F04EF03FDB"),
                    Active = true,
                    Name = "Abandoned/Inactive Site Review",
                },
                new()
                {
                    Id = new Guid("A80FA804-3A37-4E5A-BC1C-3E4B30EC8D79"),
                    Active = true,
                    Name = "Administrative Order",
                },
                new()
                {
                    Id = new Guid("AB83B36A-FCB2-4C97-918C-FD38421F9F41"),
                    Active = true,
                    Name = "Annual Certification",
                },
                new()
                {
                    Id = new Guid("8465A50C-18D6-4F87-BE7F-10021F1638C4"),
                    Active = true,
                    Name = "Compliance Status Report",
                },
                new()
                {
                    Id = new Guid("1791A93F-C9AC-43F1-BCD5-F687802DFE6D"),
                    Active = true,
                    Name = "Consent Order",
                },
                new()
                {
                    Id = new Guid("458A7D80-8E8C-4476-961C-CE823281EFAB"),
                    Active = true,
                    Name = "Corrective Action Plan",
                },
                new()
                {
                    Id = new Guid("6898B627-5E0C-48D1-9520-6A8428D9D7F3"),
                    Active = true,
                    Name = "Environmental Covenant",
                },
                new()
                {
                    Id = new Guid("867CAD56-D354-493A-A3CB-985101A5ACB7"),
                    Active = true,
                    Name = "Financial Assurance",
                },
                new()
                {
                    Id = new Guid("C1A6B598-9502-4310-8D43-7030EF3A44FA"),
                    Active = true,
                    Name = "Geologist Hydrogologic review",
                },
                new()
                {
                    Id = new Guid("04959D42-18C2-4D18-9839-C3D0C6AFA11B"),
                    Active = true,
                    Name = "Groundwater Monitoring Report",
                },
                new()
                {
                    Id = new Guid("F1A8BCAD-B09E-42BB-ABFD-74E230567A65"),
                    Active = true,
                    Name = "HWTF Master Project",
                },
                new()
                {
                    Id = new Guid("4CF16FE7-B240-49DE-AB17-0595DDD45F4E"),
                    Active = true,
                    Name = "HWTF Request for Advance",
                },
                new()
                {
                    Id = new Guid("9A657480-2DC4-40AE-84C2-21C464835FEB"),
                    Active = true,
                    Name = "HWTF Request for Reimbursement",
                },
                new()
                {
                    Id = new Guid("766262E9-38BD-4819-B9FB-546FA193EDEF"),
                    Active = true,
                    Name = "Misc Correspondence",
                },
                new()
                {
                    Id = new Guid("0A669B8E-6043-4089-B225-1C4261E3731D"),
                    Active = true,
                    Name = "Notice of Violation",
                },
                new()
                {
                    Id = new Guid("9F2BF565-5A7C-4389-B9DB-EBB0EAB66FB1"),
                    Active = true,
                    Name = "Order of the Court/Civil Penalties",
                },
                new()
                {
                    Id = new Guid("7AF4D45F-0C17-4231-9D0B-3971051B75E6"),
                    Active = true,
                    Name = "PAF",
                },
                new()
                {
                    Id = new Guid("43F184E5-0CD9-4C4B-B2C1-1F093076C60F"),
                    Active = true,
                    Name = "PAF Invoice",
                },
                new()
                {
                    Id = new Guid("4563D366-6298-4ACF-99AF-CA70D28EDC4D"),
                    Active = true,
                    Name = "Petition for Hearing",
                },
                new()
                {
                    Id = new Guid("189CFDF2-CB33-41BA-BB52-921DE7720B9C"),
                    Active = true,
                    Name = "Progress Report / Misc. Report",
                },
                new()
                {
                    Id = new Guid("3B26DB21-52A8-4057-953F-6E5A1F5895CC"),
                    Active = true,
                    Name = "Prospective Purchaser Compliance Status Report",
                },
                new()
                {
                    Id = new Guid("3875FD5D-610F-4B6E-9EDE-262633AC157B"),
                    Active = true,
                    Name = "Removal Work Plan",
                },
                new()
                {
                    Id = new Guid("D58802CC-26D8-41B7-ADBA-4F867276B5CE"),
                    Active = true,
                    Name = "Response Activities Report",
                },
                new()
                {
                    Id = new Guid("97862545-E5ED-4FD5-873B-3C4965008BA2"),
                    Active = true,
                    Name = "TEST HSRP SQL",
                },
                new()
                {
                    Id = new Guid("6535561F-F958-432C-ACF3-6FE004705119"),
                    Active = true,
                    Name = "VRP Compliance Status Report",
                },
                new()
                {
                    Id = new Guid("588E50EB-440D-403D-A942-7A95509C5E4E"),
                    Active = true,
                    Name = "VRP Corrective Action Plan",
                },
                new()
                {
                    Id = new Guid("D2CFC4B4-C23F-47F8-A996-DE55C17DF431"),
                    Active = true,
                    Name = "VRP Progress Report / Misc. Report",
                }
            };
        }
    }
}