using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FMS.Infrastructure.SeedData
{
    public static partial class DevSeedData
    {
        public static RetentionRecord[] GetRetentionRecords()
        {
            return new List<RetentionRecord>
            {
                new RetentionRecord
                {
                    Id = new Guid("6781CB75-2573-4095-B4CF-18596B4E9D58"),
                    Active = true,
                    Facility = { },
                    StartYear = 2003,
                    EndYear = 2015,
                    ConsignmentNumber = "",
                    BoxNumber = "",
                    ShelfNumber = "",
                    RetentionSchedule = ""
                },
                new RetentionRecord
                {
                    Id = new Guid("852617B3-D76A-48E5-A765-E391956E3AB4"),
                    Active = true,
                    Facility = { },
                    StartYear = 2000,
                    EndYear = 2010,
                    ConsignmentNumber = "",
                    BoxNumber = "",
                    ShelfNumber = "",
                    RetentionSchedule = ""
                }
            }.ToArray();
        }
    }
}
