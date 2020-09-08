using FMS.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Entities
{
    public class FileCabinet : BaseActiveModel
    {
        [StringLength(5)]
        [Display(Name = "File Cabinet Number")]
        public string Name { get; set; }

        public virtual County StartCounty { get; set; }

        public virtual County EndCounty { get; set; }

        public int StartSequence { get; set; }

        public int EndSequence { get; set; }
    }
}
