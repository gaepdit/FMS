using FMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FileCabinetSpec
    {
        [StringLength(5)]
        [Display(Name = "File Cabinet Number")]
        public string Name { get; set; }

        //public Guid StartCountyId { get; set; }
        public County StartCounty { get; set; }   //virtual

        //public Guid EndCountyId { get; set; }
        public County EndCounty { get; set; }   //virtual

        public int StartSequence { get; set; }

        public int EndSequence { get; set; }
    }
}
