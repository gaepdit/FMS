using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class FundingSource : BaseActiveModel, INamedModel
    {
        public FundingSource() { }

        public FundingSource(FundingSourceCreateDto fundingSource)
        {
            Name = fundingSource.Name;
            Description = fundingSource.Description;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
            Description = Description?.Trim();
        }

        public string DisplayName => $"{Name} ({Description})";
    }
}
