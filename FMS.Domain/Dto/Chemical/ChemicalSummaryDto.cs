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
            Active = chemical.Active;
            CasNo = chemical.CasNo;
            ChemicalName = chemical.ChemicalName;
            CommonName = chemical.CommonName;
            ToxValue = chemical.ToxValue;
            MCLs = chemical.MCLs;
            FinalRc = chemical.FinalRc;
            RQ = chemical.RQ;
        }

        public Guid Id { get; set; }

        [Display(Name = "CASNO")]
        public string CasNo { get; set; }

        [Display(Name = "Chemical Name")]
        public string ChemicalName { get; set; }

        [Display(Name = "Common Name")]
        public string CommonName { get; set; }

        [Display(Name = "Toxicity Value")]
        public string ToxValue { get; set; }

        [Display(Name = "MCL Value")]
        public string MCLs { get; set; }

        [Display(Name = "Final RC")]
        public string FinalRc { get; set; }

        [Display(Name = "RQ")]
        public string RQ { get; set; }

        public bool Active { get; set; }
    }
}
