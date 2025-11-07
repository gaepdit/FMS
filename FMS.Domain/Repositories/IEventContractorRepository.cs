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
        Task<EventContractorEditDto> GetEventContractorByIdAsync(Guid id);
        public Task<IReadOnlyList<EventContractorSummaryDto>> GetAllEventContractorsAsync(bool activeOnly = true);
        Task CreateEventContractorAsync(EventContractor contractor);
        Task UpdateEventContractorAsync(EventContractor contractor);
        Task DeleteEventContractorAsync(Guid id);
    }
}
