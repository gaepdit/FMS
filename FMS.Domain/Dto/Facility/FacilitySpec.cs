using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace FMS.Domain.Dto
{
    public class FacilitySpec
    {
        public FacilitySort SortBy { get; set; } = FacilitySort.Name;

        public bool FirstPass { get; set; } = true;

        [Display(Name = "Facility Number")]
        public string FacilityNumber { get; set; }

        [Display(Name = "Facility Name")]
        public string Name { get; set; }

        [Display(Name = "Include deleted records")]
        public bool ShowDeleted { get; set; }

        [Display(Name = "Show Pending Only")]
        public bool ShowPendingOnly { get; set; }

        [Display(Name = "County")]
        public int? CountyId { get; set; }

        [Display(Name = "Facility Status")]
        public Guid? FacilityStatusId { get; set; }

        [Display(Name = "Type/Environmental Interest")]
        public List<Guid> FacilityTypeId { get; set; }

        [Display(Name = "Budget Code")]
        public Guid? BudgetCodeId { get; set; }

        [Display(Name = "Organizational Unit")]
        public Guid? OrganizationalUnitId { get; set; }

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
        public string PostalCode { get; set; }

        //Advanced Properties for search
        [Display(Name = "Class")]
        public Guid? LocationClassId { get; set; }

        [Display(Name = "Add'l Org. Unit")]
        public Guid? AdditionalOrgUnitId { get; set; }

        [Display(Name = "UEC")]
        public bool UEC { get; set; }

        [Display(Name = "Liens")]
        public bool Liens { get; set; }

        [Display(Name = "Financial Assurance")]
        public bool FinancialAssurance { get; set; }

        [Display(Name = "Landfills")]
        public bool Landfills { get; set; }

        [Display(Name = "ISWQS")]
        public bool ISWQS { get; set; }
        
        public IDictionary<string, string> AsRouteValues =>
            new Dictionary<string, string>
            {
                {nameof(SortBy), SortBy.ToString()},
                {nameof(ShowDeleted), ShowDeleted.ToString()},
                {nameof(Address), Address},
                {nameof(BudgetCodeId), BudgetCodeId?.ToString()},
                {nameof(City), City},
                {nameof(ComplianceOfficerId), ComplianceOfficerId?.ToString()},
                {nameof(CountyId), CountyId?.ToString()},
                {nameof(FacilityNumber), FacilityNumber},
                {nameof(FacilityStatusId), FacilityStatusId?.ToString()},
                {nameof(FacilityTypeId), FacilityTypeId != null ? JsonSerializer.Serialize(FacilityTypeId) : null},
                {nameof(FileLabel), FileLabel},
                {nameof(Location), Location},
                {nameof(Name), Name},
                {nameof(OrganizationalUnitId), OrganizationalUnitId?.ToString()},
                {nameof(PostalCode), PostalCode},
                {nameof(State), State},
                {nameof(ShowPendingOnly), ShowPendingOnly.ToString()},
                {nameof(FirstPass), FirstPass.ToString()},
                {nameof(LocationClassId), LocationClassId?.ToString()},
                {nameof(AdditionalOrgUnitId), AdditionalOrgUnitId?.ToString()},
                {nameof(Liens), Liens.ToString()},
                {nameof(FinancialAssurance), FinancialAssurance.ToString()},
                {nameof(Landfills), Landfills.ToString()},
                {nameof(ISWQS), ISWQS.ToString()}
            };
        //JsonSerializer.Serialize(FacilityTypeId)
        public void TrimAll()
        {
            FileLabel = FileLabel?.Trim();
            FacilityNumber = FacilityNumber?.Trim();
            Name = Name?.Trim();
            Location = Location?.Trim();
            Address = Address?.Trim();
            City = City?.Trim();
            PostalCode = PostalCode?.Trim();
        }
    }
}