﻿using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FacilityEditDto
    {
        public FacilityEditDto() { }
        public FacilityEditDto(FacilityDetailDto facility)
        {
            FileLabel = facility.FileLabel;
            FacilityNumber = facility.FacilityNumber;
            Name = facility.Name;
            Active = facility.Active;
            CountyId = facility.County.Id;
            FacilityStatusId = facility.FacilityStatus?.Id;
            FacilityTypeId = facility.FacilityType?.Id;
            BudgetCodeId = facility.BudgetCode?.Id;
            OrganizationalUnitId = facility.OrganizationalUnit?.Id;
            ComplianceOfficerId = facility.ComplianceOfficer?.Id;
            Location = facility.Location;
            Address = facility.Address;
            City = facility.City;
            State = facility.State;
            PostalCode = facility.PostalCode;
            Latitude = facility.Latitude;
            Longitude = facility.Longitude;
            HSInumber = facility.HSInumber;
            NonHSILetterDate = facility.NonHSILetterDate;
            Comments = facility.Comments;
            PreRQSMcleanup = facility.PreRQSMcleanup;
            ImageChecked = facility.ImageChecked;
            DeferredOnSiteScoring = facility.DeferredOnSiteScoring;
            AdditionalDataRequested = facility.AdditionalDataRequested;
            VRPReferral = facility.VRPReferral;
            HasERecord = facility.HasERecord;
            IsRetained = facility.IsRetained;
        }

        [Required]
        [Display(Name = "Facility Number")]
        public string FacilityNumber { get; set; }

        [Required]
        [Display(Name = "Facility Name")]
        public string Name { get; set; }

        [Display(Name = "Active Site")]
        public bool Active { get; set; }

        [Required]
        [Display(Name = "County")]
        public int CountyId { get; set; }

        [Required]
        [Display(Name = "Facility Status")]
        public Guid? FacilityStatusId { get; set; }

        [Required]
        [Display(Name = "Type/Environmental Interest")]
        public Guid? FacilityTypeId { get; set; }

        [Required]
        [Display(Name = "Budget Code")]
        public Guid? BudgetCodeId { get; set; }

        [Required]
        [Display(Name = "Organizational Unit")]
        public Guid? OrganizationalUnitId { get; set; }

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
        public string PostalCode { get; set; }

        [Required]
        [Display(Name = "Latitude")]
        [DisplayFormat(DataFormatString = "{0:F6}", ApplyFormatInEditMode = true)]
        public decimal Latitude { get; set; }

        [Required]
        [Display(Name = "Longitude")]
        [DisplayFormat(DataFormatString = "{0:F6}", ApplyFormatInEditMode = true)]
        public decimal Longitude { get; set; }

        [Display(Name = "Is Retained Onsite")]
        public bool IsRetained { get; set; }

        // The following properties only apply to Release Notifications
        [Display(Name = "HSI Number")]
        public string HSInumber { get; set; }

        [Display(Name = "Determination Letter Date")]
        public DateOnly NonHSILetterDate { get; set; }

        [Display(Name = "Comments")]
        public string Comments { get; set; }

        [Display(Name = "Pre-RQSM Cleanup")]
        public bool PreRQSMcleanup { get; set; }

        [Display(Name = "Image Checked")]
        public bool ImageChecked { get; set; }

        [Display(Name = "Deferred OnSite Scoring")]
        public bool DeferredOnSiteScoring { get; set; }

        [Display(Name = "Additional Data Requested")]
        public bool AdditionalDataRequested { get; set; }

        [Display(Name = "VRP Referral")]
        public bool VRPReferral { get; set; }

        [Display(Name = "Has Electronic Records")]
        public bool HasERecord { get; set; }

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