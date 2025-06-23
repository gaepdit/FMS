using System;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class PhoneSummaryDto
    {
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

        public string Number { get; set; }

        public string PhoneType { get; set; }
    }
}
