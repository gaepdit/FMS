using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
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
        public Guid LocationId { get; set; } = Guid.Empty;
        public string ParcelId { get; set; } = string.Empty;
        public string ParcelDescription { get; set; } = string.Empty;
        public string ParcelType { get; set; } = string.Empty;
        public double Acres { get; set; } = 0.0;
        public double Latitude { get; set; } = 0.0;
        public double Longitude { get; set; } = 0.0;
    }

}
