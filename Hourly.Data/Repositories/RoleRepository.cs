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
        public async Task<Role> GetById(Guid roleId);
        public async Task<IEnumerable<Role>> GetAll();
        public async Task Create(Role role);
        public async Task Update(Role role);
        public async Task Delete(Role roleId);
    }
}
