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
            Number = phone.Number;
            Type = phone.Type;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        public string Number { get; set; }

        public PhoneType Type { get; set; }
    }
}
