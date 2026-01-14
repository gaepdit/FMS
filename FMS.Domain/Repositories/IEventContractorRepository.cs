using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IEventContractorRepository : IDisposable
    {
        Task<bool> EventContractorExistsAsync(Guid id);
        Task<bool> EventContractorNameExistsAsync(string name);
        Task<EventContractorEditDto> GetEventContractorByIdAsync(Guid id);
        public Task<IReadOnlyList<EventContractorSummaryDto>> GetEventContractorListAsync(bool activeOnly = true);
        Task CreateEventContractorAsync(EventContractorCreateDto contractor);
        Task UpdateEventContractorAsync(Guid id,EventContractorEditDto editContractor);
        Task UpdateEventContractorStatusAsync(Guid id);
    }
}
