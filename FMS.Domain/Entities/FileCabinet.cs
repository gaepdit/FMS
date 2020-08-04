using FMS.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Entities
{
    public class FileCabinet : BaseActiveModel
    {
        // Existing Program Cabinet Numbers
        [Display(Name = "File Cabinet Number")]
        [StringLength(5)]
        public string Name { get; set; }

        [Display(Name = "Starting County")]
        public County StartCounty { get; set; }

        [Display(Name = "Ending County")]
        public County EndCounty { get; set; }

        [Display(Name = "Starting Sequence Number")]
        public int StartSequence { get; set; }

        [Display(Name = "Ending Sequence Number")]
        public int EndSequence { get; set; }

        // Collection of Files in Cabinet go here if necessary
    }
}
