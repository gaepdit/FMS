using System.ComponentModel.DataAnnotations;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class Cabinet : BaseActiveNamedModel
    {
        /// <summary>
        /// The first/lowest file label in the cabinet. Used for determining appropriate cabinet when
        /// creating new file labels.
        /// </summary>
        [StringLength(9)]
        public string FirstFileLabel { get; set; }
    }
}