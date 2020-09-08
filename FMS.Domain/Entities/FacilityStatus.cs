using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class FacilityStatus : BaseActiveModel, INamedModel
    {
        public string Status { get; set; }

        //public Guid EnvironmentalInterestId { get; set; }
        public EnvironmentalInterest EnvironmentalInterest { get; set; }

        public string Name => Status;
    }
}