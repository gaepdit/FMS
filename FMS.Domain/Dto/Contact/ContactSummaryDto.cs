using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            Phones = contact.Phones ?
                .Select(p => new PhoneSummaryDto(p)).ToList() ?? new List<PhoneSummaryDto>();
        }
        public Guid Id { get; set; }

        [Display(Name = "Active")]
        public bool Active { get; set; }

        public Guid FacilityId { get; set; }

        [Display(Name = "First Name")]
        public string GivenName { get; set; }

        [Display(Name = "Last Name")]
        public string FamilyName { get; set; }

        [Display(Name = "Title")]
        public ContactTitle ContactTitle { get; set; }

        [Display(Name = "Type")]
        public ContactType ContactType { get; set; }

        [Display(Name = "Company")]
        public string Company { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        public List<PhoneSummaryDto> Phones { get; set; }

        public string GetMailTo()
        {
            return string.Concat("mailto:", Email);
        }
    }
}
