using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities;

namespace FMS.Domain.Repositories
{
    public interface IChemicalRepository : IDisposable 
    {
        Task<bool> ChemicalExistsAsync(Guid id);

        Task<bool> ChemicalCasNoExistsAsync(string casNo, Guid? ignoreId = null);

        Task<bool> ChemicalChemicalNameExistsAsync(string chemicalName, Guid? ignoreId = null);

        Task<bool> ChemicalCommonNameExistsAsync(string commonName, Guid? ignoreId = null);

        Task<ChemicalEditDto> GetChemicalByIdAsync(Guid? id);

        Task<Chemical> GetChemicalByNameAsync(string name);

        Task<Chemical> GetChemicalByChemIdAsync(Guid id);

        Task<IReadOnlyList<ChemicalSummaryDto>> GetChemicalListAsync();

        Task<Guid> CreateChemicalAsync(ChemicalCreateDto chemical);

        Task UpdateChemicalAsync(Guid id, ChemicalEditDto chemicalUpdates);

        Task UpdateChemicalStatusAsync(Guid id, bool active);
    }
}
