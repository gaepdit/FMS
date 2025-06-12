using System;
using System.ComponentModel.DataAnnotations;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class ChemicalEditDto
    {
        public ChemicalEditDto() { }

        public ChemicalEditDto(Chemical chemical)
        {
            Id = chemical.Id;
            Active = chemical.Active;
            CasNo = chemical.CasNo;
            ChemicalName = chemical.ChemicalName;
            CommonName = chemical.CommonName;
            ToxValue = chemical.ToxValue;
            MCLs = chemical.MCLs;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        [Display(Name = "CASNO")]
        [Required(ErrorMessage = "CASNO is required.")]
        public string CasNo { get; set; }

        [Display(Name = "Chemical Name")]
        [Required(ErrorMessage = "Chemical Name is required.")]
        public string ChemicalName { get; set; }

        [Display(Name = "Common Name")]
        public string CommonName { get; set; }

        [Display(Name = "Toxicity Value")]
        public string ToxValue { get; set; }

        [Display(Name = "MCL Value")]
        public string MCLs { get; set; }
    }
}
