using FMS.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Entities
{
    public class Cabinet : BaseActiveModel, INamedModel
    {
        [StringLength(5)]
        public string Name { get; set; }

        // TODO: These properties are unused; remove from database if no use for them is found
        //public virtual County StartCounty { get; set; }
        //public virtual County EndCounty { get; set; }
        //public int? StartSequence { get; set; }
        //public int? EndSequence { get; set; }
    }
}
