using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class ParcelCreateDto
    {
        public Guid LocationId { get; set; }

        [Display(Name = "Parcel ID")]
        public string ParcelId { get; set; }

        [Display(Name = "Description")]
        public string ParcelDescription { get; set; }

        [Display(Name = "Type")]
        public string ParcelType { get; set; }

        [Display(Name = "Acreage")]
        public double Acres { get; set; }

        [Display(Name = "Latitude")]
        [DisplayFormat(DataFormatString = "{0:F6}", ApplyFormatInEditMode = true)]
        public double Latitude { get; set; }

        [Display(Name = "Longitude")]
        [DisplayFormat(DataFormatString = "{0:F6}", ApplyFormatInEditMode = true)]
        public double Longitude { get; set; }

        public void TrimAll()
        {
            ParcelId = ParcelId?.Trim();
            ParcelDescription = ParcelDescription?.Trim();
        }
    }
}
