using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hourly.Shared.Entities;

namespace Hourly.Abstractions.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAll();
        Task<Department?> GetById(Guid id);
        Task Create(Department department);
        Task Update(Department department);
        Task Delete(Guid id);
    }
}
