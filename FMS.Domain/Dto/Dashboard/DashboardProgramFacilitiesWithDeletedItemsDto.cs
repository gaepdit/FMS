using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class DashboardProgramFacilitiesWithDeletedItemsDto
    {
        public DashboardProgramFacilitiesWithDeletedItemsDto(Facility facility, bool inactiveContacts = false, bool inactiveEvents = false)
        {
            Id = facility.Id;
            Name = facility.Name;
            FacilityNumber = facility.FacilityNumber;
            FacilityType = facility.FacilityType;
            FacilityStatus = facility.FacilityStatus;
            OrganizationalUnit = facility.OrganizationalUnit;
            ComplianceOfficer = facility.ComplianceOfficer;
            InactiveContacts = inactiveContacts;
            InactiveEvents = inactiveEvents;
            Active = facility.Active;
            Contacts = facility.Contacts;
            Events = facility.Events;
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

        [Display(Name = "Inactive Contacts")]
        public bool InactiveContacts { get; set; }

        [Display(Name = "Inactive Events")]
        public bool InactiveEvents { get; set; }

        public bool Active { get; set; }

        public ICollection<Contact> Contacts { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}
