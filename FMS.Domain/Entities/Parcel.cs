using System;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class Parcel : BaseActiveModel
    {
        public Parcel() { }

        public Parcel(Guid id, ParcelCreateDto parcel)
        {
            LocationId = id;
            ParcelNumber = parcel.ParcelNumber;
            ParcelDescription = parcel.ParcelDescription;
            ParcelType = parcel.ParcelType;
            Acres = parcel.Acres;
            Latitude = parcel.Latitude;
            Longitude = parcel.Longitude;
        }
        public Guid LocationId { get; set; }
        public string ParcelNumber { get; set; }
        public string ParcelDescription { get; set; } 
        public string ParcelType { get; set; } 
        public double Acres { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
