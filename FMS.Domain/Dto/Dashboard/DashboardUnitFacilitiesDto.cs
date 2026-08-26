using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class DashboardUnitFacilitiesDto
    {
        public DashboardUnitFacilitiesDto(Facility facility)
        {
            Id = facility.Id;
            Name = facility.Name;
            FacilityNumber = facility.FacilityNumber;
            FacilityType = facility.FacilityType;
            FacilityStatus = facility.FacilityStatus;
            OrganizationalUnit = facility.OrganizationalUnit;
            ComplianceOfficer = facility.ComplianceOfficer;
            County = facility.County;
            LocationClass = facility.LocationDetails?.LocationClass;
            AddlOrgUnit = facility.HsrpFacilityProperties?.OrganizationalUnit;
            Active = facility.Active;
        }
        public Guid Id { get; set; }

        [Display(Name = "Facility Number")]
        public string FacilityNumber { get; }

        [Display(Name = "Facility Name")]
        public string Name { get; }

        [Display(Name = "Facility Type")]
        public FacilityType FacilityType { get; set; }

        [Display(Name = "Facility Status")]
        public FacilityStatus FacilityStatus { get; set; }

        [Display(Name = "Organizational Unit")]
        public OrganizationalUnit OrganizationalUnit { get; set; }

        [Display(Name = "Compliance Officer")]
        public ComplianceOfficer ComplianceOfficer { get; set; }

        [Display(Name = "County")]
        public County County { get; set; }

        [Display(Name = "Location Class")]
        public LocationClass LocationClass { get; set; }

        [Display(Name = "Add'l Org Unit")]
        public OrganizationalUnit AddlOrgUnit { get; set; }

        public bool Active { get; set; }
    }
}
