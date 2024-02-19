using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using System.Xml.Linq;
using Dapper;
using FMS.Domain.Data;
using FMS.Domain.Dto;
using FMS.Domain.Dto.PaginatedList;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Domain.Utils;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FMS.Infrastructure.Repositories
{
    public class FacilityRepository : IFacilityRepository
    {
        private readonly FmsDbContext _context;
        public FacilityRepository(FmsDbContext context) => _context = context;

        public Task<bool> FacilityExistsAsync(Guid id) =>
            _context.Facilities.AnyAsync(e => e.Id == id);

        public async Task<FacilityDetailDto> GetFacilityAsync(Guid id)
        {
            var facility = await _context.Facilities.AsNoTracking()
                .Include(e => e.County)
                .Include(e => e.FacilityStatus)
                .Include(e => e.FacilityType)
                .Include(e => e.BudgetCode)
                .Include(e => e.OrganizationalUnit)
                .Include(e => e.ComplianceOfficer)
                .Include(e => e.File)
                .Include(e => e.RetentionRecords)
                .SingleOrDefaultAsync(e => e.Id == id);

            if (facility == null) return null;

            facility.RetentionRecords = facility.RetentionRecords
                .OrderBy(e => e.StartYear)
                .ThenBy(e => e.EndYear)
                .ThenBy(e => e.BoxNumber).ToList();

            var facilityDetail = new FacilityDetailDto(facility);

            if (!facilityDetail.FileLabel.IsNullOrEmpty())
            {
                facilityDetail.Cabinets = (await _context.GetCabinetListAsync(false))
                .GetCabinetsForFile(facilityDetail.FileLabel);
            }

            return facilityDetail;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Critical Code Smell", "S3776:Cognitive Complexity of methods should not be too high", Justification = "<Pending>")]
        private IQueryable<Facility> QueryFacilities(FacilitySpec spec) => _context.Facilities.AsNoTracking()
            .Where(e => string.IsNullOrEmpty(spec.Name) || e.Name.Contains(spec.Name))
            .Where(e => !spec.CountyId.HasValue || e.County.Id == spec.CountyId.Value)
            .Where(e => spec.ShowDeleted || e.Active)
            .Where(e => !spec.ShowPendingOnly || !e.DeterminationLetterDate.HasValue)
            .Where(e => string.IsNullOrEmpty(spec.FacilityNumber) || e.FacilityNumber.Contains(spec.FacilityNumber))
            .Where(e => !spec.FacilityStatusId.HasValue || e.FacilityStatus.Id.Equals(spec.FacilityStatusId))
            .Where(e => !spec.FacilityTypeId.HasValue || e.FacilityType.Id.Equals(spec.FacilityTypeId))
            .Where(e => !spec.BudgetCodeId.HasValue || e.BudgetCode.Id.Equals(spec.BudgetCodeId))
            .Where(e =>
                !spec.OrganizationalUnitId.HasValue || e.OrganizationalUnit.Id.Equals(spec.OrganizationalUnitId))
            .Where(e => !spec.ComplianceOfficerId.HasValue || e.ComplianceOfficer.Id.Equals(spec.ComplianceOfficerId))
            .Where(e => string.IsNullOrEmpty(spec.FileLabel) || e.File.FileLabel.Contains(spec.FileLabel))
            .Where(e => string.IsNullOrEmpty(spec.Location) || e.Location.Contains(spec.Location))
            .Where(e => string.IsNullOrEmpty(spec.Address) || e.Address.Contains(spec.Address))
            .Where(e => string.IsNullOrEmpty(spec.City) || e.City.Contains(spec.City))
            .Where(e => string.IsNullOrEmpty(spec.State) || e.State.Contains(spec.State))
            .Where(e => string.IsNullOrEmpty(spec.PostalCode) || e.PostalCode.Contains(spec.PostalCode));

        private static IOrderedQueryable<Facility> OrderFacilityQuery(
            IQueryable<Facility> included, FacilitySort sortBy) =>
            sortBy switch
            {
                FacilitySort.NameDesc => included.OrderByDescending(e => e.Name)
                    .ThenByDescending(e => e.FacilityNumber),
                FacilitySort.Address => included.OrderBy(e => e.Address)
                    .ThenBy(e => e.City).ThenBy(e => e.State)
                    .ThenBy(e => e.Name),
                FacilitySort.AddressDesc => included.OrderByDescending(e => e.Address)
                    .ThenByDescending(e => e.City).ThenByDescending(e => e.State)
                    .ThenByDescending(e => e.Name),
                FacilitySort.FacilityNumber => included.OrderBy(e => e.FacilityNumber)
                    .ThenBy(e => e.Name),
                FacilitySort.FacilityNumberDesc => included.OrderByDescending(e => e.FacilityNumber)
                    .ThenByDescending(e => e.Name),
                FacilitySort.FileLabel => included.OrderBy(e => e.File.FileLabel)
                    .ThenBy(e => e.Name),
                FacilitySort.FileLabelDesc => included.OrderByDescending(e => e.File.FileLabel)
                    .ThenByDescending(e => e.Name),
                // FacilitySort.Name
                _ => included.OrderBy(e => e.Name)
                    .ThenBy(e => e.FacilityNumber)
            };

        public Task<int> CountAsync(FacilitySpec spec)
        {
            var queried = QueryFacilities(spec);
            return queried.CountAsync();
        }

        public async Task<PaginatedList<FacilitySummaryDto>> GetFacilityPaginatedListAsync(
            FacilitySpec spec, int pageNumber, int pageSize)
        {
            Prevent.NegativeOrZero(pageNumber, nameof(pageNumber));
            Prevent.NegativeOrZero(pageSize, nameof(pageSize));

            var queried = QueryFacilities(spec);

            var included = queried
                .Include(e => e.File)
                .Include(e => e.RetentionRecords)
                .Include(e => e.FacilityType);

            var ordered = OrderFacilityQuery(included, spec.SortBy);

            var items = await ordered
                .Skip((pageNumber - 1) * pageSize).Take(pageSize)
                .Select(e => new FacilitySummaryDto(e))
                .ToListAsync();

            var cabinets = await _context.GetCabinetListAsync(false);
            foreach (var item in items)
            {
                bool test = item.FacilityType.Name != "RN" || !item.FileLabel.IsNullOrEmpty();

                item.Cabinets = cabinets.GetCabinetsForFile(item.FileLabel, test);
            }

            var totalCount = await queried.CountAsync();
            return new PaginatedList<FacilitySummaryDto>(items, totalCount, pageNumber, pageSize);
        }

        public async Task<IReadOnlyList<FacilityDetailDto>> GetFacilityDetailListAsync(FacilitySpec spec)
        {
            var queried = QueryFacilities(spec);

            var included = queried
                .Include(e => e.County)
                .Include(e => e.FacilityStatus)
                .Include(e => e.FacilityType)
                .Include(e => e.BudgetCode)
                .Include(e => e.OrganizationalUnit)
                .Include(e => e.ComplianceOfficer)
                .Include(e => e.File)
                .Include(e => e.RetentionRecords);

            var ordered = OrderFacilityQuery(included, spec.SortBy);

            var items = await ordered.Select(e => new FacilityDetailDto(e)).ToListAsync();

            if (!spec.ShowPendingOnly)
            {
                var cabinets = await _context.GetCabinetListAsync(false);
                foreach (var item in items)
                {
                    item.Cabinets = cabinets.GetCabinetsForFile(item.FileLabel);
                }
            }

            return items;
        }

        public async Task<IReadOnlyList<FacilityMapSummaryDto>> GetFacilityListAsync(FacilityMapSpec spec)
        {
            var conn = _context.Database.GetDbConnection();

            return (await conn.QueryAsync<FacilityMapSummaryDto>("dbo.getNearbyFacilities",
                new { Active = !spec.ShowDeleted, spec.Latitude, spec.Longitude, spec.Radius, spec.FacilityTypeId },
                commandType: CommandType.StoredProcedure)).ToList();
        }

        public async Task<IEnumerable<RetentionRecordDetailDto>> GetRetentionRecordsListAsync(FacilitySpec spec)
        {
            var queried = QueryFacilities(spec);

            var included = queried
                .Include(e => e.RetentionRecords);

            var ordered = OrderFacilityQuery(included, spec.SortBy);

            // create a List<RetentionRecord>
            var retentionRecordsList = await ordered.SelectMany(e => e.RetentionRecords).ToListAsync();

            // convert the List<RetentionRecord> to IEnumerable<RetentionRecordDetailDto>
            var returnList = from retentionRecord in retentionRecordsList
                             select new RetentionRecordDetailDto(retentionRecord);

            return returnList;
        }

        public Task<Guid> CreateFacilityAsync(FacilityCreateDto newFacility, bool newFileId = true)
        {
            if (string.IsNullOrWhiteSpace(newFacility.FacilityNumber))
            {
                throw new ArgumentException("Facility Number is required.");
            }

            if (string.IsNullOrWhiteSpace(newFacility.FileLabel) &&
                Data.Counties.TrueForAll(e => e.Id != newFacility.CountyId))
            {
                throw new ArgumentException($"County ID {newFacility.CountyId} does not exist.");
            }

            return CreateFacilityInternalAsync(newFacility, newFileId);
        }

        private async Task<Guid> CreateFacilityInternalAsync(FacilityCreateDto newFacility, bool newFileId)
        {
            if (await FacilityNumberExists(newFacility.FacilityNumber))
            {
                throw new ArgumentException($"Facility Number '{newFacility.FacilityNumber}' already exists.");
            }

            File file;

            if (string.IsNullOrWhiteSpace(newFacility.FileLabel) && newFileId)
            {
                // If File Label is empty, generate new File
                // Release Notificatiopns are allowed to have no File Label
                file = await CreateFileInternal(newFacility.CountyId);
            }
            else
            {
                // Otherwise, if File Label is provided, make sure it exists
                file = await _context.Files.SingleOrDefaultAsync(e => e.FileLabel == newFacility.FileLabel);
                if (file == null && newFileId) throw new ArgumentException($"File Label {newFacility.FileLabel} does not exist.");
            }

            var newFac = new Facility(newFacility)
            {
                File = file
            };

            await _context.Facilities.AddAsync(newFac);
            await _context.SaveChangesAsync();

            return newFac.Id;
        }

        public Task UpdateFacilityAsync(Guid id, FacilityEditDto facilityUpdates)
        {
            if (string.IsNullOrWhiteSpace(facilityUpdates.FacilityNumber))
            {
                throw new ArgumentException("Facility Number is required.");
            }

            if (string.IsNullOrWhiteSpace(facilityUpdates.FileLabel) &&
                Data.Counties.TrueForAll(e => e.Id != facilityUpdates.CountyId))
            {
                throw new ArgumentException($"County ID {facilityUpdates.CountyId} does not exist.");
            }

            return UpdateFacilityInternalAsync(id, facilityUpdates);
        }

        private async Task UpdateFacilityInternalAsync(Guid id, FacilityEditDto facilityUpdates)
        {
            var facility = await _context.Facilities.FindAsync(id);

            if (facility == null)
            {
                throw new ArgumentException("Facility ID not found.", nameof(id));
            }

            if (string.IsNullOrWhiteSpace(facilityUpdates.FileLabel) && facilityUpdates.FacilityTypeName != "RN")
            {
                // Generate new File if File Label is empty
                // Release Notificatiopns are allowed to have no File Label
                facility.File = await CreateFileInternal(facilityUpdates.CountyId);
            }
            else
            {
                // Otherwise, if File Label is provided and different from existing label, make sure it exists
                var oldFile = await _context.Files.FindAsync(facility.FileId);
                if (oldFile is null || facilityUpdates.FileLabel != oldFile.FileLabel)
                {
                    var file = await _context.Files.SingleOrDefaultAsync(e => e.FileLabel == facilityUpdates.FileLabel);
                    if (file == null && facilityUpdates.FacilityTypeName != "RN")
                        throw new ArgumentException($"File Label {facilityUpdates.FileLabel} does not exist.");
                    facility.File = file;
                    facility.FileId = file?.Id;
                }
            }

            facility.CountyId = facilityUpdates.CountyId;
            facility.FacilityNumber = facilityUpdates.FacilityNumber;
            facility.Name = facilityUpdates.Name;
            facility.FacilityStatusId = facilityUpdates.FacilityStatusId;
            facility.FacilityTypeId = facilityUpdates.FacilityTypeId;
            facility.BudgetCodeId = facilityUpdates.BudgetCodeId;
            facility.OrganizationalUnitId = facilityUpdates.OrganizationalUnitId;
            facility.ComplianceOfficerId = facilityUpdates.ComplianceOfficerId;
            facility.Location = facilityUpdates.Location;
            facility.Address = facilityUpdates.Address;
            facility.City = facilityUpdates.City;
            facility.State = facilityUpdates.State;
            facility.PostalCode = facilityUpdates.PostalCode;
            facility.Latitude = facilityUpdates.Latitude;
            facility.Longitude = facilityUpdates.Longitude;
            // New for all Facilities
            facility.HasERecord = facilityUpdates.HasERecord;
            facility.Comments = facilityUpdates.Comments;
            // added for release notifications
            facility.HSInumber = facilityUpdates.HSInumber;
            facility.DeterminationLetterDate = facilityUpdates.DeterminationLetterDate;
            facility.PreRQSMcleanup = facilityUpdates.PreRQSMcleanup;
            facility.ImageChecked = facilityUpdates.ImageChecked;
            facility.DeferredOnSiteScoring = facilityUpdates.DeferredOnSiteScoring;
            facility.AdditionalDataRequested = facilityUpdates.AdditionalDataRequested;
            facility.VRPReferral = facilityUpdates.VRPReferral;
            facility.RNDateReceived = facilityUpdates.RNDateReceived;
            facility.HistoricalUnit = facilityUpdates.HistoricalUnit;
            facility.HistoricalComplianceOfficer = facilityUpdates.HistoricalComplianceOfficer;
            // ******************
            facility.IsRetained = facilityUpdates.IsRetained;

            await _context.SaveChangesAsync();
        }

        private async Task<File> CreateFileInternal(int countyId)
        {
            // Generate new File
            var nextSequence = await GetNextSequenceForCountyAsync(countyId);
            var file = new File(countyId, nextSequence);
            await _context.Files.AddAsync(file);
            return file;
        }

        public async Task<int> GetNextSequenceForCountyAsync(int countyId)
        {
            var countyString = File.CountyString(countyId);
            var allSequencesForCounty = await _context.Files.AsNoTracking()
                .Where(e => e.FileLabel.StartsWith(countyString))
                .Select(e => int.Parse(e.FileLabel.Substring(4, 4)))
                .ToListAsync();
            return allSequencesForCounty.Count == 0 ? 1 : allSequencesForCounty.Max() + 1;
        }

        public async Task DeleteFacilityAsync(Guid id)
        {
            var facility = await _context.Facilities.FindAsync(id);

            if (facility == null)
            {
                throw new ArgumentException("Facility ID not found.", nameof(id));
            }

            facility.Active = false;
            await _context.SaveChangesAsync();
        }

        public async Task UndeleteFacilityAsync(Guid id)
        {
            var facility = await _context.Facilities.FindAsync(id);

            if (facility == null)
            {
                throw new ArgumentException("Facility ID not found.", nameof(id));
            }

            facility.Active = true;
            await _context.SaveChangesAsync();
        }

        public Task<bool> FacilityNumberExists(string facilityNumber, Guid? ignoreId = null) =>
            _context.Facilities.AnyAsync(e =>
                e.FacilityNumber == facilityNumber
                && (!ignoreId.HasValue || e.Id != ignoreId.Value));

        public Task<bool> FileLabelExists(string fileLabel) =>
            _context.Files.AnyAsync(e => e.FileLabel == fileLabel);

        // Retention Records

        public Task<bool> RetentionRecordExistsAsync(Guid id) =>
            _context.RetentionRecords.AnyAsync(e => e.Id == id);

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