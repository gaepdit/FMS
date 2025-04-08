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
        public Guid LocationId { get; set; } = Guid.Empty;  
        public string ParcelId { get; set; } = string.Empty;
        public string ParcelDescription { get; set; } = string.Empty;
        public string ParcelType { get; set; } = string.Empty;
        public double Acres { get; set; } = 0.0;
        public double Latitude { get; set; } = 0.0;
        public double Longitude { get; set; } = 0.0;
    }
}
