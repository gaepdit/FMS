using System.ComponentModel.DataAnnotations;

namespace FMS.Models.Models
{
    public class FacilityType : BaseActiveModel
    {
        // Existing numeric code
        public int Code { get; set; }

        [StringLength(20)]
        public string Name { get; set; }
    }
}
