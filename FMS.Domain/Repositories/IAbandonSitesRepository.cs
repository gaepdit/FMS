using FMS.Domain.Dto;
using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Domain.Repositories 
{
    public interface IAbandonSitesRepository : IDisposable
    {
        /// <summary>
        /// Checks to see if AbandonSites with the specified ID exists.
        /// </summary>
        /// <param name="id"> The ID of the Abandon Site to check.</param>
        /// <returns>True if the Abandon Site exists, otherwise false.</returns>
        Task<bool> AbandonSitesExistsAsync(Guid id);

        /// <summary>
        /// Checks if an Abandon Site with the specified name exists.
        /// </summary>  
        /// <param name="name"> The name of the Abandon Site to check.</param>
        /// <param name="ignoreId">Optional ID to ignore in the check (used for updates).</param>
        /// <returns>True if the name exists, otherwise false.</returns>
        Task<bool> AbandonSitesNameExistsAsync(string name, Guid? ignoreId = null);

        /// <summary>
        /// Retrieves an Abandon Site by its ID.
        /// </summary>
        /// <param name="id">The ID of the Abandon Site.</param>
        /// <returns>The Abandon Site with the specified ID, or null if not found.</returns>
        Task<AbandonSitesEditDto> GetAbandonSitesByIdAsync(Guid id);

        /// <summary>
        /// Retrieves all Abandon Sites.
        /// </summary>
        /// <returns>A list of all Abandon Sites.</returns>
        Task<IReadOnlyList<AbandonSitesSummaryDto>> GetAbandonSitesListAsync(bool ActiveOnly = false);

        /// <summary>
        /// Creates a new Abandon Site.
        /// </summary>
        /// <param name="abandonSite">The Abandon Site to create.</param>
        /// <returns>The created Abandon Site.</returns>
        Task<Guid> CreateAbandonSitesAsync(AbandonSitesCreateDto abandonSite);

        /// <summary>
        /// Updates an existing Abandon Site.
        /// </summary>
        /// <param name="id">The ID of the Abandon Site to update.</param>
        /// <param name="abandonSitesUpdates">The Abandon Site to update.</param>
        /// <returns>The updated Abandon Site.</returns>
        Task UpdateAbandonSitesAsync(Guid id, AbandonSitesEditDto abandonSitesUpdates);

        /// <summary>
        /// Deletes an Abandon Site by its ID.
        /// </summary>
        /// <param name="id">The ID of the Abandon Site to delete.</param>
        Task UpdateAbandonSitesStatusAsync(Guid id, bool active);
    }
}
