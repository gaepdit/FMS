using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class GroundwaterStatus : BaseActiveModel
    {
        public GroundwaterStatus() { }

        public GroundwaterStatus(GroundwaterStatusCreateDto groundwaterStatus)
        {
            Name = groundwaterStatus.Name;
        }

        public string Name { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}
