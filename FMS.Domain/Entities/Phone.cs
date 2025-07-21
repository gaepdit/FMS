using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Entities
{
    public class Phone : BaseActiveModel
    {
        public Phone() { }
        public Phone(PhoneCreateDto phone)
        {
            Id = phone.Id;
            ContactId = phone.ContactId;
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

        public static Phone FormatNumber(Phone phone)
        {
            if (phone == null || string.IsNullOrWhiteSpace(phone.Number))
                return phone;
            // Format the phone number to a standard format, e.g., (123) 456-7890
            phone.Number = phone.Number.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "");
            if (phone.Number.Length == 10)
            {
                phone.Number = $"({phone.Number.Substring(0, 3)}) {phone.Number.Substring(3, 3)}-{phone.Number.Substring(6)}";
            }
            else if (phone.Number.Length == 11 && phone.Number.StartsWith('1'))
            {
                phone.Number = $"1 ({phone.Number.Substring(1, 3)}) {phone.Number.Substring(4, 3)}-{phone.Number.Substring(7)}";
            }
            return phone;
        }
    }
}
