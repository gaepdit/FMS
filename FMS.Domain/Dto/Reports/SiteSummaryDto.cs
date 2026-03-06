using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Domain.Dto
{
    public class SiteSummaryDto
    {
        public SiteSummaryDto() { }

        public SiteSummaryDto(Facility facility)
        {
            Id = facility.Id;
            FacilityNumber = facility.FacilityNumber;
            Name = facility.Name;
            Active = facility.Active;
            County = facility.County;
            FacilityStatus = facility.FacilityStatus;
            FacilityType = facility.FacilityType;
            BudgetCode = facility.BudgetCode;
            OrganizationalUnit = facility.OrganizationalUnit;
            ComplianceOfficer = facility.ComplianceOfficer;
            Location = facility.Location;
            Address = facility.Address;
            City = facility.City;
            State = facility.State;
            PostalCode = facility.PostalCode;
            Latitude = facility.Latitude;
            Longitude = facility.Longitude;
            HsrpFacilityPropertyDetails = facility.HsrpFacilityProperties;
            LocationDetails = facility.LocationDetails;
            Parcels = facility.Parcels?
                .Select(static p => new ParcelSummaryDto(p)).ToList() ??
                new List<ParcelSummaryDto>();
            Contacts = facility.Contacts?
                .Select(c => new ContactSummaryDto(c)).ToList() ?? new List<ContactSummaryDto>();
            ScoreDetails = facility.ScoreDetails;
            GroundwaterScoreDetails = facility.GroundwaterScoreDetails;
            OnsiteScoreDetails = facility.OnsiteScoreDetails;
            Substances = facility.Substances?
                .Select(s => new SubstanceSummaryDto(s)).ToList() ?? new List<SubstanceSummaryDto>();
            StatusDetails = facility.StatusDetails;
        }

        public Guid Id { get; }

        [Display(Name = "Facility Number")]
        public string FacilityNumber { get; }

        [Display(Name = "Facility Name")]
        public string Name { get; set; }

        [Display(Name = "Active Site")]
        public bool Active { get; }

        [Display(Name = "County")]
        public County County { get; set; }

        [Display(Name = "Facility Status")]
        public FacilityStatus FacilityStatus { get; }

        [Display(Name = "Type/Env. Interest")]
        public FacilityType FacilityType { get; }

        [Display(Name = "Budget Code")]
        public BudgetCode BudgetCode { get; }

        [Display(Name = "Organizational Unit")]
        public OrganizationalUnit OrganizationalUnit { get; }

        [Display(Name = "Compliance Officer")]
        public ComplianceOfficer ComplianceOfficer { get; }

        [Display(Name = "Location Description")]
        public string Location { get; }

        [Display(Name = "Street Address")]
        public string Address { get; }

        [Display(Name = "City")]
        public string City { get; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "ZIP Code")]
        public string PostalCode { get; }

        [Display(Name = "Latitude")]
        [DisplayFormat(DataFormatString = "{0:F6}")]
        public decimal Latitude { get; }

        [Display(Name = "Longitude")]
        [DisplayFormat(DataFormatString = "{0:F6}")]
        public decimal Longitude { get; }

        public HsrpFacilityProperties HsrpFacilityPropertyDetails { get; set; }

        [Display(Name = "Class")]
        public Location LocationDetails { get; set; }

        public List<ParcelSummaryDto> Parcels { get; }

        public List<ContactSummaryDto> Contacts { get; }

        public Score ScoreDetails { get; set; }

        public GroundwaterScore GroundwaterScoreDetails { get; set; }

        public OnsiteScore OnsiteScoreDetails { get; set; }

        public List<SubstanceSummaryDto> Substances { get; }

        public Status StatusDetails { get; set; }

    }
}
