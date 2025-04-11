using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;
using System;

namespace FMS.Domain.Entities
{
    public class Phone : BaseActiveModel
    {
        public Phone() { }
        public Phone(Guid contactId, PhoneCreateDto phone)
        {
            ContactId = contactId;
            CountryCode = phone.CountryCode;
            AreaCode = phone.AreaCode;
            Prefix = phone.Prefix;
            Number = phone.Number;
            Type = phone.Type;
        }
        public Guid ContactId { get; set; }

        public int CountryCode { get; set; }

        public int AreaCode { get; set; }

        public int Prefix { get; set; }

        public int Number { get; set; }

        public string Type { get; set; }
    }
}
