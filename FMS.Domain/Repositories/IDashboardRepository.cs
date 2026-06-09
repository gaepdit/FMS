using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;

namespace FMS.Domain.Repositories
{
    public interface IDashboardRepository : IDisposable
    {
        Task<bool> UserExistsAsync(Guid id);
        Task<ApplicationUser> GetApplicationUser(Guid id);
        Task<List<DashboardUserFacilitiesDto>> GetUserFacilitiesById(Guid id, bool includeInactive = false);
        Task<List<DashboardUnitFacilitiesDto>> GetUnitFacilitiesById(Guid id, bool includeInactive = false);
        Task<List<DashboardProgramFacilitiesDto>> GetProgramFacilitiesById(Guid id, bool includeInactive = false);
    }
}
