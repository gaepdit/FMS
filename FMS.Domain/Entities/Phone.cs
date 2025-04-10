using FMS.Domain.Dto;
using System;

namespace FMS.Domain.Entities
{
    public class Phone
    {
        public Phone() { }
        public Phone(Guid contactId, PhoneCreateDto phoneCreateDto)
        {
            ContactId = contactId;
            CountryCode = phoneCreateDto.CountryCode;
            AreaCode = phoneCreateDto.AreaCode;
            Prefix = phoneCreateDto.Prefix;
            Number = phoneCreateDto.Number;
            Type = phoneCreateDto.Type;
        }
        public Guid ContactId { get; set; }

        public int CountryCode { get; set; }

        public int AreaCode { get; set; }

        public int Prefix { get; set; }

        public int Number { get; set; }

        public string Type { get; set; }
    }
}
