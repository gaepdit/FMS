using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    // TODO #56 reconsider mandatory fields
    public class FacilityCreateDto
    {
        [Display(Name = "Facility Number"), Required]
        public string FacilityNumber { get; set; }

        [Display(Name = "Facility Name"), Required]
        public string Name { get; set; }

        [Display(Name = "County")]
        public int CountyId { get; set; }

        [Display(Name = "Facility Status"), Required]
        public Guid FacilityStatusId { get; set; }

        [Display(Name = "Facility Type"), Required]
        public Guid FacilityTypeId { get; set; }

        [Display(Name = "Budget Code"), Required]
        public Guid BudgetCodeId { get; set; }

        [Display(Name = "Organizational Unit"), Required]
        public Guid OrganizationalUnitId { get; set; }

        [Display(Name = "Environmental Interest")]
        public Guid EnvironmentalInterestId { get; set; }

        [Display(Name = "Compliance Officer"), Required]
        public Guid ComplianceOfficerId { get; set; }

        [Display(Name = "File Label")]
        public Guid? FileId { get; set; }

        [Display(Name = "Location Description")]
        public string Location { get; set; }

        [Display(Name = "Street Address"), Required]
        public string Address { get; set; }

        [Display(Name = "City"), Required]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "Zip Code"), Required]
        public string PostalCode { get; set; }

        [Required]
        [Display(Name = "Latitude")]
        [DisplayFormat(DataFormatString = "{0:F6}", ApplyFormatInEditMode = true)]
        public decimal? Latitude { get; set; }

        [Required]
        [Display(Name = "Longitude")]
        [DisplayFormat(DataFormatString = "{0:F6}", ApplyFormatInEditMode = true)]
        public decimal? Longitude { get; set; }
    }
}