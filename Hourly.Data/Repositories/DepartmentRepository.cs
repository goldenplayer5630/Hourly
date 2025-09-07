using Hourly.Abstractions.Repositories;
using Hourly.Domain.Entities;
using Hourly.Domain.Exceptions;
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

        public async Task<Department?> GetById(Guid departmentId)
        {
            return await _context.Departments
                .Include(d => d.Users)
                .FirstOrDefaultAsync(d => d.Id == departmentId);
        }

        public async Task<IEnumerable<Department>> GetAll()
        {
            return await _context.Departments
            .Include(d => d.Users)
            .ToListAsync();
        }

        public async Task<Department> Create(Department department)
        {
            await _context.Departments.AddAsync(department);
            var result = await _context.SaveChangesAsync();
            return (result > 0 ? department : null) ?? throw new InvalidOperationException();
        }

        public async Task<Department> Update(Department department)
        {
            _context.Departments.Update(department);
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
