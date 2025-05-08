using System;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class ContactSummaryDto
    {
        public ContactSummaryDto(Contact contact)
        {
            Id = contact.Id;
            Active = contact.Active;
            FacilityId = contact.FacilityId;
            GivenName = contact.GivenName;
            FamilyName = contact.FamilyName;
            ContactTitleId = contact.ContactTitleId;
            ContactTypeId = contact.ContactTypeId;
            Company = contact.Company;
            Address = contact.Address;
            City = contact.City;
            State = contact.State;
            PostalCode = contact.PostalCode;
            Email = contact.Email;
            Status = contact.Status;
        }
        public Guid Id { get; set; }

        public bool Active { get; set; }

        public Guid FacilityId { get; set; }

        public string GivenName { get; set; }

        public string FamilyName { get; set; }

        public Guid ContactTitleId { get; set; }

        public Guid ContactTypeId { get; set; }

        public string Company { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Email { get; set; }

        public string Status { get; set; }
    }
}
