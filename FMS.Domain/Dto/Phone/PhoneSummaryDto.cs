using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class PhoneSummaryDto
    {
        public PhoneSummaryDto() { }

        public PhoneSummaryDto(PhoneSummaryDto phone)
        {
            Id = phone.Id;
            Active = phone.Active;
            ContactId = phone.ContactId;
            Number = phone.Number;
            PhoneType = phone.PhoneType;
        }

        public PhoneSummaryDto(Phone phone)
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

        public string GetTelTo()
        {
            return string.Concat("tel:", Number.Replace(" ", "").Replace("-", "").Replace("(","").Replace(")",""));
        }
    }
}
