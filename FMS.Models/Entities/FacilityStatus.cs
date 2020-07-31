using FMS.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Entities
{
    public class FacilityStatus : BaseActiveModel
    {
        [Display(Name = "Facility Status")]
        public string Status { get; set; }

        public EnvironmentalInterest EnvironmentalInterest { get; set; }
    }
}