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
            CountryCode = phone.CountryCode;
            AreaCode = phone.AreaCode;
            Prefix = phone.Prefix;
            Number = phone.Number;
            Type = phone.Type;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        public int CountryCode { get; set; }

        public int AreaCode { get; set; }

        public int Prefix { get; set; }

        public int Number { get; set; }

        public string Type { get; set; }
    }
}
