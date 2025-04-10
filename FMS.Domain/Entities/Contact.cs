using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;
using System;

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
            Title = contact.Title;
            Type = contact.Type;
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
        public string Title { get; set; }
        public string Type { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
    }
}
