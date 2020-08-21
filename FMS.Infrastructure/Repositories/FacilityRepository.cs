﻿using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS.Infrastructure.Repositories
{
    public class FacilityRepository : IFacilityRepository
    {
        private readonly FmsDbContext _context;

        public FacilityRepository(FmsDbContext context) => _context = context;

        public async Task<bool> FacilityExistsAsync(Guid id)
        {
            return await _context.Facilities.AnyAsync(e => e.Id == id);
        }

        public async Task<FacilityDetailDto> GetFacilityAsync(Guid id)
        {
            var facility = await _context.Facilities.AsNoTracking()
                .Include(e => e.County)
                .Include(e => e.FacilityStatus)
                .Include(e => e.FacilityType)
                .Include(e => e.BudgetCode)
                .Include(e => e.OrganizationalUnit)
                .Include(e => e.EnvironmentalInterest)
                .Include(e => e.ComplianceOfficer)
                .Include(e => e.File)
                .SingleOrDefaultAsync(e => e.Id == id);

            if (facility == null)
            {
                return null;
            }

            return new FacilityDetailDto(facility);
        }

        public Task<int> CountAsync(FacilitySpec spec)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<FacilitySummaryDto>> GetFacilityListAsync(FacilitySpec spec)
        {
            return await _context.Facilities.AsNoTracking()
                .Where(e => string.IsNullOrEmpty(spec.Name) || e.Name.Contains(spec.Name))
                .Where(e => !spec.CountyId.HasValue || e.County.Id == spec.CountyId.Value)
                .Where(e => !spec.Active.HasValue || e.Active == spec.Active.Value)
                .Where(e => !spec.FacilityStatusId.Equals(Guid.Empty) || e.FacilityStatus.Id.Equals(spec.FacilityStatusId))
                .Where(e => !spec.FacilityTypeId.Equals(Guid.Empty) || e.FacilityType.Id.Equals(spec.FacilityTypeId))
                .Where(e => !spec.BudgetCodeId.Equals(Guid.Empty) || e.BudgetCode.Id.Equals(spec.BudgetCodeId))
                .Where(e => !spec.OrganizationalUnitId.Equals(Guid.Empty) || e.OrganizationalUnit.Id.Equals(spec.OrganizationalUnitId))
                .Where(e => !spec.EnvironmentalInterestId.Equals(Guid.Empty) || e.EnvironmentalInterest.Id.Equals(spec.EnvironmentalInterestId))
                .Where(e => !spec.ComplianceOfficerId.Equals(Guid.Empty) || e.ComplianceOfficer.Id.Equals(spec.ComplianceOfficerId))
                .Where(e => !spec.FileId.Equals(Guid.Empty) || e.File.Id.Equals(spec.FileId))
                .Where(e => string.IsNullOrEmpty(spec.Location) || e.Location.Contains(spec.Location))
                .Where(e => string.IsNullOrEmpty(spec.Address) || e.Address.Contains(spec.Address))
                .Where(e => string.IsNullOrEmpty(spec.City) || e.City.Contains(spec.City))
                .Where(e => string.IsNullOrEmpty(spec.State) || e.State.Contains(spec.State))
                .Where(e => string.IsNullOrEmpty(spec.PostalCode) || e.PostalCode.Contains(spec.PostalCode))
                .Where(e => !spec.Latitude.Equals(null) || e.Latitude.ToString().Contains(spec.Latitude.ToString()))
                .Where(e => !spec.Longitude.Equals(null) || e.Longitude.ToString().Contains(spec.Longitude.ToString()))
                .Select(e => new FacilitySummaryDto(e))
                .ToListAsync();
        }

        public Task<bool> CreateFacilityAsync(FacilityCreateDto facility)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateFacilityAsync(Guid id, FacilityEditDto facilityUpdates)
        {
            var facility = await _context.Facilities.FindAsync(id);
            if (facility == null)
            {
                throw new ArgumentException("Facility ID not found", nameof(id));
            }

            facility.Active = facilityUpdates.Active;
            facility.CountyId = facilityUpdates.CountyId;
            facility.FacilityNumber = facilityUpdates.FacilityNumber;
            facility.Name = facilityUpdates.Name;
            facility.FacilityStatusId = facilityUpdates.FacilityStatusId;
            facility.FacilityTypeId = facilityUpdates.FacilityTypeId;
            facility.BudgetCodeId = facilityUpdates.BudgetCodeId;
            facility.OrganizationalUnitId = facilityUpdates.OrganizationalUnitId;
            facility.EnvironmentalInterestId = facilityUpdates.EnvironmentalInterestId;
            facility.ComplianceOfficerId = facilityUpdates.ComplianceOfficerId;
            facility.FileId = facilityUpdates.FileId;
            facility.Location = facilityUpdates.Location;
            facility.Address = facilityUpdates.Address;
            facility.City = facilityUpdates.City;
            facility.State = facilityUpdates.Address;
            facility.PostalCode = facilityUpdates.PostalCode;
            facility.Latitude = facilityUpdates.Latitude;
            await _context.SaveChangesAsync();
        }

        #region IDisposable Support

        private bool _disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects)
                    _context.Dispose();
                }

                _disposedValue = true;
            }
        }

        //  override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~FacilityRepository()
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
