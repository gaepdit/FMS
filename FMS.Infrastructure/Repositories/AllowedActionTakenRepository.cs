using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Domain.Utils;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS.Infrastructure.Repositories
{
    public class AllowedActionTakenRepository : IAllowedActionTakenRepository
    {
        public readonly FmsDbContext _context;
        public AllowedActionTakenRepository(FmsDbContext context) => _context = context;

        public Task<bool> AllowedActionTakenExistsAsync(Guid id) =>
            _context.AllowedActionsTaken.AnyAsync(e => e.Id == id);

        public Task<bool> AllowedActionTakenExistsAsync(Guid eventTypeId, Guid actionTakenId) =>
            _context.AllowedActionsTaken.AnyAsync(e => e.EventTypeId == eventTypeId && e.ActionTakenId == actionTakenId);

        public async Task<AllowedActionTakenSpec> GetAllowedActionTakenByIdAsync(Guid? id)
            {
            if (!id.HasValue)
            {
                return null;
            }
            var allowedActionTaken =  await _context.AllowedActionsTaken.AsNoTracking()
                .Where(e => e.Id == id.Value)
                .Include(e => e.EventType)
                .Include(e => e.ActionTaken)
                .Select(e => new AllowedActionTaken(e))
                .SingleOrDefaultAsync();

            if (allowedActionTaken == null)
            {
                return null;
            }
            return new AllowedActionTakenSpec()
            {
                Id = allowedActionTaken.Id,
                EventTypeId = allowedActionTaken.EventTypeId,
                ActionTakenId = allowedActionTaken.ActionTakenId,
                EventTypeName = allowedActionTaken.EventType.Name,
                ActionTakenName = allowedActionTaken.ActionTaken.Name,
                Active = allowedActionTaken.Active
            };
        }

        public async Task<AllowedActionTakenSpec> GetAllowedActionTakenByAATIdAsync(Guid? id)
        {
            if (!id.HasValue)
            {
                return null;
            }
            if (!await AllowedActionTakenExistsAsync(id.Value))
            {
                return null;
            }
            AllowedActionTaken allowedActionTaken = await _context.AllowedActionsTaken
                .AsNoTracking()
                .Include(e => e.EventType)
                .Include(e => e.ActionTaken)
                .SingleOrDefaultAsync(e => e.Id == id.Value);

            return new AllowedActionTakenSpec()
            {
                Id = allowedActionTaken.Id,
                EventTypeId = allowedActionTaken.EventTypeId,
                ActionTakenId = allowedActionTaken.ActionTakenId,
                EventTypeName = allowedActionTaken.EventType.Name,
                ActionTakenName = allowedActionTaken.ActionTaken.Name,
                Active = allowedActionTaken.Active
            };
        }

        public async Task<AllowedActionTaken> GetAllowedActionTakenAsync(Guid eventTypeId, Guid actionTakenId)
        {
             return await _context.AllowedActionsTaken.AsNoTracking()
                .Where(e => e.EventTypeId == eventTypeId && e.ActionTakenId == actionTakenId)
                .Select(e => new AllowedActionTaken(e))
                .SingleOrDefaultAsync();
        }

        public async Task<IList<AllowedActionTakenSpec>> GetAllowedActionTakenListAsync(Guid eventTypeId)
        {
            return await _context.AllowedActionsTaken.AsNoTracking()
                .Include(e => e.EventType)
                .Include(e => e.ActionTaken)
                .Where(e => e.EventTypeId == eventTypeId)
                .OrderByDescending(e => e.Active)
                .ThenBy(e => e.ActionTaken.Name)
                .Select(e => new AllowedActionTakenSpec()
                {
                    Id = e.Id,
                    EventTypeId = e.EventTypeId,
                    ActionTakenId = e.ActionTakenId,
                    EventTypeName = e.EventType.Name,
                    EventTypeActive = e.EventType.Active,
                    ActionTakenName = e.ActionTaken.Name,
                    ActionTakenActive = e.ActionTaken.Active,
                    Active = e.Active
                })
                .ToListAsync();
        }

        public async Task<Guid> CreateAllowedActionTakenAsync(AllowedActionTakenSpec allowedActionTaken)
        {
            Prevent.Null(allowedActionTaken, nameof(allowedActionTaken));
            Prevent.NullOrEmpty(allowedActionTaken.EventTypeId, nameof(allowedActionTaken.EventTypeId));
            Prevent.NullOrEmpty(allowedActionTaken.ActionTakenId, nameof(allowedActionTaken.ActionTakenId));

            if (await AllowedActionTakenExistsAsync(allowedActionTaken.EventTypeId, allowedActionTaken.ActionTakenId))
            {
                throw new ArgumentException($"Allowed Action Taken already exists.");
            }
            var newAllowedActionTaken = new AllowedActionTaken
            {
                ActionTakenId = allowedActionTaken.ActionTakenId,
                EventTypeId = allowedActionTaken.EventTypeId,
                Active = true
            };
            await _context.AllowedActionsTaken.AddAsync(newAllowedActionTaken);
            await _context.SaveChangesAsync();
            return newAllowedActionTaken.Id;
        }

        //public async Task<Guid> UpdateAllowedActionTakenAsync(Guid actionTakenId, Guid eventTypeId)
        //{
        //    if (await AllowedActionTakenExistsAsync(eventTypeId, actionTakenId))
        //    {
        //        return await UpdateAllowedActionTakenInternalAsync(actionTakenId, eventTypeId);
        //    }
        //    else
        //    {
        //        return await UpdateAllowedActionTakenInternalAsync(actionTakenId, eventTypeId, true);
        //    }
        //}

        //private async Task<Guid> UpdateAllowedActionTakenInternalAsync(Guid actionTakenId, Guid eventTypeId, bool? createNew = false)
        //{
        //    if(createNew == true)
        //    {
        //        var newAllowedActionTaken = new AllowedActionTaken
        //        {
        //            ActionTakenId = actionTakenId,
        //            EventTypeId = eventTypeId,
        //            Active = true
        //        };
        //        await _context.AllowedActionsTaken.AddAsync(newAllowedActionTaken);
        //        await _context.SaveChangesAsync();
        //        return newAllowedActionTaken.Id;
        //    }
        //    else
        //    {
        //        var existingAllowedActionTaken = await GetAllowedActionTakenAsync(eventTypeId, actionTakenId);

        //        if (existingAllowedActionTaken == null)
        //        {
        //            throw new ArgumentException($"Allowed Action Taken with Id {existingAllowedActionTaken.Id} does not exist.");
        //        }

        //        existingAllowedActionTaken.Active = !existingAllowedActionTaken.Active;

        //        _context.AllowedActionsTaken.Update(existingAllowedActionTaken);
        //        await _context.SaveChangesAsync();
        //        return existingAllowedActionTaken.Id;
        //    }
        //}

        public async Task<Guid> DeleteAllowedActionTakenAsync(Guid? id)
        {
            if(!await AllowedActionTakenExistsAsync(id.Value))
            {
                throw new ArgumentException("No Allowed Action Taken records found.");
            }
            else
            {
                if (await GetAllowedActionTakenByAATIdAsync(id.Value) != null)
                {
                    var existingAllowedActionTaken = await _context.AllowedActionsTaken.FindAsync(id.Value);
                    _context.AllowedActionsTaken.Remove(existingAllowedActionTaken);
                    await _context.SaveChangesAsync();
                    return id.Value;
                }
                else {   
                    throw new ArgumentException($"Allowed Action Taken with Id {id.Value} does not exist.");
                }
            }
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
        ~AllowedActionTakenRepository()
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
