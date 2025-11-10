using FMS.Domain.Dto;
using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Domain.Repositories
{
    public interface IEventContractorRepository : IDisposable
    {
        Task<bool> EventContractorExistsAsync(Guid id);
        Task<bool> EventContractorNameExistsAsync(string name);
        Task<EventContractorEditDto> GetEventContractorByIdAsync(Guid id);
        public Task<IReadOnlyList<EventContractorSummaryDto>> GetEventContractorListAsync(bool activeOnly = true);
        Task CreateEventContractorAsync(EventContractorCreateDto contractor);
        Task UpdateEventContractorAsync(EventContractorEditDto contractor);
        Task UpdateEventContractorStatusAsync(Guid id);
    }
}
