using FMS.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Entities
{
    public class FacilityType : BaseActiveModel
    {
        // Existing numeric code
        public int Code { get; set; }

        [StringLength(20)]
        [Display(Name = "Facility Type")]
        public string Name { get; set; }
    }
}
