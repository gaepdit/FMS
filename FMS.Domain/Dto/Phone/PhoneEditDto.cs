using System;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class PhoneEditDto
    {
        public PhoneEditDto() { }

        public PhoneEditDto(Guid contactId, Phone phone)
        {
            Id = phone.Id;
            Active = phone.Active;
            ContactId = contactId;
            CountryCode = phone.CountryCode;
            AreaCode = phone.AreaCode;
            Prefix = phone.Prefix;
            Number = phone.Number;
            Type = phone.Type;
        }
        public Guid Id { get; set; }

        public bool Active { get; set; }

        public Guid ContactId { get; set; }

        public int CountryCode { get; set; }

        public int AreaCode { get; set; }

        public int Prefix { get; set; }

        public int Number { get; set; }

        public string Type { get; set; }
    }
}
