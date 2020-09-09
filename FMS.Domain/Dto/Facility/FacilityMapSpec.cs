using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FacilityMapSpec
    {
        [Display(Name = "Facility Number")]
        public string? FacilityNumber { get; set; }

        [Display(Name = "Facility Name")]
        public string? Name { get; set; }

        [Display(Name = "Active Site")]
        public bool? Active { get; set; }

        [Display(Name = "Address")]
        public string? Address { get; set; }

        [Display(Name = "City")]
        public string? City { get; set; }

        [Display(Name = "State")]
        public string? State { get; set; }

        [Display(Name = "Zip Code")]
        public string? PostalCode { get; set; }

        [Display(Name = "Latitude")]
        public decimal? Latitude { get; set; }

        [Display(Name = "Longitude")]
        public decimal? Longitude { get; set; }

        [Display(Name = "Radius")]
        public string? Radius { get; set; }
        
        [Display(Name = "Distance")]
        public decimal? Distance { get; set; }
    }
}
