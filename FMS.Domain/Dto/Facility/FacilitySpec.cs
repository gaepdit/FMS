using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FacilitySpec
    {
        [Display(Name = "Facility Number")]
        public string FacilityNumber { get; set; }

        [Display(Name = "Facility Name")]
        public string Name { get; set; }

        [Display(Name = "Include deleted records")]
        public bool ShowDeleted { get; set; } 

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

        [Display(Name = "Street Address")]
        public string Address { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "ZIP Code")]
        [StringLength(10)]
        public string PostalCode { get; set; }

        public IDictionary<string, string> AsRouteValues =>
            new Dictionary<string, string>
            {
                {nameof(ShowDeleted), ShowDeleted.ToString()},
                {nameof(Address), Address},
                {nameof(BudgetCodeId), BudgetCodeId?.ToString()},
                {nameof(City), City},
                {nameof(ComplianceOfficerId), ComplianceOfficerId?.ToString()},
                {nameof(CountyId), CountyId?.ToString()},
                {nameof(EnvironmentalInterestId), EnvironmentalInterestId?.ToString()},
                {nameof(FacilityNumber), FacilityNumber},
                {nameof(FacilityStatusId), FacilityStatusId?.ToString()},
                {nameof(FacilityTypeId), FacilityTypeId?.ToString()},
                {nameof(FileLabel), FileLabel},
                {nameof(Location), Location},
                {nameof(Name), Name},
                {nameof(OrganizationalUnitId), OrganizationalUnitId?.ToString()},
                {nameof(PostalCode), PostalCode},
                {nameof(State), State},
            };
    }
}