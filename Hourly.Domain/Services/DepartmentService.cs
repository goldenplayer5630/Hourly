using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hourly.Abstractions.Repositories;
using Hourly.Abstractions.Services;
using Hourly.Shared.Entities;

namespace Hourly.Domain.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentService(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Department?> GetById(Guid departmentId)
        {
            return await _repository.GetById(departmentId);
        }

        public async Task<IEnumerable<Department>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task Create(Department department)
        {
            await _repository.Create(department);
        }

        public async Task Update(Department department)
        {
            await _repository.Update(department);
        }

        public async Task Delete(Guid departmentId)
        {
            await _repository.Delete(departmentId);
        }
    }
}
