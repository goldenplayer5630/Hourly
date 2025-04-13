using Hourly.Shared.Entities;

namespace Hourly.Abstractions.Repositories
{
    public interface IDepartmentRepository
    {
        Task<Department?> GetById(Guid departmentId);
        Task<IEnumerable<Department>> GetAll();
        Task<Department> Create(Department department);
        Task<Department> Update(Department department);
        Task Delete(Guid departmentId);
        Task SaveChanges();
    }
}
