using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Domain.Utils;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS.Infrastructure.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly FmsDbContext _context;
        public StatusRepository(FmsDbContext context) => _context = context;


        public Task<bool> StatusExistsAsync(Guid id) =>
            _context.Statuses.AnyAsync(e => e.Id == id);

        public Task<StatusEditDto> GetStatusAsync(Guid facilityId) =>
            _context.Statuses.AsNoTracking()
                .Include(e => e.SourceStatus)
                .Include(e => e.SoilStatus)
                .Include(e => e.GroundwaterStatus)
                .Include(e => e.OverallStatus)
                .Include(e => e.FundingSource)
                .Include(e => e.GAPSAssessment)
                .Include(e => e.AbandonSites)
                .Where(e => e.FacilityId == facilityId)
                .Select(e => new StatusEditDto(e))
                .SingleOrDefaultAsync();

        public Task<Guid> CreateStatusAsync(StatusCreateDto status)
            {
            Prevent.Null(status, nameof(status));
            Prevent.NullOrEmpty(status.FacilityId, nameof(status.FacilityId));

            return CreateStatusInternalAsync(status);
        }

        private async Task<Guid> CreateStatusInternalAsync(StatusCreateDto status)
        {
            var newStatus = new Status(status);

            newStatus.AbandonSitesId = null;
            newStatus.FundingSourceId = null;
            newStatus.GAPSAssessmentId = null;
            newStatus.GroundwaterStatusId = null;
            newStatus.OverallStatusId = null;
            newStatus.SoilStatusId = null;
            newStatus.SourceStatusId = null;

            if (status.FacilityId == Guid.Empty)
            {
                throw new ArgumentException("FacilityId cannot be empty.", nameof(status));
            }

            _context.Statuses.Add(newStatus);
            await _context.SaveChangesAsync();
            return newStatus.Id;
        }

        public Task<bool> UpdateStatusAsync(Guid id, StatusEditDto statusUpdates)
        {
            Prevent.Null(statusUpdates, nameof(statusUpdates));
            Prevent.NullOrEmpty(id, nameof(id));

            return UpdateStatusInternalAsync(statusUpdates);
        }

        private async Task<bool> UpdateStatusInternalAsync(StatusEditDto statusUpdates)
        {
            var existingStatus = await _context.Statuses.SingleOrDefaultAsync(e => e.Id == statusUpdates.Id);

            if (existingStatus == null)
            {
                throw new ArgumentException($"Status: {statusUpdates.Id} does not exist.");
            }

            // Map properties from StatusEditDto to Status entity
            existingStatus.FacilityId = statusUpdates.FacilityId;
            existingStatus.SourceStatusId = statusUpdates.SourceStatusId;
            existingStatus.SourceDate = statusUpdates.SourceDate;
            existingStatus.SoilStatusId = statusUpdates.SoilStatusId;
            existingStatus.SoilDate = statusUpdates.SoilDate;
            existingStatus.GroundwaterStatusId = statusUpdates.GroundwaterStatusId;
            existingStatus.GroundwaterDate = statusUpdates.GroundwaterDate;
            existingStatus.OverallStatusId = statusUpdates.OverallStatusId;
            existingStatus.OverallDate = statusUpdates.OverallDate;
            existingStatus.ISWQS = statusUpdates.ISWQS;
            existingStatus.FundingSourceId = statusUpdates.FundingSourceId;
            existingStatus.LandFill = statusUpdates.LandFill;
            existingStatus.SolidWastePermitNumber = statusUpdates.SolidWastePermitNumber;
            existingStatus.GAPSScore = statusUpdates.GAPSScore;
            existingStatus.Comments = statusUpdates.Comments;
            existingStatus.Lien = statusUpdates.Lien;
            existingStatus.FinancialAssurance = statusUpdates.FinancialAssurance;
            existingStatus.GAPSModelDate = statusUpdates.GAPSModelDate;
            existingStatus.GAPSNoOfUnknowns = statusUpdates.GAPSNoOfUnknowns;
            existingStatus.GAPSAssessmentId = statusUpdates.GAPSAssessmentId;
            existingStatus.CostEstimate = statusUpdates.CostEstimate;
            existingStatus.CostEstimateDate = statusUpdates.CostEstimateDate;
            existingStatus.AbandonSitesId = statusUpdates.AbandonSitesId;

            _context.Statuses.Update(existingStatus);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task UpdateStatusStatusAsync(Guid id, bool active)
        {
            Prevent.NullOrEmpty(id, nameof(id));

            if (!await StatusExistsAsync(id))
            {
                throw new ArgumentException($"Status: {id} does not exist.");
            }

            var existingStatus = await _context.Statuses
                .SingleOrDefaultAsync(e => e.Id == id);

            if (existingStatus == null)
            {
                throw new ArgumentException($"Status: {id} does not exist.");
            }

            existingStatus.Active = active;

            _context.Statuses.Update(existingStatus);
            await _context.SaveChangesAsync();
        }

        #region IDisposable Support

        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;

            if (disposing)
            {
                // dispose managed state (managed objects)
                _context.Dispose();
            }

            // free unmanaged resources (unmanaged objects) and override finalizer
            // set large fields to null
            _disposedValue = true;
        }

        // override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~StatusRepository()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
