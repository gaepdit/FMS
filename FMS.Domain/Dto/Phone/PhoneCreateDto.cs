using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class PhoneCreateDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public bool Active { get; set; } = true;

        public Guid ContactId { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"(\D*\d\D*){10}", ErrorMessage = "Invalid Phone Number format. Please enter a 3-digit area code followed by a 7-digit number.")]
        public string Number { get; set; }

        [Display(Name = "Phone Type")]
        public string PhoneType { get; set; }

        public static PhoneCreateDto FormatNumber(PhoneCreateDto phone)
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
