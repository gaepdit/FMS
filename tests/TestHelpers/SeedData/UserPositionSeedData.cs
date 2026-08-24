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
                    Id = new Guid("a7558e1b-f52e-4fd1-b961-a28ef11e37cc"),
                    Active = true,
                    Name = "CO",
                    Description = "Compliance Officer",
                },
                new()
                {
                    Id = new Guid("1361a427-fb79-47d7-a367-60b98924e9e4"),
                    Active = true,
                    Name = "PM1",
                    Description = "Unit Manager",
                },
                new()
                {
                    Id = new Guid("6e77d090-b0e5-447d-b74e-ae72eb440097"),
                    Active = true,
                    Name = "PM2",
                    Description = "Program Manager",
                }, 
                new()
                {
                    Id = new Guid("1506a56c-a85a-43f0-a91b-cd669b484f68"),
                    Active = true,
                    Name = "Team Lead",
                    Description = "Brownfield Team Lead",
                },
            };
        }
    }
}