using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FMS.Infrastructure.SeedData
{
    public static partial class DevSeedData
    {
        public static ComplianceOfficer[] GetComplianceOfficers()
        {
            return new List<ComplianceOfficer>
            {
                new ComplianceOfficer
                {
                    Id = new Guid("FCE1195E-BF17-4513-B617-029EE8766A6E"),
                    Active = true,
                    Name = "01069946",
                    Unit = { }
                },
                new ComplianceOfficer
                {
                    Id = new Guid("468F746A-270F-4584-8B04-71CD5271A40F"),
                    Active = true,
                    Name = "00943668",
                    Unit = { }
                },
                new ComplianceOfficer
                {
                    Id = new Guid("B87CADC7-AD43-40CD-A1B6-C906883E386B"),
                    Active = true,
                    Name = "00945102",
                    Unit = { }
                },
                new ComplianceOfficer
                {
                    Id = new Guid("255ACC97-1C23-4621-A08A-FE77B500BDD0"),
                    Active = true,
                    Name = "00285148",
                    Unit = { }
                },
                new ComplianceOfficer
                {
                    Id = new Guid("C505460A-1AFF-4A9C-9637-3FF5CC09878D"),
                    Active = true,
                    Name = "01080246",
                    Unit = { }
                },
                new ComplianceOfficer
                {
                    Id = new Guid("63B66867-0961-4ADE-99D2-85D6D4FED985"),
                    Active = true,
                    Name = "00945809",
                    Unit = { }
                }
            }.ToArray();
        }
    }
}
