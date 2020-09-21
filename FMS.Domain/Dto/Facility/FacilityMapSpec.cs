using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FacilityMapSpec
    {
        [Display(Name = "Facility Number")]
        public string FacilityNumber { get; set; }

        [Display(Name = "Facility Name")]
        public string Name { get; set; }

        [Display(Name = "Show Active Sites Only")]
        public bool ActiveOnly { get; set; } = true;

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; } = "Georgia";

        [Display(Name = "Zip Code")]
        public string PostalCode { get; set; }

        [Display(Name = "Latitude")]
        public decimal? Latitude { get; set; }

        [Display(Name = "Longitude")]
        public decimal? Longitude { get; set; }

        [Display(Name = "Radius")]
        public string Radius { get; set; }

        [Display(Name = "Distance")]
        public decimal? Distance { get; set; }

        [Display(Name = "Facility Status")]
        public string FacilityStatus { get; set; }

        [Display(Name = "Facility Type")]
        public string FacilityType { get; set; }

        [Display(Name = "File Label")]
        public string FileLabel { get; set; }

        [Display(Name = "Output")]
        public string Output { get; set; }
        public string GeocodeLat { get; set; }
        public string GeocodeLng { get; set; }
    }
}
