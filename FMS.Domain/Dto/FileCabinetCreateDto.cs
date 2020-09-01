using FMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FileCabinetCreateDto
    {
        // File Cabinet Number
        [StringLength(5)]
        [Display(Name = "File Cabinet Number")]
        public string Name { get; set; }

        // Starting County
        public County StartCounty { get; set; }

        // Ending County
        public County EndCounty { get; set; }

        // Starting Sequence Number
        public int StartSequence { get; set; }

        // Ending Sequence Number
        public int EndSequence { get; set; }
    }
}
