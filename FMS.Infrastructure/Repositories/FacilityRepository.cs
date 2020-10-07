using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMS.Domain.Data;
using FMS.Domain.Dto;
using FMS.Domain.Dto.PaginatedList;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Domain.Utils;
using FMS.Infrastructure.Contexts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FMS.Infrastructure.Repositories
{
    public class FacilityRepository : IFacilityRepository
    {
        private readonly FmsDbContext _context;
        private readonly IFileRepository _fileRepository;

        public FacilityRepository(FmsDbContext context, IFileRepository fileRepository)
        {
            _context = context;
            _fileRepository = fileRepository;
        }

        public async Task<bool> FacilityExistsAsync(Guid id) =>
            await _context.Facilities.AnyAsync(e => e.Id == id);

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
                .Include(e => e.File).ThenInclude(e => e.CabinetFiles).ThenInclude(c => c.Cabinet)
                .Include(e => e.RetentionRecords)
                .SingleOrDefaultAsync(e => e.Id == id);

            return facility == null ? null : new FacilityDetailDto(facility);
        }

        public async Task<int> CountAsync(FacilitySpec spec)
        {
            return await _context.Facilities.AsNoTracking()
                .Where(e => string.IsNullOrEmpty(spec.Name) || e.Name.Contains(spec.Name))
                .Where(e => !spec.CountyId.HasValue || e.County.Id == spec.CountyId.Value)
                .Where(e => !spec.ActiveOnly || e.Active)
                .Where(e => string.IsNullOrEmpty(spec.FacilityNumber) || e.FacilityNumber.Contains(spec.FacilityNumber))
                .Where(e => !spec.FacilityStatusId.HasValue || e.FacilityStatus.Id.Equals(spec.FacilityStatusId))
                .Where(e => !spec.FacilityTypeId.HasValue || e.FacilityType.Id.Equals(spec.FacilityTypeId))
                .Where(e => !spec.BudgetCodeId.HasValue || e.BudgetCode.Id.Equals(spec.BudgetCodeId))
                .Where(e => !spec.OrganizationalUnitId.HasValue ||
                    e.OrganizationalUnit.Id.Equals(spec.OrganizationalUnitId))
                .Where(e => !spec.EnvironmentalInterestId.HasValue ||
                    e.EnvironmentalInterest.Id.Equals(spec.EnvironmentalInterestId))
                .Where(e => !spec.ComplianceOfficerId.HasValue ||
                    e.ComplianceOfficer.Id.Equals(spec.ComplianceOfficerId))
                .Where(e => string.IsNullOrEmpty(spec.FileLabel) || e.File.FileLabel.Contains(spec.FileLabel))
                .Where(e => string.IsNullOrEmpty(spec.Address) || e.Address.Contains(spec.Address))
                .Where(e => string.IsNullOrEmpty(spec.City) || e.City.Contains(spec.City))
                .Where(e => string.IsNullOrEmpty(spec.State) || e.State.Contains(spec.State))
                .Where(e => string.IsNullOrEmpty(spec.PostalCode) || e.PostalCode.Contains(spec.PostalCode))
                .CountAsync();
        }

        public async Task<PaginatedList<FacilitySummaryDto>> GetFacilityPaginatedListAsync(
            FacilitySpec spec, int pageNumber, int pageSize)
        {
            Prevent.NegativeOrZero(pageNumber, nameof(pageNumber));
            Prevent.NegativeOrZero(pageSize, nameof(pageSize));

            var items = await _context.Facilities.AsNoTracking()
                .Where(e => string.IsNullOrEmpty(spec.Name) || e.Name.Contains(spec.Name))
                .Where(e => !spec.CountyId.HasValue || e.County.Id == spec.CountyId.Value)
                .Where(e => !spec.ActiveOnly || e.Active)
                .Where(e => string.IsNullOrEmpty(spec.FacilityNumber) || e.FacilityNumber.Contains(spec.FacilityNumber))
                .Where(e => !spec.FacilityStatusId.HasValue || e.FacilityStatus.Id.Equals(spec.FacilityStatusId))
                .Where(e => !spec.FacilityTypeId.HasValue || e.FacilityType.Id.Equals(spec.FacilityTypeId))
                .Where(e => !spec.BudgetCodeId.HasValue || e.BudgetCode.Id.Equals(spec.BudgetCodeId))
                .Where(e => !spec.OrganizationalUnitId.HasValue ||
                    e.OrganizationalUnit.Id.Equals(spec.OrganizationalUnitId))
                .Where(e => !spec.EnvironmentalInterestId.HasValue ||
                    e.EnvironmentalInterest.Id.Equals(spec.EnvironmentalInterestId))
                .Where(e => !spec.ComplianceOfficerId.HasValue ||
                    e.ComplianceOfficer.Id.Equals(spec.ComplianceOfficerId))
                .Where(e => string.IsNullOrEmpty(spec.FileLabel) || e.File.FileLabel.Contains(spec.FileLabel))
                .Where(e => string.IsNullOrEmpty(spec.Address) || e.Address.Contains(spec.Address))
                .Where(e => string.IsNullOrEmpty(spec.City) || e.City.Contains(spec.City))
                .Where(e => string.IsNullOrEmpty(spec.State) || e.State.Contains(spec.State))
                .Where(e => string.IsNullOrEmpty(spec.PostalCode) || e.PostalCode.Contains(spec.PostalCode))
                .Include(e => e.File).ThenInclude(e => e.CabinetFiles).ThenInclude(c => c.Cabinet)
                .Include(e => e.RetentionRecords)
                .OrderBy(e => e.File.FileLabel).ThenBy(e => e.FacilityNumber)
                .Skip((pageNumber - 1) * pageSize).Take(pageSize)
                .Select(e => new FacilitySummaryDto(e))
                .ToListAsync();

            var totalCount = await CountAsync(spec);
            return new PaginatedList<FacilitySummaryDto>(items, totalCount, pageNumber, pageSize);
        }

        public async Task<IReadOnlyList<FacilityDetailDto>> GetFacilityDetailListAsync(FacilitySpec spec)
        {
            return await _context.Facilities.AsNoTracking()
                .Where(e => string.IsNullOrEmpty(spec.Name) || e.Name.Contains(spec.Name))
                .Where(e => !spec.CountyId.HasValue || e.County.Id == spec.CountyId.Value)
                .Where(e => !spec.ActiveOnly || e.Active)
                .Where(e => string.IsNullOrEmpty(spec.FacilityNumber) || e.FacilityNumber.Contains(spec.FacilityNumber))
                .Where(e => !spec.FacilityStatusId.HasValue || e.FacilityStatus.Id.Equals(spec.FacilityStatusId))
                .Where(e => !spec.FacilityTypeId.HasValue || e.FacilityType.Id.Equals(spec.FacilityTypeId))
                .Where(e => !spec.BudgetCodeId.HasValue || e.BudgetCode.Id.Equals(spec.BudgetCodeId))
                .Where(e => !spec.OrganizationalUnitId.HasValue ||
                    e.OrganizationalUnit.Id.Equals(spec.OrganizationalUnitId))
                .Where(e => !spec.EnvironmentalInterestId.HasValue ||
                    e.EnvironmentalInterest.Id.Equals(spec.EnvironmentalInterestId))
                .Where(e => !spec.ComplianceOfficerId.HasValue ||
                    e.ComplianceOfficer.Id.Equals(spec.ComplianceOfficerId))
                .Where(e => string.IsNullOrEmpty(spec.FileLabel) || e.File.FileLabel.Contains(spec.FileLabel))
                .Where(e => string.IsNullOrEmpty(spec.Address) || e.Address.Contains(spec.Address))
                .Where(e => string.IsNullOrEmpty(spec.City) || e.City.Contains(spec.City))
                .Where(e => string.IsNullOrEmpty(spec.State) || e.State.Contains(spec.State))
                .Where(e => string.IsNullOrEmpty(spec.PostalCode) || e.PostalCode.Contains(spec.PostalCode))
                .Include(e => e.County)
                .Include(e => e.FacilityStatus)
                .Include(e => e.FacilityType)
                .Include(e => e.BudgetCode)
                .Include(e => e.OrganizationalUnit)
                .Include(e => e.EnvironmentalInterest)
                .Include(e => e.ComplianceOfficer)
                .Include(e => e.File).ThenInclude(e => e.CabinetFiles).ThenInclude(c => c.Cabinet)
                .Include(e => e.RetentionRecords)
                .OrderBy(e => e.File.FileLabel).ThenBy(e => e.FacilityNumber)
                .Select(e => new FacilityDetailDto(e))
                .ToListAsync();
        }

        public async Task<IReadOnlyList<FacilityMapSummaryDto>> GetFacilityListAsync(FacilityMapSpec spec)
        {
            var active = new SqlParameter("@active", spec.ActiveOnly);
            var lat = new SqlParameter("@Lat", spec.Latitude);
            var lng = new SqlParameter("@Lng", spec.Longitude);
            var radius = new SqlParameter("@radius", spec.Radius);

            return await _context.FacilityList
                .FromSqlRaw("EXEC dbo.getNearbyFacilities @Lat={0}, @Lng={1}, @radius={2}, @active={3}",
                    lat, lng, radius, active)
                .ToListAsync();
        }

        public async Task<Guid> CreateFacilityAsync(FacilityCreateDto newFacility)
        {
            if (string.IsNullOrWhiteSpace(newFacility.FacilityNumber))
            {
                throw new ArgumentException("Facility Number is required.");
            }

            if (await FacilityNumberExists(newFacility.FacilityNumber))
            {
                throw new ArgumentException($"Facility Number '{newFacility.FacilityNumber}' already exists.");
            }

            File file;
            if (string.IsNullOrWhiteSpace(newFacility.FileLabel))
            {
                // Generate new File if File Label is empty
                if (Data.Counties.All(e => e.Id != newFacility.CountyId))
                {
                    throw new ArgumentException($"County ID {newFacility.CountyId} does not exist.");
                }

                var nextSequence = await _fileRepository.GetNextSequenceForCountyAsync(newFacility.CountyId);
                file = new File(newFacility.CountyId, nextSequence);

                await _context.Files.AddAsync(file);
            }
            else
            {
                // Otherwise, if File Label is provided, make sure it exists
                file = await _context.Files.SingleOrDefaultAsync(e => e.FileLabel == newFacility.FileLabel)
                    ?? throw new ArgumentException($"File Label {newFacility.FileLabel} does not exist.");
            }

            var newFac = new Facility(newFacility)
            {
                File = file
            };

            await _context.Facilities.AddAsync(newFac);
            await _context.SaveChangesAsync();

            return newFac.Id;
        }

        public async Task UpdateFacilityAsync(Guid id, FacilityEditDto facilityUpdates)
        {
            var facility = await _context.Facilities.FindAsync(id)
                ?? throw new ArgumentException("Facility ID not found.", nameof(id));

            if (string.IsNullOrWhiteSpace(facilityUpdates.FacilityNumber))
            {
                throw new ArgumentException("Facility Number is required.");
            }

            if (await FacilityNumberExists(facilityUpdates.FacilityNumber, id))
            {
                throw new ArgumentException($"Facility Number '{facilityUpdates.FacilityNumber}' already exists.");
            }

            File file;
            if (string.IsNullOrWhiteSpace(facilityUpdates.FileLabel))
            {
                // Generate new File if File Label is empty
                if (Data.Counties.All(e => e.Id != facilityUpdates.CountyId))
                {
                    throw new ArgumentException($"County ID {facilityUpdates.CountyId} does not exist.");
                }

                var nextSequence = await _fileRepository.GetNextSequenceForCountyAsync(facilityUpdates.CountyId);
                file = new File(facilityUpdates.CountyId, nextSequence);

                await _context.Files.AddAsync(file);
                facility.File = file;
            }
            else
            {
                // Otherwise, if File Label is provided and different from existing label, make sure it exists
                var oldFile = await _context.Files.FindAsync(facility.FileId);
                if (facilityUpdates.FileLabel != oldFile.FileLabel)
                {
                    file = await _context.Files.SingleOrDefaultAsync(e => e.FileLabel == facilityUpdates.FileLabel);
                    if (file == null)
                        throw new ArgumentException($"File Label {facilityUpdates.FileLabel} does not exist.");
                    facility.File = file;
                }
            }

            // facility.Active = facilityUpdates.Active;
            facility.CountyId = facilityUpdates.CountyId;
            facility.FacilityNumber = facilityUpdates.FacilityNumber;
            facility.Name = facilityUpdates.Name;
            facility.FacilityStatusId = facilityUpdates.FacilityStatusId;
            facility.FacilityTypeId = facilityUpdates.FacilityTypeId;
            facility.BudgetCodeId = facilityUpdates.BudgetCodeId;
            facility.OrganizationalUnitId = facilityUpdates.OrganizationalUnitId;
            facility.EnvironmentalInterestId = facilityUpdates.EnvironmentalInterestId;
            facility.ComplianceOfficerId = facilityUpdates.ComplianceOfficerId;
            facility.Location = facilityUpdates.Location;
            facility.Address = facilityUpdates.Address;
            facility.City = facilityUpdates.City;
            facility.State = facilityUpdates.State;
            facility.PostalCode = facilityUpdates.PostalCode;
            facility.Latitude = facilityUpdates.Latitude;

            await _context.SaveChangesAsync();
        }

        public async Task<bool> FacilityNumberExists(string facilityNumber, Guid? ignoreId = null) =>
            await _context.Facilities.AnyAsync(e =>
                e.FacilityNumber == facilityNumber
                && (!ignoreId.HasValue || e.Id != ignoreId.Value));

        public async Task<bool> FileLabelExists(string fileLabel) =>
            await _context.Files.AnyAsync(e => e.FileLabel == fileLabel);

        // Retention Records

        public async Task<bool> RetentionRecordExistsAsync(Guid id) =>
            await _context.RetentionRecords.AnyAsync(e => e.Id == id);

        public async Task<RetentionRecordDetailDto> GetRetentionRecordAsync(Guid id)
        {
            var record = await _context.RetentionRecords.FindAsync(id);
            return record == null ? null : new RetentionRecordDetailDto(record);
        }

        public async Task<Guid> CreateRetentionRecordAsync(Guid facilityId, RetentionRecordCreateDto create)
        {
            var record = new RetentionRecord(facilityId, create);
            await _context.RetentionRecords.AddAsync(record);
            await _context.SaveChangesAsync();
            return record.Id;
        }

        public async Task UpdateRetentionRecordAsync(Guid id, RetentionRecordEditDto edit)
        {
            var record = await _context.RetentionRecords.FindAsync(id);

            if (record == null)
            {
                throw new ArgumentException("Retention Record ID not found.", nameof(id));
            }

            record.Active = edit.Active;
            record.BoxNumber = edit.BoxNumber;
            record.ConsignmentNumber = edit.ConsignmentNumber;
            record.EndYear = edit.EndYear;
            record.RetentionSchedule = edit.RetentionSchedule;
            record.ShelfNumber = edit.ShelfNumber;
            record.StartYear = edit.StartYear;

            await _context.SaveChangesAsync();
        }

        public async Task<FacilityBasicDto> GetFacilityForRetentionRecord(Guid recordId)
        {
            var record = await _context.RetentionRecords.FindAsync(recordId);

            if (record == null)
            {
                throw new ArgumentException("Retention Record ID not found.", nameof(recordId));
            }

            var facility = await _context.Facilities.AsNoTracking()
                .Include(e => e.File)
                .SingleOrDefaultAsync(e => e.Id == record.FacilityId);

            if (facility == null)
            {
                throw new ArgumentException("Facility not found for Retention Record.");
            }

            return new FacilityBasicDto(facility);
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