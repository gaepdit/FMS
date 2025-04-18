﻿using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class ContactEditDto
    {
        public ContactEditDto() { }
        public ContactEditDto(Contact contact)
        {
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
        [Display(Name = "First Name")]
        public string GivenName { get; set; }

        [Display(Name = "Last Name")]
        public string FamilyName { get; set; }

        [Display(Name = "Title")]
        public Guid ContactTitleId { get; set; }

        [Display(Name = "Contact Type")]
        public Guid ContactTypeId { get; set; }

        [Display(Name = "Company")]
        public string Company { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "ZIP Code")]
        public string PostalCode { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }
    }
}
