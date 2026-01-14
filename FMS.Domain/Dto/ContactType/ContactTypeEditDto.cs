using FMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "Contact Type is required.")]
        [Display(Name = "Contact Type")]
        public string Name { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}
