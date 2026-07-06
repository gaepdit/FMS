using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Services;

namespace FMS.Domain.Repositories
{
    public interface IDashboardRepository : IDisposable
    {
        Task<bool> UserExistsAsync(Guid id);
        Task<ApplicationUser> GetApplicationUser(Guid id);
        Task<Guid> GetCOIdFromUser(UserView user);
        Task<List<OrganizationalUnitSummaryDto>> GetUserUnitListFromProgramById(Guid id, bool includeInactive = false);
        Task<List<DashboardUserFacilitiesDto>> GetUserHSIFacilitiesById(Guid id, bool includeInactive = false);
        Task<List<DashboardUnitFacilitiesDto>> GetUnitHSIFacilitiesById(Guid id, bool includeInactive = false);
        Task<List<DashboardProgramFacilitiesDto>> GetProgramHSIFacilitiesById(Guid id, bool includeInactive = false);
        Task<List<DashboardUserEventsDto>> GetEventsByUserId(Guid id, bool includeInactive = false);
        Task<List<DashboardUnitEventsDto>> GetUnitEventsByUserId(Guid id, bool includeInactive = false);
        Task<List<DashboardProgramEventsDto>> GetProgramEventsByUserId(Guid id, bool includeInactive = false);
    }
}
