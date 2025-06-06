using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Entities
{
    public class Phone : BaseActiveModel
    {
        public Phone() { }
        public Phone(Guid contactId, PhoneCreateDto phone)
        {
            ContactId = contactId;
            Number = phone.Number;
            PhoneType = phone.PhoneType;
        }
        public Guid ContactId { get; set; }

        [Display(Name = "Number")]
        [Required(ErrorMessage = "Phone number is required")]
        [StringLength(15, ErrorMessage = "Phone number cannot exceed 15 characters")]
        [RegularExpression(@"^\+?[0-9\s\-()]+$", ErrorMessage = "Invalid phone number format. Only digits, spaces, dashes, parentheses, and an optional leading '+' are allowed.")]
        [DataType(DataType.PhoneNumber)]
        public string Number { get; set; }

        public string PhoneType { get; set; }
    }
}
