using Hourly.Abstractions.Repositories;
using Hourly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Data.Repositories
{
    public class LockedMonthRepository : ILockedMonthRepository
    {
        private readonly AppDbContext _context;

        public LockedMonthRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LockedMonth>> GetAll()
        {
            return await _context.LockedMonths
                .Include(lm => lm.UserContract)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        public async Task<LockedMonth?> GetById(Guid lockedMonthId)
        {
            return await _context.LockedMonths
                .Include(lm => lm.UserContract)
                .FirstOrDefaultAsync(lm => lm.Id == lockedMonthId)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<LockedMonth>> Filter(Guid? userContractId, int? year, int? month)
        {
            var query = _context.LockedMonths
                .Include(lm => lm.UserContract)
                .AsQueryable();

            if (userContractId.HasValue)
                query = query.Where(lm => lm.UserContractId == userContractId.Value);
            if (year.HasValue)
                query = query.Where(lm => lm.Year == year.Value);
            if (month.HasValue)
                query = query.Where(lm => lm.Month == month.Value);

            return await query.ToListAsync().ConfigureAwait(false);
        }

        public async Task<LockedMonth> Create(LockedMonth lockedMonth)
        {
            _context.LockedMonths.Add(lockedMonth);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return lockedMonth;
        }

        public async Task<LockedMonth> Update(LockedMonth lockedMonth)
        {
            _context.LockedMonths.Update(lockedMonth);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return lockedMonth;
        }

        public async Task Delete(Guid lockedMonthId)
        {
            var entity = await _context.LockedMonths.FindAsync(lockedMonthId).ConfigureAwait(false);
            if (entity != null)
            {
                _context.LockedMonths.Remove(entity);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
        }

    }
}
