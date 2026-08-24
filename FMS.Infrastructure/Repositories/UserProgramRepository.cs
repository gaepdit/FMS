using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Domain.Utils;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FMS.Infrastructure.Repositories
{
    public class UserProgramRepository : IUserProgramRepository
    {
        private readonly FmsDbContext _context;
        public UserProgramRepository(FmsDbContext context) => _context = context;

        public async Task<bool> UserProgramExistsAsync(Guid id) =>
            await _context.UserPrograms.AnyAsync(e => e.Id == id);

        public async Task<bool> UserProgramNameExistsAsync(string name, Guid? ignoreId = null) =>
            await _context.UserPrograms.AnyAsync(e => e.Name == name && (!ignoreId.HasValue || e.Id != ignoreId.Value));

        public async Task<bool> UserProgramDescriptionExistsAsync(string description, Guid? ignoreId = null) =>
            await _context.UserPrograms.AnyAsync(e => e.Description == description && (!ignoreId.HasValue || e.Id != ignoreId.Value));

        public async Task<UserProgramEditDto> GetUserProgramAsync(Guid id)
        {
            var userProgram = await _context.UserPrograms.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id);

            return userProgram == null ? null : new UserProgramEditDto(userProgram);
        }

        public async Task<UserProgram> GetUserProgramByNameAsync(string name)
        {
            var userProgram = await _context.UserPrograms.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Name == name);
            return userProgram == null ? null : userProgram;
        }

        public async Task<IReadOnlyList<UserProgramSummaryDto>> GetUserProgramListAsync() =>
            await _context.UserPrograms.AsNoTracking()
            .OrderByDescending(e => e.Active)
            .ThenBy(e => e.Name)
            .Select(e => new UserProgramSummaryDto(e))
            .ToListAsync();

        public async Task<Guid> CreateUserProgramAsync(UserProgramCreateDto userProgram)
        {
            Prevent.Null(userProgram, nameof(userProgram));
            Prevent.NullOrEmpty(userProgram.Name, nameof(userProgram.Name));

            return await CreateUserProgramInternalAsync(userProgram);
        }

        private async Task<Guid> CreateUserProgramInternalAsync(UserProgramCreateDto userProgram)
        {
            if (await UserProgramNameExistsAsync(userProgram.Name))
            {
                throw new ArgumentException($"User Program Name: {userProgram.Name} Already Exists.");
            }

            if (await UserProgramDescriptionExistsAsync(userProgram.Description))
            {
                throw new ArgumentException($"User Program Description: {userProgram.Description} Already Exists.");
            }

            var newUserProgram = new UserProgram(userProgram);

            await _context.UserPrograms.AddAsync(newUserProgram);
            await _context.SaveChangesAsync();
            return newUserProgram.Id;
        }

        public async Task UpdateUserProgramAsync(Guid Id, UserProgramEditDto userProgramUpdates)
        {
            Prevent.Null(userProgramUpdates, nameof(userProgramUpdates));
            Prevent.NullOrEmpty(userProgramUpdates.Name, nameof(userProgramUpdates.Name));

            if (!await UserProgramExistsAsync(Id))
            {
                throw new ArgumentException($"User Program with Id {Id} does not exist.");
            }

            if (await UserProgramNameExistsAsync(userProgramUpdates.Name, Id))
            {
                throw new ArgumentException($"User Program Name: {userProgramUpdates.Name} Already Exists.");
            }
            var existingUserProgram = await _context.UserPrograms.FindAsync(Id);

            existingUserProgram.Name = userProgramUpdates.Name;
            existingUserProgram.Description = userProgramUpdates.Description;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserProgramStatusAsync(Guid id, bool active)
        {
            if (!await UserProgramExistsAsync(id))
            {
                throw new ArgumentException($"User Program with Id {id} does not exist.");
            }
            var existingUserProgram = await _context.UserPrograms.FindAsync(id);

            existingUserProgram.Active = active;

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
        ~UserProgramRepository()
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
