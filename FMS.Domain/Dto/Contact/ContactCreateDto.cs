using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class ContactCreateDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public bool Active { get; set; } = true;

        public Guid FacilityId { get; set; }

        [Display(Name = "First Name")]
        public string GivenName { get; set; }

        [Display(Name = "Last Name")]
        public string FamilyName { get; set; }

        [Display(Name = "Contact Title")]
        public Guid? ContactTitleId { get; set; }

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
        public string PostalCode { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        public string GetMailTo()
        {
            return string.Concat("mailto:", Email);
        }
    }
}
