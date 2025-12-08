using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FacilityCreateDto
    {
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
        public string FacilityStatusName { get; set; }

        [Required]
        [Display(Name = "Type/Environmental Interest")]
        public Guid FacilityTypeId { get; set; }
        public string FacilityTypeName { get; set; }

        [Required]
        [Display(Name = "Budget Code")]
        public Guid BudgetCodeId { get; set; }

        [Required]
        [Display(Name = "Organizational Unit")]
        public Guid OrganizationalUnitId { get; set; }

        [Display(Name = "Compliance Officer")]
        public Guid? ComplianceOfficerId { get; set; }

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
        [DataType(DataType.PostalCode)]
        public string PostalCode { get; set; }

        [Required]
        [Display(Name = "Latitude")]
        [DisplayFormat(DataFormatString = "{0:F6}", ApplyFormatInEditMode = true)]
        public decimal? Latitude { get; set; }

        [Required]
        [Display(Name = "Longitude")]
        [DisplayFormat(DataFormatString = "{0:F6}", ApplyFormatInEditMode = true)]
        public decimal? Longitude { get; set; }

        [Display(Name = "HSI Number")]
        public string HSInumber { get; set; }

        [Display(Name = "Determination Letter Date")]
        public DateOnly? DeterminationLetterDate { get; set; }

        [Display(Name = "General Comments for this Facility")]
        public string Comments { get; set; }

        [Display(Name = "Pre-RQSM Cleanup")]
        public bool PreRQSMcleanup { get; set; }

        [Display(Name = "Image Checked")]
        public bool ImageChecked { get; set; }

        [Display(Name = "Brownfield Deferral")]
        public bool DeferredOnSiteScoring { get; set; }

        [Display(Name = "Add'l Data Requested")]
        public bool AdditionalDataRequested { get; set; }

        [Display(Name = "VRP Deferral")]
        public bool VRPReferral { get; set; }

        [Display(Name = "Date Received")]
        public DateOnly? RNDateReceived { get; set; }

        [Display(Name = "Historical Unit")]
        public string HistoricalUnit { get; set; }

        [Display(Name = "Historical C.O.")]
        public string HistoricalComplianceOfficer { get; set; }

        [Display(Name = "Has Electronic Records")]
        public bool HasERecord { get; set; }

        [Display(Name = "Is Retained Onsite")]
        public bool IsRetained { get; set; }

        public void TrimAll()
        {
            FacilityNumber = FacilityNumber?.Trim();
            Name = Name?.Trim();
            FileLabel = FileLabel?.Trim();
            Location = Location?.Trim();
            Address = Address?.Trim();
            City = City?.Trim();
            PostalCode = PostalCode?.Trim();
            HSInumber = HSInumber?.Trim();
            HistoricalUnit = HistoricalUnit?.Trim();
        }
    }
}