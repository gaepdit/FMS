using FMS.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Domain.Repositories
{
    public interface ICountyRepository : IDisposable
    {
        Task<IReadOnlyList<CountySummaryDto>> GetCountyListAsync();
    }
}
