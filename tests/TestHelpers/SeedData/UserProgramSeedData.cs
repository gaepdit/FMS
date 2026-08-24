using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.TestData.SeedData
{
    public static partial class SeedData
    {
        public static List<UserProgram> GetUserPrograms()
        {
            return new List<UserProgram>()
            {
                new()
                {
                    Id = new Guid("d8129875-0a51-4281-8ae7-13f76a3c04c2"),
                    Active = true,
                    Name = "Hazardous Waste",
                    Description = "Hazardous Waste Program",
                },
                new()
                {
                    Id = new Guid("9ec5ba13-edaf-4058-bc51-b31cc136e5a3"),
                    Active = true,
                    Name = "Response and Remediation",
                    Description = "Response and Remediation Program",
                }
            };
        }
    }
}