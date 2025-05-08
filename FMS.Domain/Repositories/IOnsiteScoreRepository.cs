using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;
using FMS.Domain.Entities;

namespace FMS.Domain.Repositories
{
    public interface IOnsiteScoreRepository : IDisposable
    {
        Task<bool> OnsiteScoreExistsAsync(Guid id);

        Task<OnsiteScoreEditDto> GetOnsiteScoreByScoreIdAsync(Guid id);

        Task<bool> CreateOnsiteScoreAsync(OnSiteScoreCreateDto onSiteScore);

        Task<bool> UpdateOnsiteScoreAsync(OnsiteScoreEditDto onSiteScore);

        Task<bool> DeleteOnsiteScoreAsync(Guid id);
    }
}
