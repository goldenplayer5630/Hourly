using Hourly.Abstractions.Exceptions;
using Hourly.Abstractions.Repositories;
using Hourly.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hourly.Data.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _context;
        public DepartmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<entity?> GetById(Guid departmentId)
        {
            var ressult = await _context.Departments.FindAsync(departmentId);

            if (ressult == null)
            {
                throw new EntityNotFoundException("Department not found!");
            }

            return ressult;
        }

        public async Task<IEnumerable<entity>> GetAll()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<entity> Create(entity department)
        {
            await _context.Departments.AddAsync(department);
            var result = await _context.SaveChangesAsync();
            return (result > 0 ? department : null) ?? throw new InvalidOperationException();

        }

        public async Task<entity> AddUser(Guid departmentId, Guid userId)
        {
            var department = await _context.Departments.FindAsync(departmentId);

            if (department == null)
            {
                throw new EntityNotFoundException("Department not found!");
            }

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new EntityNotFoundException("User not found!");
            }

            department.Users.Add(user);
            var result = await _context.SaveChangesAsync();
            return (result > 0 ? department : null) ?? throw new InvalidOperationException();
        }

        public async Task<entity> RemoveUser(Guid departmentId, Guid userId)
        {
            var department = await _context.Departments.FindAsync(departmentId);
            if (department == null)
            {
                throw new EntityNotFoundException("Department not found!");
            }

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new EntityNotFoundException("User not found!");
            }

            department.Users.Remove(user);
            var result = await _context.SaveChangesAsync();

            return (result > 0 ? department : null) ?? throw new InvalidOperationException();
        }

        public async Task<entity> Update(entity department)
        {
            var existingDepartment = await _context.Departments.FindAsync(department.Id);
            if (existingDepartment == null)
            {
                throw new EntityNotFoundException("Department not found!");
            }

            _context.Entry(existingDepartment).CurrentValues.SetValues(department);
            var result = await _context.SaveChangesAsync();

            return (result > 0 ? department : null) ?? throw new InvalidOperationException();
        }

        public async Task Delete(Guid departmentId)
        {
            var existingDepartment = await _context.Departments.FindAsync(departmentId);

            if (existingDepartment == null)
            {
                throw new EntityNotFoundException("Department not found!");
            }

            _context.Departments.Remove(existingDepartment);
            await _context.SaveChangesAsync();
        }
    }
}
