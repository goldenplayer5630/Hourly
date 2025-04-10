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

        public async Task<Department?> GetById(Guid departmentId)
        {
            var ressult = await _context.Departments.FindAsync(departmentId);

            if (ressult == null)
            {
                throw new EntityNotFoundException("Department not found!");
            }

            return ressult;
        }

        public async Task<IEnumerable<Department>> GetAll()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department> Create(Department department)
        {
            await _context.Departments.AddAsync(department);
            var result = await _context.SaveChangesAsync();
            return (result > 0 ? department : null) ?? throw new InvalidOperationException();

        }

        public async Task<Department> Update(Department department)
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
