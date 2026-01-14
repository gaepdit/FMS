using FMS.Domain.Dto;

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
        /// Checks if a Gaps Assessment with the specified name exists.
        /// </summary>  
        /// <param name="ignoreId">Optional ID to ignore when checking for name uniqueness.</param>
        /// <param name="name"> The name of the Gaps Assessment to check.</param>
        /// <returns>True if the Gaps Assessment name exists, otherwise false.</returns>
        Task<bool> GapsAssessmentNameExistsAsync(string name, Guid? ignoreId = null);

        /// <summary>
        /// Retrieves a Gaps Assessment by its ID.
        /// </summary>
        /// <param name="id">The ID of the Gaps Assessment.</param>
        /// <returns>The Gaps Assessment with the specified ID, or null if not found.</returns>
        Task<GapsAssessmentEditDto> GetGapsAssessmentByIdAsync(Guid id);

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
        /// <param name="gapsAssessmentUpdates">The Gaps Assessment to update.</param>
        /// <returns>The updated Gaps Assessment.</returns>
        Task UpdateGapsAssessmentAsync(Guid id, GapsAssessmentEditDto gapsAssessmentUpdates);

        /// <summary>
        /// Deletes a Gaps Assessment by its ID.
        /// </summary>
        /// <param name="id">The ID of the Gaps Assessment to delete.</param>
        Task UpdateGapsAssessmentStatusAsync(Guid id, bool active);
    }
}
