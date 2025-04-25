using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Excel;
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
                //LocationId = id;
                //ParcelNumber = parcel.ParcelNumber;
                //ParcelDescription = parcel.ParcelDescription;
                //ParcelType = parcel.ParcelType;
                //Acres = parcel.Acres;
                //Latitude = parcel.Latitude;
                //Longitude = parcel.Longitude;
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    LocationId = new Guid(""),
                    ParcelNumber = "",
                    ParcelDescription = "",
                    ParcelTypeId = new Guid(""),
                    Acres = 0,
                    Latitude = 0,
                    Longitude = 0
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    LocationId = new Guid(""),
                    ParcelNumber = "",
                    ParcelDescription = "",
                    ParcelTypeId = new Guid(""),
                    Acres = 0,
                    Latitude = 0,
                    Longitude = 0
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    LocationId = new Guid(""),
                    ParcelNumber = "",
                    ParcelDescription = "",
                    ParcelTypeId = new Guid(""),
                    Acres = 0,
                    Latitude = 0,
                    Longitude = 0
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    LocationId = new Guid(""),
                    ParcelNumber = "",
                    ParcelDescription = "",
                    ParcelTypeId = new Guid(""),
                    Acres = 0,
                    Latitude = 0,
                    Longitude = 0
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    LocationId = new Guid(""),
                    ParcelNumber = "",
                    ParcelDescription = "",
                    ParcelTypeId = new Guid(""),
                    Acres = 0,
                    Latitude = 0,
                    Longitude = 0
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    LocationId = new Guid(""),
                    ParcelNumber = "",
                    ParcelDescription = "",
                    ParcelTypeId = new Guid(""),
                    Acres = 0,
                    Latitude = 0,
                    Longitude = 0
                }
            };
        }
    }
}
