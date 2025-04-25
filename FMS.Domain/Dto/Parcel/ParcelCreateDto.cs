using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class ParcelCreateDto
    {
        public Guid LocationId { get; set; }

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
        [DisplayFormat(DataFormatString = "{0:F6}", ApplyFormatInEditMode = true)]
        public double Latitude { get; set; }

        [Display(Name = "Longitude")]
        [DisplayFormat(DataFormatString = "{0:F6}", ApplyFormatInEditMode = true)]
        public double Longitude { get; set; }

        public void TrimAll()
        {
            ParcelNumber = ParcelNumber?.Trim();
            ParcelDescription = ParcelDescription?.Trim();
        }
    }
}
