using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.TestData.SeedData
{
    public static partial class SeedData
    {
        public static List<UserPosition> GetUserPositions()
        {
            return new List<UserPosition>()
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