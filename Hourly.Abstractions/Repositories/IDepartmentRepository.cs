using Hourly.Shared.Entities;

namespace Hourly.Abstractions.Repositories
{
    public interface IDepartmentRepository
    {
        Task<Department?> GetById(Guid departmentId);
        Task<IEnumerable<Department>> GetAll();
        Task Create(Department department);
        Task Update(Department department);
        Task Delete(Guid departmentId);
    }
}
