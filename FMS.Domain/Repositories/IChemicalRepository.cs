using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IChemicalRepository : IDisposable
    {
        Task<bool> ChemicalExistsAsync(Guid id);
        
        Task<ChemicalEditDto> GetChemicalByIdAsync(Guid id);

        Task<IReadOnlyList<ChemicalSummaryDto>> GetChemicaListAsync();

        Task<Guid> CreateChemicalAsync(ChemicalCreateDto chemical);

        Task UpdateChemicalAsync(Guid Id, ChemicalEditDto chemicalUpdates);

        Task UpdateChemicalStatusAsync(Guid id, bool active);
    }
}
