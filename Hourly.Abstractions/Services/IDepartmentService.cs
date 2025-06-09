using Hourly.Shared.Contracts.Requests.DepartmentRequests;
using Hourly.Shared.Contracts.Responses.DepartmentResponses;

namespace Hourly.Abstractions.Services
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentSummaryResponse>> GetAll();
        Task<DepartmentResponse> GetById(Guid departmentId);
        Task<DepartmentResponse> Create(CreateDepartmentRequest department);
        Task<DepartmentResponse> Update(Guid id, UpdateDepartmentRequest department);
        Task Delete(Guid departmentId);
    }
}
