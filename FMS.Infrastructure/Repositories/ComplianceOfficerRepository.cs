using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Domain.Services;
using FMS.Domain.Utils;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FMS.Infrastructure.Repositories
{
    public class ComplianceOfficerRepository : IComplianceOfficerRepository
    {
        private readonly FmsDbContext _context;
        public ComplianceOfficerRepository(FmsDbContext context) => _context = context;

        public Task<bool> ComplianceOfficerIdExistsAsync(Guid id) =>
            _context.ComplianceOfficers.AnyAsync(e => e.Id == id);

        public async Task<ComplianceOfficerDetailDto> GetComplianceOfficerAsync(Guid id)
        {
            var complianceOfficer = await _context.ComplianceOfficers.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id);

            return complianceOfficer == null ? null : new ComplianceOfficerDetailDto(complianceOfficer);
        }

        public async Task<ComplianceOfficerSummaryDto> GetComplianceOfficerSummaryAsync(Guid id)
        {
            var complianceOfficer = await _context.ComplianceOfficers.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id);

            if (complianceOfficer == null)
            {
                return null;
            }

            var returnCo = new ComplianceOfficerSummaryDto(complianceOfficer);

            var userCo = await _context.Users.AsNoTracking()
                    .Where(e => e.GivenName == complianceOfficer.GivenName && e.FamilyName == complianceOfficer.FamilyName)
                    .Select(e => new UserView(e))
                    .FirstOrDefaultAsync();

            if(userCo == null)
            {
                return null;
            }

            returnCo.UserInfo = userCo;

            return returnCo;
        }

        public async Task<IReadOnlyList<ComplianceOfficerSummaryDto>> GetComplianceOfficerListAsync()
        {
            var co = await _context.ComplianceOfficers.AsNoTracking()
                .OrderByDescending(e => e.Active)
                .ThenBy(e => e.FamilyName)
                .ThenBy(e => e.GivenName)
                .Select(e => new ComplianceOfficerSummaryDto(e))
                .ToListAsync();

            var userCo = _context.Users.AsEnumerable()
                .Where(e => e.Active)
                .Where(e => co.Any(c => c.GivenName == e.GivenName && c.FamilyName == e.FamilyName))
                .Select(e => new UserView(e))
                .ToList();

            foreach (var item in co)
            {
                item.UserInfo = userCo.FirstOrDefault(u => u.GivenName == item.GivenName && u.FamilyName == item.FamilyName);
            }

            return co;
        }

        public async Task<List<Guid>> GetComplianceOfficerListByUnitAsync(Guid UnitId)
        {
            var UnitUsers = await _context.Users.AsNoTracking()
                .Where(e => e.Active)
                .Where(e => e.UserUnit.Id == UnitId)
                .ToListAsync();

            var UnitCOs = await _context.ComplianceOfficers.AsAsyncEnumerable()
                .Where(e => UnitUsers.Any(u => u.GivenName == e.GivenName && u.FamilyName == e.FamilyName))
                .OrderByDescending(e => e.Active)
                .ThenBy(e => e.FamilyName)
                .ThenBy(e => e.GivenName)
                .Select(e => e.Id)
                .ToListAsync();

            return UnitCOs;
        }

        public async Task<List<Guid>> GetComplianceOfficerListByProgramAsync(Guid UnitId, bool IncludeInactive = false)
        {
            var program = await _context.OrganizationalUnits.AsNoTracking()
               .Where(e => e.Active || IncludeInactive)
               .Where(e => e.Id == UnitId)
               .Select(e => e.UserProgram)
               .SingleOrDefaultAsync();

            if (program == null)
            {
                return null;
            }

            var programUsers = await _context.Users.AsNoTracking()
                .Where(e => e.Active || IncludeInactive)
                .Where(e => e.UserProgram.Id == program.Id)
                .ToListAsync();

            var programCOs = await _context.ComplianceOfficers.AsAsyncEnumerable()
                .Where(e => programUsers.Any(u => u.GivenName == e.GivenName && u.FamilyName == e.FamilyName))
                .OrderByDescending(e => e.Active)
                .ThenBy(e => e.FamilyName)
                .ThenBy(e => e.GivenName)
                .Select(e => e.Id)
                .ToListAsync();

            return programCOs;
        }

        public Task<Guid?> TryCreateComplianceOfficerAsync(ComplianceOfficerCreateDto complianceOfficer)
        {
            Prevent.Null(complianceOfficer, nameof(complianceOfficer));
            Prevent.Null(complianceOfficer.Email, nameof(complianceOfficer.Email));

            return CreateComplianceOfficerInternalAsync(complianceOfficer);
        }

        private async Task<Guid?> CreateComplianceOfficerInternalAsync(ComplianceOfficerCreateDto complianceOfficer)
        {
            if (await _context.ComplianceOfficers.AnyAsync(e => e.Email == complianceOfficer.Email))
            {
                return null;
            }

            var newCO = new ComplianceOfficer(complianceOfficer);

            await _context.ComplianceOfficers.AddAsync(newCO);
            await _context.SaveChangesAsync();

            return newCO.Id;
        }

        public async Task UpdateComplianceOfficerStatusAsync(Guid id, bool active)
        {
            var complianceOfficer = await _context.ComplianceOfficers.FindAsync(id);

            if (complianceOfficer == null)
            {
                throw new ArgumentException("Compliance Officer ID not found");
            }

            complianceOfficer.Active = active;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteComplianceOfficerAsync(Guid id)
        {
            var complianceOfficer = await _context.ComplianceOfficers.FindAsync(id);
            if (complianceOfficer == null)
            {
                throw new ArgumentException("Compliance Officer ID not found");
            }
            _context.ComplianceOfficers.Remove(complianceOfficer);
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
        ~ComplianceOfficerRepository()
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