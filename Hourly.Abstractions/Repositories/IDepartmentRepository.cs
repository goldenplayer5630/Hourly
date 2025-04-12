using Hourly.Shared.Entities;

namespace Hourly.Abstractions.Repositories
{
    public interface IDepartmentRepository
    {
        Task<Department?> GetById(Guid departmentId);
        Task<IEnumerable<Department>> GetAll();
        Task<Department> Create(Department department);
        Task<Department> AddUser(Guid departmentId, Guid userId);
        Task<Department> RemoveUser(Guid departmentId, Guid userId);
        Task<Department> Update(Department department);
        Task Delete(Guid departmentId);
    }
}
