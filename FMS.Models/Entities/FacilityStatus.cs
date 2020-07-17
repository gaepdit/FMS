using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class FacilityStatus : BaseActiveModel
    {
        public string Status { get; set; }

        public EnvironmentalInterest EnvironmentalInterest { get; set; }
    }
}