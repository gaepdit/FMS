using FMS.Domain.Entities;

namespace FMS.TestData.SeedData
{
    public static partial class SeedData
    {
        public static List<AbandonedInactive> GetAbandonedInactives()
        {
            return new List<AbandonedInactive>
            {
                new()
                {
                    Id = new Guid("9c9582b2-c065-4640-933c-eeff05626b04"),
                    Active = true,
                    Name = "Drums/Abandoned Liquid Waste",
                    Description = "Initial abandoned/inactive status for testing purposes."
                },
                new()
                {
                    Id = new Guid("88faf018-66fe-4b0e-a9fc-71bcaef20194"),
                    Active = true,
                    Name = "High Community Involvement",
                    Description = "Secondary abandoned/inactive status for testing purposes."
                },
                new()
                {
                    Id = new Guid("74f1dbdd-cd4a-463d-b426-3ccc98fc0ab6"),
                    Active = true,
                    Name = "Known/Ongoing Exposure",
                    Description = "Tertiary abandoned/inactive status for testing purposes."
                },
                new()
                {
                    Id = new Guid("222d50a5-3158-4228-a7cd-a0e86e8223dd"),
                    Active = true,
                    Name = "Potential To Delist",
                    Description = "Quaternary abandoned/inactive status for testing purposes."
                },
                new()
                {
                    Id = new Guid("abd0b8e9-920e-4242-9f7d-b0c8ae2efda4"),
                    Active = true,
                    Name = "Potential To Redevelop",
                    Description = "Quinary abandoned/inactive status for testing purposes."
                }
            };
        }
    }
}
