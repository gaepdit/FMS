using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class FacilityStatus : BaseActiveModel, INamedModel
    {
        public FacilityStatus() { }

        public FacilityStatus(FacilityStatusCreateDto newFacilityStatus)
        {
            Status = newFacilityStatus.Status;
        }
        public string Status { get; set; }

        public string Name => Status;
    }
}