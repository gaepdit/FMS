using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.TestData.SeedData
{
    public static partial class SeedData
    {
        public static List<Chemical> GetChemicals()
        {
            return new List<Chemical>()
            {
                new()
                {
                    Id = new Guid("2365ba02-13a9-406c-9e39-f600f37d64e5"),
                    Active = true,
                    CasNo = "83329",
                    ChemicalName = "Acenaphthene",
                    ToxValue = "2",
                    MCLs = ""
                },
                new() 
                {
                    Id = new Guid("a39e6b98-0c37-432c-9a35-fccb9151703f"),
                    Active = true,
                    CasNo = "7440360",
                    ChemicalName = "Antimony",
                    ToxValue = "16",
                    MCLs = ".006"
                },
                new() 
                {
                    Id = new Guid("e197d2d5-193b-409f-88bb-f0191f3f508d"),
                    Active = true,
                    CasNo = "7440382", 
                    ChemicalName = "Arsenic", 
                    ToxValue = "16", 
                    MCLs = ".010",
                },
                new()
                {
                    Id = new Guid("a6c4e176-f45d-47e9-a61b-4fe5f3341dec"),
                    Active = true,
                    CasNo = "2642719", 
                    ChemicalName = "Azinphos-Ethyl", 
                    ToxValue = "4", 
                    MCLs = "",
                },
                new()
                {
                    Id = new Guid("c87369b4-bbc2-41fd-b24f-48f0199126fb"),
                    Active = true,
                    CasNo = "71432", 
                    ChemicalName = "Benzene", 
                    ToxValue = "8", 
                    MCLs = "0.005",
                },
                new()
                {
                    Id = new Guid("DC132688-7B95-4328-8655-057C95EB59D1"),
                    Active = true,
                    CasNo = "98884", 
                    ChemicalName = "Benzene Carbonal Chloride", 
                    ToxValue = "4", 
                    MCLs = "",
                },
                new()
                {
                    Id = new Guid("5ECEF580-F3CB-4C60-8D3E-AA19AE49A03F"),
                    Active = true,
                    CasNo = "50328",
                    ChemicalName = "Benzo (A) Pyrene",
                    ToxValue = "16",
                    MCLs = "2E-4",
                },
                new()
                {
                    Id = new Guid("1BD5A2B6-5D1A-4477-B621-E7DC4E6CB504"),
                    Active = true,
                    CasNo = "205992",
                    ChemicalName = "Benzo (A) Fluoranthene",
                    ToxValue = "8",
                    MCLs = "",
                },
                new()
                {
                    Id = new Guid("816D044A-8B2C-4C71-AB75-C1137971E6D6"),
                    Active = true,
                    CasNo = "67663",
                    ChemicalName = "Chloroform",
                    ToxValue = "4",
                    MCLs = "0.080",
                },
                new()
                {
                    Id = new Guid("31A34C9F-5600-4D18-85E5-5242B4D8BB26"),
                    Active = true,
                    CasNo = "107302",
                    ChemicalName = "Chloromethyl Methyl Ether",
                    ToxValue = "16",
                    MCLs = "",
                },
                new()
                {
                    Id = new Guid("AF3F6523-91C9-41E2-B08A-6BEAD2599FAA"),
                    Active = true,
                    CasNo = "18540299",
                    ChemicalName = "Chromium (VI)",
                    ToxValue = "16",
                    MCLs = "",
                }
            };
        }
    }
}
