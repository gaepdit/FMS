using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FacilityCreateDto
    {
        [Required]
        [Display(Name = "Facility Number")]
        public string FacilityNumber { get; set; }

        [Required]
        [Display(Name = "Facility Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "County")]
        public int CountyId { get; set; }

        [Required]
        [Display(Name = "Facility Status")]
        public Guid FacilityStatusId { get; set; }

        [Required]
        [Display(Name = "Facility Type")]
        public Guid FacilityTypeId { get; set; }

        [Required]
        [Display(Name = "Budget Code")]
        public Guid BudgetCodeId { get; set; }

        [Required]
        [Display(Name = "Organizational Unit")]
        public Guid OrganizationalUnitId { get; set; }

        [Display(Name = "Environmental Interest")]
        public Guid? EnvironmentalInterestId { get; set; }

        [Required]
        [Display(Name = "Compliance Officer")]
        public Guid ComplianceOfficerId { get; set; }

        [Display(Name = "File Label")]
        public string FileLabel { get; set; }

        [Display(Name = "Location Description")]
        public string Location { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Street Address")]
        public string Address { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "ZIP Code")]
        public string PostalCode { get; set; }

        [Required]
        [Display(Name = "Latitude")]
        [DisplayFormat(DataFormatString = "{0:F6}", ApplyFormatInEditMode = true)]
        public decimal? Latitude { get; set; }

        [Required]
        [Display(Name = "Longitude")]
        [DisplayFormat(DataFormatString = "{0:F6}", ApplyFormatInEditMode = true)]
        public decimal? Longitude { get; set; }

        public void TrimAll()
        {
            FacilityNumber = FacilityNumber?.Trim();
            Name = Name?.Trim();
            FileLabel = FileLabel?.Trim();
            Location = Location?.Trim();
            Address = Address?.Trim();
            City = City?.Trim();
            PostalCode = PostalCode?.Trim();
        }
    }
}