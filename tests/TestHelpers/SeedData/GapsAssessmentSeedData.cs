using FMS.Domain.Entities;

namespace FMS.TestData.SeedData
{
    public static partial class SeedData
    {
        public static List<GapsAssessment> GetGapsAssessments()
        {
            return new List<GapsAssessment>()
            {
                new()
                {
                    Id = new Guid("5CA0BFE3-25FA-4639-9668-22754F460C2B"),
                    Active = true,
                    Name = "I",
                    Description = "Description one",
                },
                new()
                {
                    Id = new Guid("DC9738A8-7C88-421A-8E1A-36D0B518C582"),
                    Active = true,
                    Name = "II",
                    Description = "Description II",
                },
                new()
                {
                    Id = new Guid("EA051EA4-1EF9-493A-B094-60D011AD6136"),
                    Active = true,
                    Name = "V",
                    Description = "Description 5",
                },
                new()
                {
                    Id = new Guid("E1F11C25-43EE-4C87-8461-1308E935451F"),
                    Active = true,
                    Name = "Unk",
                    Description = "Description Unknown",
                }
            };
        }
    }
}
