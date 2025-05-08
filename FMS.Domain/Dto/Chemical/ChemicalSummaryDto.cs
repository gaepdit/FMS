using System;
using System.ComponentModel.DataAnnotations;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class ChemicalSummaryDto
    {
        public ChemicalSummaryDto(Chemical chemical)
        {
            Id = chemical.Id;
            CasNo = chemical.CasNo;
            ChemicalName = chemical.ChemicalName;
            CommonName = chemical.CommonName;
            ToxValue = chemical.ToxValue;
            MCLs = chemical.MCLs;
        }

        public Guid Id { get; set; }

        [Display(Name = "CASNO")]
        public string CasNo { get; set; }

        [Display(Name = "Chemical Name")]
        public string ChemicalName { get; set; }

        [Display(Name = "Common Name")]
        public string CommonName { get; set; }

        public string ToxValue { get; set; }

        public string MCLs { get; set; }
    }
}
