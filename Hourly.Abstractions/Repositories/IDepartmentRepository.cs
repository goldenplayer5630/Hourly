using Hourly.Shared.Entities;

namespace Hourly.Abstractions.Repositories
{
    public interface IDepartmentRepository
    {
        Task<entity?> GetById(Guid departmentId);
        Task<IEnumerable<entity>> GetAll();
        Task<entity> Create(entity department);
        Task<entity> AddUser(Guid departmentId, Guid userId);
        Task<entity> RemoveUser(Guid departmentId, Guid userId);
        Task<entity> Update(entity department);
        Task Delete(Guid departmentId);
    }
}
