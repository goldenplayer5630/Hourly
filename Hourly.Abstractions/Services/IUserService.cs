using Hourly.Shared.Contracts.Requests.UserRequests;
using Hourly.Shared.Contracts.Responses.UserResponses;

namespace Hourly.Abstractions.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserSummaryResponse>> GetAll();
        Task<UserResponse> GetById(Guid userId);
        Task<UserResponse> Create(CreateUserRequest user);
        Task<UserResponse> AddDepartment(Guid userId, Guid departmentId);
        Task<UserResponse> RemoveDepartment(Guid userId);
        Task<UserResponse> Update(Guid id, UpdateUserRequest user);
        Task Delete(Guid userId);
    }
}
