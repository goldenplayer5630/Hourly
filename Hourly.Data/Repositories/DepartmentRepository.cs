using Hourly.Abstractions.Repositories;
using Hourly.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return await _context.Departments.FindAsync(departmentId);
        }

        public async Task<IEnumerable<Department>> GetAll()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task Create(Department department)
        {
            await _context.Departments.AddAsync(department);
        }

        public async Task Update(Department department)
        {
            var existingDepartment = await _context.Departments.FindAsync(department);
            if (existingDepartment != null)
            {
                _context.Entry(existingDepartment).CurrentValues.SetValues(department);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(Guid departmentId)
        {
            var existingDepartment = await _context.Departments.FindAsync(departmentId);
            if (existingDepartment != null)
            {
                _context.Departments.Remove(existingDepartment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
