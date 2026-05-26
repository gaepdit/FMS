using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Domain.Utils;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FMS.Infrastructure.Repositories
{
    public class UserPositionRepository : IUserPositionRepository
    {
        private readonly FmsDbContext _context;
        public UserPositionRepository(FmsDbContext context) => _context = context;

        public async Task<bool> UserPositionExistsAsync(Guid id) =>
            await _context.UserPositions.AnyAsync(e => e.Id == id);

        public async Task<bool> UserPositionNameExistsAsync(string name, Guid? ignoreId = null) =>
            await _context.UserPositions.AnyAsync(e => e.Name == name && (!ignoreId.HasValue || e.Id != ignoreId.Value));

        public async Task<UserPositionEditDto> GetUserPositionAsync(Guid id){
            var userPosition = await _context.UserPositions.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id);

            return userPosition == null ? null : new UserPositionEditDto(userPosition);
        }

        public async Task<IReadOnlyList<UserPositionSummaryDto>> GetUserPositionListAsync() =>
            await _context.UserPositions.AsNoTracking()
            .OrderByDescending(e => e.Active)
            .ThenBy(e => e.Name)
            .Select(e => new UserPositionSummaryDto(e))
            .ToListAsync();

        public async Task<Guid> CreateUserPositionAsync(UserPositionCreateDto userPosition)
        {
            Prevent.Null(userPosition, nameof(userPosition));
            Prevent.NullOrEmpty(userPosition.Name, nameof(userPosition.Name));

            return await CreateUserPositionInternalAsync(userPosition);
        }

        private async Task<Guid> CreateUserPositionInternalAsync(UserPositionCreateDto userPosition)
        {
            if (await UserPositionNameExistsAsync(userPosition.Name))
            {
                throw new ArgumentException($"User Position Name: {userPosition.Name} Already Exists.");
            }

            var newUserPosition = new UserPosition(userPosition);

            await _context.UserPositions.AddAsync(newUserPosition);
            await _context.SaveChangesAsync();
            return newUserPosition.Id;
        }

        public async Task UpdateUserPositionAsync(Guid Id, UserPositionEditDto userPositionUpdates)
        {
            Prevent.Null(userPositionUpdates, nameof(userPositionUpdates));
            Prevent.NullOrEmpty(userPositionUpdates.Name, nameof(userPositionUpdates.Name));

            if (!await UserPositionExistsAsync(Id))
            {
                throw new ArgumentException($"User Position with Id {Id} does not exist.");
            }

            if (await UserPositionNameExistsAsync(userPositionUpdates.Name, Id))
            {
                throw new ArgumentException($"User Position Name: {userPositionUpdates.Name} Already Exists.");
            }
            var existingUserPosition = await _context.UserPositions.FindAsync(Id);

            existingUserPosition.Name = userPositionUpdates.Name;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserPositionStatusAsync(Guid id, bool active)
        {
            if (!await UserPositionExistsAsync(id))
            {
                throw new ArgumentException($"User Position with Id {id} does not exist.");
            }
            var existingUserPosition = await _context.UserPositions.FindAsync(id);

            existingUserPosition.Active = active;

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
        ~UserPositionRepository()
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
