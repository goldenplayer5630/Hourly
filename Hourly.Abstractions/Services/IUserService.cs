using Hourly.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Abstractions.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(Guid userId);
        Task<User> Create(User user);
        Task<User> AddDepartment(Guid userId, Guid departmentId);
        Task<User> RemoveDepartment(Guid userId);
        Task<User> Update(User user);
        Task Delete(Guid userId);
    }
}
