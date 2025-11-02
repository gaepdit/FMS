using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class AbandonedInactive : BaseActiveModel, INamedModel
    {
        public AbandonedInactive() { }

        public AbandonedInactive(AbandonedInactiveCreateDto abandonedInactive)
        {
            Name = abandonedInactive.Name;
        }

        public string Name { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}
