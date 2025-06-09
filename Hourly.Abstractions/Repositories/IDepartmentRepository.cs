using Hourly.Abstractions.Entities;

namespace Hourly.Abstractions.Repositories
{
    public interface IDepartmentRepository
    {
        Task<IDepartment?> GetById(Guid IDepartmentId);
        Task<IEnumerable<IDepartment>> GetAll();
        Task<IDepartment> Create(IDepartment IDepartment);
        Task<IDepartment> Update(IDepartment IDepartment);
        Task Delete(Guid IDepartmentId);
        Task SaveChanges();
    }
}
