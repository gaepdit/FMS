using FMS.Domain.Entities.Base;
using FMS.Domain.Dto;

namespace FMS.Domain.Entities
{
    public class FundingSource : BaseActiveModel
    {
        public FundingSource() { }

        public FundingSource(FundingSourceCreateDto fundingSource)
        {
            Name = fundingSource.Name;
        }

        public string Name { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}
