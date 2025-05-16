using Hourly.Shared.Entities;

namespace Hourly.Abstractions.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAll();
        Task<Role> GetById(Guid roleId);
        Task<Role> Create(Role role);
        Task<Role> Update(Role role);
    }
}
