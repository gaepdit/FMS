using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;
using System;
using System.Collections;
using System.Collections.Generic;

namespace FMS.Domain.Entities
{
    public class Contact : BaseActiveModel
    {
        public Contact() { }
        public Contact(Guid id, ContactCreateDto contact)
        {
            FacilityId = id;
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
        public Guid FacilityId { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public Guid ContactTitleId { get; set; }
        public ContactTitle ContactTitle { get; set; }
        public Guid ContactTypeId { get; set; }
        public ContactType ContactType { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public ICollection<Phone> Phones { get; set; } = new List<Phone>();

        public string GetMailTo()
        {
            return string.Concat("mailto:", Email);
        }
    }
}
