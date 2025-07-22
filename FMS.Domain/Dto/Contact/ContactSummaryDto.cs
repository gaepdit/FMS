using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

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
            ContactTitle = contact.ContactTitle;
            ContactType = contact.ContactType;
            Company = contact.Company;
            Address = contact.Address;
            City = contact.City;
            State = contact.State;
            PostalCode = contact.PostalCode;
            Email = contact.Email;
            Status = contact.Status;
            Phones = contact.Phones ?
                .Select(p => new PhoneSummaryDto(p)).ToList() ?? new List<PhoneSummaryDto>();
        }
        public Guid Id { get; set; }

        public bool Active { get; set; }

        public Guid FacilityId { get; set; }

        public string GivenName { get; set; }

        public string FamilyName { get; set; }

        public ContactTitle ContactTitle { get; set; }

        public ContactType ContactType { get; set; }

        public string Company { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Email { get; set; }

        public string Status { get; set; }

        public List<PhoneSummaryDto> Phones { get; set; }

        public string GetMailTo()
        {
            return string.Concat("mailto:", Email);
        }
    }
}
