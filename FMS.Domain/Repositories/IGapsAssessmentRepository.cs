using FMS.Domain.Dto;
using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FMS.Domain.Repositories
{
    public interface IGapsAssessmentRepository : IDisposable
    {
        /// <summary>
        /// Checks if a Gaps Assessment with the specified ID exists.
        /// </summary>
        /// <param name="id">The ID of the Gaps Assessment to check.</param>
        /// <returns>True if the Gaps Assessment exists, otherwise false.</returns>
        Task<bool> GapsAssessmentExistsAsync(Guid id);

        /// <summary>
        /// Retrieves a Gaps Assessment by its ID.
        /// </summary>
        /// <param name="id">The ID of the Gaps Assessment.</param>
        /// <returns>The Gaps Assessment with the specified ID, or null if not found.</returns>
        Task<GapsAssessment> GetGapsAssessmentByIdAsync(Guid id);

        /// <summary>
        /// Retrieves all Gaps Assessments.
        /// </summary>
        /// <returns>A list of all Gaps Assessments.</returns>
        Task<IReadOnlyList<GapsAssessmentSummaryDto>> GetGapsAssessmentListAsync(bool ActiveOnly = false);

        /// <summary>
        /// Creates a new Gaps Assessment.
        /// </summary>
        /// <param name="gapsAssessment">The Gaps Assessment to create.</param>
        /// <returns>The created Gaps Assessment Guid.</returns>
        Task<Guid> CreateGapsAssessmentAsync(GapsAssessmentCreateDto gapsAssessment);

        /// <summary>
        /// Updates an existing Gaps Assessment.
        /// </summary>
        /// <param name="gapsAssessment">The Gaps Assessment to update.</param>
        /// <returns>The updated Gaps Assessment.</returns>
        Task UpdateGapsAssessmentAsync(GapsAssessmentCreateDto gapsAssessment);

        /// <summary>
        /// Deletes a Gaps Assessment by its ID.
        /// </summary>
        /// <param name="id">The ID of the Gaps Assessment to delete.</param>
        Task UpdateGapsAssessmentStatusAsync(Guid id);


    }
}
