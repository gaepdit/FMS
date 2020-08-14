using FMS.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Entities
{
    public class FacilityType : BaseActiveModel
    {
        // Existing numeric code
        public int Code { get; set; }

        [Display(Name = "Facility Type")]
        [StringLength(20)]
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
