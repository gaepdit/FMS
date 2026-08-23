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
                    Id = new Guid(""),
                    Active = true,

                },
            };
        }
    }
}