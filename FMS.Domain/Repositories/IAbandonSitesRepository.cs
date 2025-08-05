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
        /// Retrieves an Abandon Site by its ID.
        /// </summary>
        /// <param name="id">The ID of the Abandon Site.</param>
        /// <returns>The Abandon Site with the specified ID, or null if not found.</returns>
        Task<AbandonSites> GetAbandonSitesByIdAsync(Guid id);

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
        /// <param name="abandonSite">The Abandon Site to update.</param>
        /// <returns>The updated Abandon Site.</returns>
        Task UpdateAbandonSitesAsync(AbandonSitesCreateDto abandonSite);

        /// <summary>
        /// Deletes an Abandon Site by its ID.
        /// </summary>
        /// <param name="id">The ID of the Abandon Site to delete.</param>
        Task UpdateAbandonSitesStatusAsync(Guid id);
    }
}
