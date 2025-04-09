using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class Parcel : BaseActiveModel
    {
        public Parcel() { }

        public Parcel(ParcelEditDto parcel)
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
        public string ParcelId { get; set; }
        public string ParcelDescription { get; set; } 
        public string ParcelType { get; set; } 
        public double Acres { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
