using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FacilityMapSpec
    {
        public FacilityMapSpec() { }
        
        public FacilityMapSpec(FacilityDetailDto facility)
        {
            Latitude = facility.Latitude;
            Longitude = facility.Longitude;
            Radius = 3m;
        }

        [Display(Name = "Include deleted records")]
        public bool ShowDeleted { get; set; }

        [Display(Name = "Type/Environmental Interest")]
        public Guid? FacilityTypeId { get; set; }

        [Display(Name = "Street Address")]
        public string Address { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; } = "Georgia";

        [Display(Name = "ZIP Code")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Invalid Zip Code format. Please enter a 5-digit number.")]
        public string PostalCode { get; set; }

        [Display(Name = "Latitude")]
        [DisplayFormat(DataFormatString = "{0:0.000000}", ApplyFormatInEditMode = true)]
        public decimal? Latitude { get; set; }

        [Display(Name = "Longitude")]
        [DisplayFormat(DataFormatString = "{0:0.000000}", ApplyFormatInEditMode = true)]
        public decimal? Longitude { get; set; }

        [Display(Name = "Radius")]
        public decimal Radius { get; set; }

        [Display(Name = "Output")]
        public string Output { get; set; }

        // Internal geocode results
        public string GeocodeLat { get; set; }
        public string GeocodeLng { get; set; }
        public string GeocodeAddress { get; set; }
    }
}