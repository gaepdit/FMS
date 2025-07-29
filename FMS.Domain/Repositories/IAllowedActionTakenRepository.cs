using FMS.Domain.Dto;
using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Domain.Repositories
{
    public interface IAllowedActionTakenRepository : IDisposable
    {
        Task<bool> AllowedActionTakenExistsAsync(Guid id);

        Task<bool> AllowedActionTakenExistsAsync(Guid eventTypeId, Guid actionTakenId);

        Task<AllowedActionTakenSpec> GetAllowedActionTakenByIdAsync(Guid? id);

        Task<AllowedActionTakenSpec> GetAllowedActionTakenByAATIdAsync(Guid? id);

        Task<AllowedActionTaken> GetAllowedActionTakenAsync(Guid eventTypeId, Guid actionTakenId);

        Task<IList<AllowedActionTakenSpec>> GetAllowedActionTakenListAsync(Guid eventTypeId);

        Task<Guid> CreateAllowedActionTakenAsync(AllowedActionTakenSpec allowedActionTaken);

        //Task<Guid> UpdateAllowedActionTakenAsync(Guid actionTakenId, Guid eventTypeId);

        Task<Guid> DeleteAllowedActionTakenAsync(Guid? id);
    }
}
