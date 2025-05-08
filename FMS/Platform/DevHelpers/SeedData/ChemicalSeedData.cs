using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.Platform.Extensions.DevHelpers.SeedData
{
    public static partial class SeedData
    {
        private static IEnumerable<Chemical> GetChemicals()
        {
            return new List<Chemical>()
            {
                new()
                {
                    Id = new Guid("2365ba02-13a9-406c-9e39-f600f37d64e5"),
                    Active = true,
                    CasNo = "",
                    ChemicalName = "",
                    ToxValue = "",
                    MCLs = ""
                },
                new() 
                {
                    Id = new Guid("a39e6b98-0c37-432c-9a35-fccb9151703f"),
                    Active = true,
                    CasNo = "",
                    ChemicalName = "",
                    ToxValue = "",
                    MCLs = ""
                },
                new()
                {
                    Id = new Guid("e197d2d5-193b-409f-88bb-f0191f3f508d"),
                    Active = true,
                    CasNo = "", 
                    ChemicalName = "", 
                    ToxValue = "", 
                    MCLs = "",
                },
                new()
                {
                    Id = new Guid("a6c4e176-f45d-47e9-a61b-4fe5f3341dec"),
                    Active = true,
                    CasNo = "", 
                    ChemicalName = "", 
                    ToxValue = "", 
                    MCLs = "",
                },
                new()
                {
                    Id = new Guid("c87369b4-bbc2-41fd-b24f-48f0199126fb"),
                    Active = true,
                    CasNo = "", 
                    ChemicalName = "", 
                    ToxValue = "", 
                    MCLs = "",
                },
                new()
                {
                    Id = new Guid("DC132688-7B95-4328-8655-057C95EB59D1"),
                    Active = true,
                    CasNo = "", 
                    ChemicalName = "", 
                    ToxValue = "", 
                    MCLs = "",
                },
                new()
                {
                    Id = new Guid("5ECEF580-F3CB-4C60-8D3E-AA19AE49A03F"),
                    Active = true,
                    CasNo = "",
                    ChemicalName = "",
                    ToxValue = "",
                    MCLs = "",
                },
                new()
                {
                    Id = new Guid("1BD5A2B6-5D1A-4477-B621-E7DC4E6CB504"),
                    Active = true,
                    CasNo = "",
                    ChemicalName = "",
                    ToxValue = "",
                    MCLs = "",
                },
                new()
                {
                    Id = new Guid("816D044A-8B2C-4C71-AB75-C1137971E6D6"),
                    Active = true,
                    CasNo = "",
                    ChemicalName = "",
                    ToxValue = "",
                    MCLs = "",
                },
                new()
                {
                    Id = new Guid("31A34C9F-5600-4D18-85E5-5242B4D8BB26"),
                    Active = true,
                    CasNo = "",
                    ChemicalName = "",
                    ToxValue = "",
                    MCLs = "",
                },
                new()
                {
                    Id = new Guid("AF3F6523-91C9-41E2-B08A-6BEAD2599FAA"),
                    Active = true,
                    CasNo = "",
                    ChemicalName = "",
                    ToxValue = "",
                    MCLs = "",
                }
            };
        }
    }
}
