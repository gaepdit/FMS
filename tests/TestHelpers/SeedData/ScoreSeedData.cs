using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.TestData.SeedData
{
    public static partial class SeedData
    {
        public static List<Score> GetScores()
        {
            return new List<Score>()
            {
                new()
                {
                    Id = new Guid("C0FCD976-5361-4253-B140-CA39BCEC35E7"),
                    Active = true,
                    FacilityId = new Guid("50AEC751-D2FA-42D1-BE02-3EDF721787CA"),
                    ScoredDate = new(2018, 2, 13),
                    Comments = "Overriding comment",
                    UseComments = true
                },
                new()
                {
                    Id = new Guid("2268E4C6-CFDC-4786-B383-CB65829DE520"),
                    Active = true,
                    FacilityId = new Guid("3A7457EC-E4A4-47D2-B47C-35078C3F5BF7"),
                    ScoredDate = new(2018, 2, 13),
                    Comments = "New comment to use",
                    UseComments = false
                },
                new()
                {
                    Id = new Guid("8F63491E-A3A6-4ABE-BF89-479CB2915059"),
                    Active = true,
                    FacilityId = new Guid("7B20DE98-4726-4789-9AEA-2D995FF6839A"),
                    ScoredDate = new(2018, 2, 13),
                    Comments = "",
                    UseComments = false
                },
                new()
                {
                    Id = new Guid("5119BF82-D9C6-4014-B951-2F1B850BAA99"),
                    Active = true,
                    FacilityId = new Guid("E697F074-9C1C-4CEF-93F0-FCD9610ECCD3"),
                    ScoredDate = new(2018, 2, 13),
                    Comments = "Another comment",
                    UseComments = true
                }
            };
        }
    }
}
