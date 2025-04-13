using Hourly.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Abstractions.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAll();
        Task<Role> GetById(Guid roleId);
        Task<Role> Create(Role role);
        Task<Role> Update(Role role);
        Task Delete(Guid roleId);
    }
}
