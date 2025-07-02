using System;
using System.ComponentModel.DataAnnotations;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class ContactTypeEditDto
    {
        public ContactTypeEditDto() { }

        public ContactTypeEditDto(ContactType contactType)
        {
            Id = contactType.Id;
            Name = contactType.Name;
            Active = contactType.Active;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        [Required(ErrorMessage = "Contact Title is required.")]
        [Display(Name = "Contact Type")]
        public string Name { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}
