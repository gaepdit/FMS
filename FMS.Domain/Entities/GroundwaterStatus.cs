using FMS.Domain.Dto;

namespace FMS.Domain.Entities
{
    public class GroundwaterStatus
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
