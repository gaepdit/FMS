using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace FMS.Domain.Dto
{
    public class FacilitySpec
    {
        public FacilitySort SortBy { get; set; } = FacilitySort.Name;

        [Display(Name = "Facility Number")]
        public string FacilityNumber { get; set; }

        [Display(Name = "Facility Name")]
        public string Name { get; set; }

        [Display(Name = "Include deleted records")]
        public bool ShowDeleted { get; set; }

        [Display(Name = "Show Only Pending Notifications")]
        public bool ShowPendingOnly { get; set; }

        [Display(Name = "County")]
        public int? CountyId { get; set; }

        [Display(Name = "Facility Status")]
        public Guid? FacilityStatusId { get; set; }

        [Display(Name = "Type/Environmental Interest")]
        public Guid? FacilityTypeId { get; set; }

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
        [StringLength(10)]
        public string PostalCode { get; set; }

        // The following properties only apply to Release Notifications
        [Display(Name = "HSI Number")]
        public string HSInumber { get; set; }

        [Display(Name = "Determination Letter Date")]
        public DateOnly DeterminationLetterDate { get; set; }

        [Display(Name = "Comments")]
        public string Comments { get; set; }

        [Display(Name = "Pre-RQSM Cleanup")]
        public bool PreRQSMcleanup { get; set; }

        [Display(Name = "Image Checked")]
        public bool ImageChecked { get; set; }

        [Display(Name = "Brownfield Deferral")]
        public bool DeferredOnSiteScoring { get; set; }

        [Display(Name = "Additional Data Requested")]
        public bool AdditionalDataRequested { get; set; }

        [Display(Name = "VRP Deferral")]
        public bool VRPReferral { get; set; }

        [Display(Name = "Date Received")]
        public DateOnly RNDateReceived { get; set; }

        [Display(Name = "Historical Unit")]
        public string HistoricalUnit { get; set; }

        [Display(Name = "Historical C.O.")]
        public string HistoricalComplianceOfficer { get; set; }

        [Display(Name = "Has Electronic Records")]
        public bool HasERecord { get; set; }

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
                {nameof(FacilityTypeId), FacilityTypeId?.ToString()},
                {nameof(FileLabel), FileLabel},
                {nameof(Location), Location},
                {nameof(Name), Name},
                {nameof(OrganizationalUnitId), OrganizationalUnitId?.ToString()},
                {nameof(PostalCode), PostalCode},
                {nameof(State), State},
                {nameof(ShowPendingOnly), ShowPendingOnly.ToString()},
            };
    }
}