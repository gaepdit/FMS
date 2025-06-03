using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.Platform.Extensions.DevHelpers.SeedData
{
    public static partial class SeedData
    {
        private static IEnumerable<Parcel> GetParcels()
        {
            return new List<Parcel>()
            {
                new()
                {
                    Id = new Guid("54A814CC-5406-4D48-A171-7A99AEFF3FDC"),
                    Active = true,
                    LocationId = new Guid("533EBBD5-4144-41D5-9338-C3C5317C3E5E"),
                    ParcelNumber = "1587",
                    ParcelDescription = "Back 40",
                    ParcelTypeId = new Guid("A5F9B7BE-0664-44BF-9A51-72792AE53253"),
                    Acres = 2,
                    Latitude = 0,
                    Longitude = 0
                },
                new()
                {
                    Id = new Guid("6C844893-097D-4B81-AC2D-FCAD54DC9C42"),
                    Active = true,
                    LocationId = new Guid("533EBBD5-4144-41D5-9338-C3C5317C3E5E"),
                    ParcelNumber = "157jhg",
                    ParcelDescription = "Some chunk of land",
                    ParcelTypeId = new Guid("A5F9B7BE-0664-44BF-9A51-72792AE53253"),
                    Acres = .5,
                    Latitude = 0,
                    Longitude = 0
                },
                new()
                {
                    Id = new Guid("4E376EF7-C5C7-439F-A500-3E371A5274AC"),
                    Active = true,
                    LocationId = new Guid("A2ACAA03-EEAA-4409-B16B-1759FB4314E3"),
                    ParcelNumber = "HYGT6473",
                    ParcelDescription = "Some parcel",
                    ParcelTypeId = new Guid("A5F9B7BE-0664-44BF-9A51-72792AE53253"),
                    Acres = 0.73,
                    Latitude = 0,
                    Longitude = 0
                },
                new()
                {
                    Id = new Guid("AFB9C6E8-C34A-4A57-85F5-06B3D75A4983"),
                    Active = true,
                    LocationId = new Guid("41CD50F4-353D-450A-AB4E-8BE3EAF27CA8"),
                    ParcelNumber = "HGTTE-869D",
                    ParcelDescription = "A hill out back",
                    ParcelTypeId = new Guid("BF8D1BBF-921D-4091-B2D2-800AB933D7DC"),
                    Acres = 10.3,
                    Latitude = 0,
                    Longitude = 0
                },
                new()
                {
                    Id = new Guid("451BEFE5-248A-4746-AFDF-4AE7AC5D01BB"),
                    Active = true,
                    LocationId = new Guid("77D2B98D-0C55-46EA-9583-B62BD934762C"),
                    ParcelNumber = "3856",
                    ParcelDescription = "Front Yard",
                    ParcelTypeId = new Guid("BF8D1BBF-921D-4091-B2D2-800AB933D7DC"),
                    Acres = 0.47,
                    Latitude = 0,
                    Longitude = 0
                },
                new()
                {
                    Id = new Guid("B39DCD19-D6AB-42DD-A8B0-417C0353A78D"),
                    Active = true,
                    LocationId = new Guid("A99FF86A-2F97-48DB-830B-9C92F88DE681"),
                    ParcelNumber = "325445",
                    ParcelDescription = "Parcel 23",
                    ParcelTypeId = new Guid("BF8D1BBF-921D-4091-B2D2-800AB933D7DC"),
                    Acres = 0.87,
                    Latitude = 0,
                    Longitude = 0
                },
                new()
                {
                    Id = new Guid("AFD49795-B941-40EF-A0AC-174C1993D2F3"),
                    Active = true,
                    LocationId = new Guid("98B3C9ED-F747-48AE-8B92-497A35927E90"),
                    ParcelNumber = "RTY76435",
                    ParcelDescription = "Another chunk of land",
                    ParcelTypeId = new Guid("C52EBAF5-F777-4F16-AE84-B0D36BB2A248"),
                    Acres = 0.023,
                    Latitude = 0,
                    Longitude = 0
                },
                new()
                {
                    Id = new Guid("829F2C54-4B82-4D5F-AA36-216CFB794C96"),
                    Active = true,
                    LocationId = new Guid("98B3C9ED-F747-48AE-8B92-497A35927E90"),
                    ParcelNumber = "Parcel h465",
                    ParcelDescription = "Vacant Lot",
                    ParcelTypeId = new Guid("C52EBAF5-F777-4F16-AE84-B0D36BB2A248"),
                    Acres = 1.0,
                    Latitude = 0,
                    Longitude = 0
                },
                new()
                {
                    Id = new Guid("F2903BDB-0C6F-4EB6-955F-D49FF1D36508"),
                    Active = true,
                    LocationId = new Guid("77D2B98D-0C55-46EA-9583-B62BD934762C"),
                    ParcelNumber = "VFR67456",
                    ParcelDescription = "Description of a lot",
                    ParcelTypeId = new Guid("C52EBAF5-F777-4F16-AE84-B0D36BB2A248"),
                    Acres = .234,
                    Latitude = 0,
                    Longitude = 0
                },
                new()
                {
                    Id = new Guid("A239BC42-AE50-41F0-B41F-1BAAD5FD147A"),
                    Active = true,
                    LocationId = new Guid("77D2B98D-0C55-46EA-9583-B62BD934762C"),
                    ParcelNumber = "95767",
                    ParcelDescription = "kj&^&%$khdg",
                    ParcelTypeId = new Guid("990C501E-0E1B-4B8C-ABA3-5EC8B6F0321E"),
                    Acres = 45.0,
                    Latitude = 0,
                    Longitude = 0
                },
                new()
{
                    Id = new Guid("07865EAF-187F-4FB5-BDAC-E86213DB0621"),
                    Active = true,
                    LocationId = new Guid("A2ACAA03-EEAA-4409-B16B-1759FB4314E3"),
                    ParcelNumber = "srg-9",
                    ParcelDescription = "Corn Field",
                    ParcelTypeId = new Guid("990C501E-0E1B-4B8C-ABA3-5EC8B6F0321E"),
                    Acres = 2.0,
                    Latitude = 0,
                    Longitude = 0
                },
                new()
    {
                    Id = new Guid("4EB2A1E3-C8BD-4BB4-9631-79BE81B5CCBA"),
                    Active = true,
                    LocationId = new Guid("A2ACAA03-EEAA-4409-B16B-1759FB4314E3"),
                    ParcelNumber = "HYT",
                    ParcelDescription = "Hog Paddock",
                    ParcelTypeId = new Guid("990C501E-0E1B-4B8C-ABA3-5EC8B6F0321E"),
                    Acres = 0.76,
                    Latitude = 0,
                    Longitude = 0
                }
            };
        }
    }
}
