using FMS.Domain.Entities.Base;
using FMS.Domain.Dto;

namespace FMS.Domain.Entities
{
    public class FundingSource : BaseActiveNamedModel
    {
        public FundingSource(FundingSourceCreateDto fundingSource)
        {
            Name = fundingSource.Name;
        }
        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}
