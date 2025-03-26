using Hourly.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Abstractions.Repositories
{
    public class DepartmentRepository
    {
        Task<Department> GetById(Guid departmentId);
        Task<IEnumerable<Department>> GetAll();
        Task Create(Department department);
        Task Update(Department department);
        Task Delete(Guid departmentId);
    }
}
