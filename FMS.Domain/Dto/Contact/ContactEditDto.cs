using FMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class ContactEditDto
    {
        public ContactEditDto() { }
        public ContactEditDto(Contact contact)
        {
            Id = contact.Id;
            Active = contact.Active;
            FacilityId = contact.FacilityId;
            GivenName = contact.GivenName;
            FamilyName = contact.FamilyName;
            ContactTitle = contact.ContactTitle;
            ContactTypeId = contact.ContactTypeId;
            Company = contact.Company;
            Address = contact.Address;
            City = contact.City;
            State = contact.State;
            PostalCode = contact.PostalCode;
            Email = contact.Email;
        }

        public Guid Id { get; set; }

        [Display(Name = "Active")]
        public bool Active { get; set; }

        public Guid FacilityId { get; set; }

        [Display(Name = "First Name")]
        public string GivenName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string FamilyName { get; set; }

        [Display(Name = "Title")]
        public string ContactTitle { get; set; }

        [Display(Name = "Contact Type")]
        public Guid? ContactTypeId { get; set; }

        [Display(Name = "Company")]
        public string Company { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "ZIP Code")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Invalid Zip Code format. Please enter a 5-digit number.")]
        public string PostalCode { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }
    }
}
