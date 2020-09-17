using FMS.Domain.Entities;
using System;
using System.Collections.Generic;

namespace FMS.Infrastructure.SeedData
{
    public static partial class DevSeedData
    {
        public static List<RetentionRecord> GetRetentionRecords()
        {
            return new List<RetentionRecord>
            {
                new RetentionRecord
                {
                    Id = new Guid("6781CB75-2573-4095-B4CF-18596B4E9D58"),
                    Active = true,
                    FacilityId= new Guid("3FF8B38C-B2A0-4A32-B703-BEAB9138B7F0"),
                    StartYear = 2003,
                    EndYear = 2009,
                    BoxNumber = "2014L249CA"
                },
                new RetentionRecord
                {
                    Id = new Guid("852617B3-D76A-48E5-A765-E391956E3AB4"),
                    Active = true,
                    FacilityId= new Guid("3FF8B38C-B2A0-4A32-B703-BEAB9138B7F0"),
                    StartYear = 2010,
                    EndYear = 2016,
                    BoxNumber = "2018LT254P"
                },
                new RetentionRecord
                {
                    Id = new Guid("890e5de1-6499-41fe-969e-bdaac9e4bed9"),
                    Active = true,
                    FacilityId= new Guid("50AEC751-D2FA-42D1-BE02-3EDF721787CA"),
                    StartYear = 2001,
                    EndYear = 2002,
                    BoxNumber = "2014L249CA"
                },
                new RetentionRecord
                {
                    Id = new Guid("0e9456c2-b360-4b5c-bf7a-132a8a01978c"),
                    Active = true,
                    FacilityId= new Guid("50AEC751-D2FA-42D1-BE02-3EDF721787CA"),
                    StartYear = 2003,
                    EndYear = 2005,
                    BoxNumber = "2018LT254P"
                },
                new RetentionRecord
                {
                    Id = new Guid("b6c3fa12-72e8-4f79-a882-aac873612753"),
                    Active = true,
                    FacilityId= new Guid("50AEC751-D2FA-42D1-BE02-3EDF721787CA"),
                    StartYear = 2001,
                    EndYear = 2002,
                    BoxNumber = "2016L046RC"
                },
                new RetentionRecord
                {
                    Id = new Guid("f75ea5d0-a759-4366-b056-081913e5f473"),
                    Active = true,
                    FacilityId= new Guid("7B20DE98-4726-4789-9AEA-2D995FF6839A"),
                    StartYear = 2003,
                    EndYear = 2005,
                    BoxNumber = "2016L046RC"
                }
            };
        }
    }
}
