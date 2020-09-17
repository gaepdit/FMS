using FMS.Domain.Entities.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Entities
{
    public class Cabinet : BaseActiveModel, INamedModel
    {
        [StringLength(5)]
        public string Name { get; set; }

        // Files and Cabinets have a many-to-many relationship
        public ICollection<CabinetFile> CabinetFiles { get; set; }
    }
}
