using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hourly.Shared.Entities;

namespace Hourly.Abstractions.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<entity>> GetAll();
        Task<entity?> GetById(Guid departmentId);
        Task<entity> Create(entity department);
        Task<entity> AddUser(Guid departmentId, Guid userId);
        Task<entity> RemoveUser(Guid departmentId, Guid userId);
        Task<entity> Update(entity department);
        Task Delete(Guid departmentId);
    }
}
