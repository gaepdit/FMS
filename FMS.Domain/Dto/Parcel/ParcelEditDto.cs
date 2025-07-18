using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class ParcelEditDto
    {
        public ParcelEditDto() { }
        public ParcelEditDto(Parcel parcel)
        {
            Active = parcel.Active;
            FacilityId = parcel.FacilityId;
            ParcelNumber = parcel.ParcelNumber;
            ParcelDescription = parcel.ParcelDescription;
            ParcelType = parcel.ParcelType;
            Acres = parcel.Acres;
            Latitude = parcel.Latitude;
            Longitude = parcel.Longitude;
        }
        public bool Active { get; set; }

        public Guid FacilityId { get; set; }

        [Display(Name = "Parcel Number")]
        public string ParcelNumber { get; set; }

        [Display(Name = "Description")]
        public string ParcelDescription { get; set; }

        [Display(Name = "Type")]
        public Guid ParcelTypeId { get; set; }
        public ParcelType ParcelType { get; set; }

        [Display(Name = "Acreage")]
        public double Acres { get; set; }

        [Display(Name = "Latitude")]
        public double Latitude { get; set; }

        [Display(Name = "Longitude")]
        public double Longitude { get; set; }
    }

}
