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
            FacilityId = id;
            ParcelNumber = parcel.ParcelNumber;
            ParcelDescription = parcel.ParcelDescription;
            ParcelTypeId = parcel.ParcelTypeId;
            Acres = parcel.Acres;
            Latitude = parcel.Latitude;
            Longitude = parcel.Longitude;
        }
        public Guid FacilityId { get; set; }

        public string ParcelNumber { get; set; }

        public string ParcelDescription { get; set; } 

        public Guid? ParcelTypeId { get; set; }
        public ParcelType ParcelType { get; set; }

        public double Acres { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
