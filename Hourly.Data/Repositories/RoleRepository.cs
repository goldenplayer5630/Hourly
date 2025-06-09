using Hourly.Abstractions.Entities;
using Hourly.Abstractions.Repositories;
using Hourly.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Hourly.Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IRole?> GetById(Guid roleId)
        {
            return await _context.Roles
                .Include(r => r.Users)
                .FirstOrDefaultAsync(r => r.Id == roleId);
        }

        public async Task<IEnumerable<IRole>> GetAll()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<IRole> Create(IRole role)
        {
            await _context.Roles.AddAsync(role);
            var result = await _context.SaveChangesAsync();
            return (result > 0 ? role : null) ?? throw new InvalidOperationException();
        }

        public async Task<IRole> Update(IRole role)
        {
            var existingRole = await _context.Roles.FindAsync(role.Id);
            if (existingRole == null)
            {
                throw new EntityNotFoundException("Role not found!");
            }

            _context.Entry(existingRole).CurrentValues.SetValues(role);
            var result = await _context.SaveChangesAsync();

            return (result > 0 ? role : null) ?? throw new InvalidOperationException();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
