using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class AbandonSites : BaseActiveModel
    {
        public AbandonSites() { }

        public AbandonSites(AbandonSitesCreateDto createAbandonSites)
        {
            Name = createAbandonSites.Name;
            Description = createAbandonSites.Description;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string GetName()
        {
            return $"{Name} - ({Description})";
        }

        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}
