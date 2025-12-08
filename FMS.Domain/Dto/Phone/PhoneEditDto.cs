using System;
using System.ComponentModel.DataAnnotations;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class PhoneEditDto
    {
        public PhoneEditDto() { }

        public PhoneEditDto(Phone phone)
        {
            Id = phone.Id;
            Active = phone.Active;
            ContactId = phone.ContactId;
            Number = phone.Number;
            PhoneType = phone.PhoneType;
        }
        public Guid Id { get; set; }

        public bool Active { get; set; }

        public Guid ContactId { get; set; }

        [Display(Name = "Phone Number")]
        public string Number { get; set; }

        [Display(Name = "Phone Type")]
        public string PhoneType { get; set; }

        public static PhoneEditDto FormatNumber(PhoneEditDto phone)
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
