using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.TestData.SeedData
{
    public static partial class SeedData
    {
        public static List<Parcel> GetParcels()
        {
            return new List<Parcel>()
            {
                new()
                {
                    Id = new Guid("54A814CC-5406-4D48-A171-7A99AEFF3FDC"),
                    Active = true,
                    FacilityId = new Guid("50AEC751-D2FA-42D1-BE02-3EDF721787CA"),
                    ParcelNumber = "1587",
                    SubListParcelName = "Back 40",
                    ParcelTypeId = new Guid("A5F9B7BE-0664-44BF-9A51-72792AE53253"),
                    Acres = 2,
                    ListDate = new DateOnly(2018, 3, 14),
                    DeListDate = null
                },
                new()
                {
                    Id = new Guid("6C844893-097D-4B81-AC2D-FCAD54DC9C42"),
                    Active = true,
                    FacilityId = new Guid("50AEC751-D2FA-42D1-BE02-3EDF721787CA"),
                    ParcelNumber = "157jhg",
                    SubListParcelName = "Some chunk of land",
                    ParcelTypeId = new Guid("A5F9B7BE-0664-44BF-9A51-72792AE53253"),
                    Acres = .5,
                    ListDate = new DateOnly(2019, 3, 14),
                    DeListDate = new DateOnly(2024, 7, 12)
                },
                new()
                {
                    Id = new Guid("4E376EF7-C5C7-439F-A500-3E371A5274AC"),
                    Active = true,
                    FacilityId = new Guid("3A7457EC-E4A4-47D2-B47C-35078C3F5BF7"),
                    ParcelNumber = "HYGT6473",
                    SubListParcelName = "Some parcel",
                    ParcelTypeId = new Guid("A5F9B7BE-0664-44BF-9A51-72792AE53253"),
                    Acres = 0.73,
                    ListDate = new DateOnly(2016, 3, 14),
                    DeListDate = null
                },
                new()
                {
                    Id = new Guid("AFB9C6E8-C34A-4A57-85F5-06B3D75A4983"),
                    Active = true,
                    FacilityId = new Guid("3A7457EC-E4A4-47D2-B47C-35078C3F5BF7"),
                    ParcelNumber = "HGTTE-869D",
                    SubListParcelName = "A hill out back",
                    ParcelTypeId = new Guid("BF8D1BBF-921D-4091-B2D2-800AB933D7DC"),
                    Acres = 10.3,
                    ListDate = new DateOnly(2019, 4, 2),
                    DeListDate = new DateOnly(2020, 3, 14)
                },
                new()
                {
                    Id = new Guid("451BEFE5-248A-4746-AFDF-4AE7AC5D01BB"),
                    Active = true,
                    FacilityId = new Guid("3A7457EC-E4A4-47D2-B47C-35078C3F5BF7"),
                    ParcelNumber = "3856",
                    SubListParcelName = "Front Yard",
                    ParcelTypeId = new Guid("BF8D1BBF-921D-4091-B2D2-800AB933D7DC"),
                    Acres = 0.47,
                    ListDate = new DateOnly(2017, 7, 5),
                    DeListDate = new DateOnly(2022, 3, 14)
                },
                new()
                {
                    Id = new Guid("B39DCD19-D6AB-42DD-A8B0-417C0353A78D"),
                    Active = true,
                    FacilityId = new Guid("3A7457EC-E4A4-47D2-B47C-35078C3F5BF7"),
                    ParcelNumber = "325445",
                    SubListParcelName = "Parcel 23",
                    ParcelTypeId = new Guid("BF8D1BBF-921D-4091-B2D2-800AB933D7DC"),
                    Acres = 0.87,
                    ListDate = new DateOnly(2022, 4, 14),
                    DeListDate = null
                },
                new()
                {
                    Id = new Guid("AFD49795-B941-40EF-A0AC-174C1993D2F3"),
                    Active = false,
                    FacilityId = new Guid("50AEC751-D2FA-42D1-BE02-3EDF721787CA"),
                    ParcelNumber = "RTY76435",
                    SubListParcelName = "Another chunk of land",
                    ParcelTypeId = new Guid("C52EBAF5-F777-4F16-AE84-B0D36BB2A248"),
                    Acres = 0.023,
                    ListDate = new DateOnly(2014, 3, 14),
                    DeListDate = new DateOnly(2024, 6, 23)
                },
                new()
                {
                    Id = new Guid("829F2C54-4B82-4D5F-AA36-216CFB794C96"),
                    Active = true,
                    FacilityId = new Guid("50AEC751-D2FA-42D1-BE02-3EDF721787CA"),
                    ParcelNumber = "Parcel h465",
                    SubListParcelName = "Vacant Lot",
                    ParcelTypeId = new Guid("C52EBAF5-F777-4F16-AE84-B0D36BB2A248"),
                    Acres = 1.0,
                    ListDate = new DateOnly(2016, 6, 14),
                    DeListDate = new DateOnly(2021, 7, 14)
                },
                new()
                {
                    Id = new Guid("F2903BDB-0C6F-4EB6-955F-D49FF1D36508"),
                    Active = true,
                    FacilityId = new Guid("50AEC751-D2FA-42D1-BE02-3EDF721787CA"),
                    ParcelNumber = "VFR67456",
                    SubListParcelName = "Description of a lot",
                    ParcelTypeId = new Guid("C52EBAF5-F777-4F16-AE84-B0D36BB2A248"),
                    Acres = .234,
                    ListDate = new DateOnly(2019, 9, 14),
                    DeListDate = new DateOnly(2023, 3, 22)
                },
                new()
                {
                    Id = new Guid("A239BC42-AE50-41F0-B41F-1BAAD5FD147A"),
                    Active = true,
                    FacilityId = new Guid("3A7457EC-E4A4-47D2-B47C-35078C3F5BF7"),
                    ParcelNumber = "95767",
                    SubListParcelName = "kj&^&%$khdg",
                    ParcelTypeId = new Guid("990C501E-0E1B-4B8C-ABA3-5EC8B6F0321E"),
                    Acres = 45.0,
                    ListDate = new DateOnly(2019, 11, 14),
                    DeListDate = null
                },
                new()
                {
                    Id = new Guid("07865EAF-187F-4FB5-BDAC-E86213DB0621"),
                    Active = true,
                    FacilityId = new Guid("50AEC751-D2FA-42D1-BE02-3EDF721787CA"),
                    ParcelNumber = "srg-9",
                    SubListParcelName = "Corn Field",
                    ParcelTypeId = new Guid("990C501E-0E1B-4B8C-ABA3-5EC8B6F0321E"),
                    Acres = 2.0,
                    ListDate = new DateOnly(2017, 8, 14),
                    DeListDate = new DateOnly(2023, 2, 21)
                },
                new()
                {
                    Id = new Guid("4EB2A1E3-C8BD-4BB4-9631-79BE81B5CCBA"),
                    Active = true,
                    FacilityId = new Guid("3A7457EC-E4A4-47D2-B47C-35078C3F5BF7"),
                    ParcelNumber = "HYT",
                    SubListParcelName = "Hog Paddock",
                    ParcelTypeId = new Guid("990C501E-0E1B-4B8C-ABA3-5EC8B6F0321E"),
                    Acres = 0.76,
                    ListDate = new DateOnly(2018, 3, 14),
                    DeListDate = null
                }
            };
        }
    }
}
