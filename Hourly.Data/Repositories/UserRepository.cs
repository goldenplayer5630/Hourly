using Hourly.Abstractions.Entities;
using Hourly.Abstractions.Repositories;
using Hourly.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;


namespace Hourly.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IUser?> GetById(Guid userId)
        {
            return await _context.Users
                .Include(u => u.Role)
                .Include(u => u.Department)
                .Include("_gitCommits")
                .Include("_userContracts")
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<IEnumerable<IUser>> GetAll()
        {
            return await _context.Users
                .Include(u => u.Role)
                .ToListAsync();
        }

        public async Task<IUser> Create(IUser user)
        {
            await _context.Users.AddAsync(user);
            var result = await _context.SaveChangesAsync();
            return (result > 0 ? user : null) ?? throw new InvalidOperationException();
        }

        public async Task<IUser> Update(IUser user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser == null)
            {
                throw new EntityNotFoundException("User not found!");
            }

            _context.Entry(existingUser).CurrentValues.SetValues(user);
            var result = await _context.SaveChangesAsync();

            return (result > 0 ? user : null) ?? throw new InvalidOperationException();
        }

        public async Task Delete(Guid userId)
        {
            var existingUser = await _context.Users.FindAsync(userId);
            if (existingUser == null)
            {
                throw new EntityNotFoundException("User not found!");
            }

            _context.Users.Remove(existingUser);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
