using FMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS.Domain.Dto;

namespace FMS.Domain.Entities
{
    public class FundingSource : BaseActiveNamedModel
    {
        public FundingSource() { }
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
