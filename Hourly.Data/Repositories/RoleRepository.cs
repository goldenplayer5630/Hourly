using Hourly.Abstractions.Repositories;
using Hourly.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        Task<Role> GetById(Guid roleId);
        Task<IEnumerable<Role>> GetAll();
        Task Create(Role role);
        Task Update(Role role);
        Task Delete(Role roleId);
    }
}
