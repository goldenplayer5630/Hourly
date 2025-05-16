using Hourly.Abstractions.Repositories;
using Hourly.Shared.Entities;
using Hourly.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Data.Repositories
{
    public class UserContractRepository : IUserContractRepository
    {
        private readonly AppDbContext _context;

        public UserContractRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserContract?> GetById(Guid userContractId)
        {
            return await _context.UserContracts
                .Include(r => r.User)
                .ThenInclude(u => u.Role)
                .FirstOrDefaultAsync(r => r.Id == userContractId);
        }

        public async Task<IEnumerable<UserContract>> GetAll()
        {
            return await _context.UserContracts
                .Include(u => u.User)
                .ThenInclude(u => u.Role)
                .ToListAsync();
        }

        public async Task<UserContract> Create(UserContract userContract)
        {
            await _context.UserContracts.AddAsync(userContract);
            var result = await _context.SaveChangesAsync();
            return (result > 0 ? userContract : null) ?? throw new InvalidOperationException();
        }

        public async Task<UserContract> Update(UserContract userContract)
        {
            var existingUserContract = await _context.UserContracts.FindAsync(userContract.Id);
            if (existingUserContract == null)
            {
                throw new EntityNotFoundException("UserContract not found!");
            }

            _context.Entry(existingUserContract).CurrentValues.SetValues(userContract);
            var result = await _context.SaveChangesAsync();

            return (result > 0 ? userContract : null) ?? throw new InvalidOperationException();
        }

        public async Task Delete(Guid userContractId)
        {
            var existingUserContract = await _context.UserContracts.FindAsync(userContractId);
            if (existingUserContract == null)
            {
                throw new EntityNotFoundException("UserContract not found!");
            }
            _context.UserContracts.Remove(existingUserContract);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
