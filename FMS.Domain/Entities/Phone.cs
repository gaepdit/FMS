using FMS.Domain.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Domain.Entities
{
    public class Phone
    {
        public Phone() { }
        public Phone(Guid id, PhoneCreateDto phoneCreateDto)
        {
            ContactId = id;
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
