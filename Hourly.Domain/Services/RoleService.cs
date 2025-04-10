using Hourly.Abstractions.Repositories;
using Hourly.Abstractions.Services;
using Hourly.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Domain.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;

        public RoleService(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<Role?> GetById(Guid roleId)
        {
            return await _repository.GetById(roleId);
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task Create(Role role)
        {
            await _repository.Create(role);
        }

        public async Task Update(Role role)
        {
            await _repository.Update(role);
        }

        public async Task Delete(Guid roleId)
        {
            await _repository.Delete(roleId);
        }
    }
}
