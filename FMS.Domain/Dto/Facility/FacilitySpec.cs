using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FacilitySpec
    {
        [Display(Name = "Facility Number")]
        public string FacilityNumber { get; set; }

        [Display(Name = "Facility Name")]
        public string Name { get; set; }

        [Display(Name = "Show Active Sites Only")]
        public bool ActiveOnly { get; set; } = true;

        [Display(Name = "County")]
        public int? CountyId { get; set; }

        [Display(Name = "Facility Status")]
        public Guid? FacilityStatusId { get; set; }

        [Display(Name = "Facility Type")]
        public Guid? FacilityTypeId { get; set; }

        [Display(Name = "Budget Code")]
        public Guid? BudgetCodeId { get; set; }

        [Display(Name = "Organizational Unit")]
        public Guid? OrganizationalUnitId { get; set; }

        [Display(Name = "Environmental Interest")]
        public Guid? EnvironmentalInterestId { get; set; }

        [Display(Name = "Compliance Officer")]
        public Guid? ComplianceOfficerId { get; set; }

        [Display(Name = "File Label")]
        public string FileLabel { get; set; }

        [Display(Name = "Location Description")]
        public string Location { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "Zip Code")]
        public string PostalCode { get; set; }

    }
}