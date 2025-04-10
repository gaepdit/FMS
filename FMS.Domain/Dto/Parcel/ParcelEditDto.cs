using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Domain.Dto
{
    public class ParcelEditDto
    {
        public ParcelEditDto() { }
        public ParcelEditDto(Parcel parcel)
        {
            LocationId = parcel.LocationId;
            ParcelId = parcel.ParcelId;
            ParcelDescription = parcel.ParcelDescription;
            ParcelType = parcel.ParcelType;
            Acres = parcel.Acres;
            Latitude = parcel.Latitude;
            Longitude = parcel.Longitude;
        }
        public Guid LocationId { get; set; }

        [Display(Name = "Parcel ID")]
        public string ParcelId { get; set; }

        [Display(Name = "Deescription")]
        public string ParcelDescription { get; set; }

        [Display(Name = "Type")]
        public string ParcelType { get; set; }

        [Display(Name = "Acreage")]
        public double Acres { get; set; }

        [Display(Name = "Latitude")]
        public double Latitude { get; set; }

        [Display(Name = "Longitude")]
        public double Longitude { get; set; }
    }

}
