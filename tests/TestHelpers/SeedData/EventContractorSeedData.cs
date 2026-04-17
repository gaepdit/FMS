using FMS.Domain.Entities;

namespace FMS.TestData.SeedData
{
    public static partial class SeedData
    {
        public static List<EventContractor> GetEventContractors()
        {
            return new List<EventContractor>()
            {
                new EventContractor()
                {
                    Id = new Guid("0DBEDF10-14FA-4FE9-A241-40FD5374ACFC"),
                    Active = true,
                    Name = "AECS",
                    Description = "AECS"
                },
                new EventContractor()
                {
                    Id = new Guid("9E4EAF2E-251C-48EE-8A1E-5F34BE80CD27"),
                    Active = true,
                    Name = "WSP",
                    Description = "WSP aka Wood, AMEC"
                },
                new EventContractor()
                {
                    Id = new Guid("CDA13444-C0FB-48DC-BE77-B94FAFF33CC5"),
                    Active = true,
                    Name = "Atlas",
                    Description = "Atlas"
                },
                new EventContractor()
                {
                    Id = new Guid("488E409C-1C5A-4667-98FE-1387960B2A3F"),
                    Active = true,
                    Name = "UES",
                    Description = "aka UES, UES-Contour, Contour, Contour (UES)"
                },
                new EventContractor()
                {
                    Id = new Guid("CF994588-4B71-4188-9A66-369D437E91B8"),
                    Active = true,
                    Name = "Kemron",
                    Description = "Kemron"
                },
                new EventContractor()
                {
                    Id = new Guid("3E2E125D-7CA7-45CA-8070-CDB6F3623E18"),
                    Active = true,
                    Name = "TT",
                    Description = "TT aka Tetra Tech"
                }
            };
        }
    }
}
