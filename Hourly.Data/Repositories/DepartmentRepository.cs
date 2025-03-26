using Hourly.Abstractions.Repositories;
using Hourly.Shared.Models;
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

        public async Task<Department> GetById(Guid departmentId)
        {

        }

        public async Task<IEnumerable<Department>> GetAll()
        {

        }

        public async Task Create(Department department)
        {

        }

        public async Task Update(Department department)
        {

        }

        public async Task Delete(Guid departmentId)
        {

        }

    }
}
