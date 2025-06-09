using Hourly.Abstractions.Repositories;
using Hourly.Abstractions.Services;
using Hourly.Domain.Entities;
using Hourly.Domain.Mappers;
using Hourly.Shared.Contracts.Requests.UserRequests;
using Hourly.Shared.Contracts.Responses.UserResponses;
using Hourly.Shared.Exceptions;

namespace Hourly.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IRoleRepository _roleRepository;

        public UserService(IUserRepository repository, IDepartmentRepository departmentRepository, IRoleRepository roleRepository)
        {
            _repository = repository;
            _departmentRepository = departmentRepository;
            _roleRepository = roleRepository;
        }

        public async Task<UserResponse> GetById(Guid userId)
        {
            var result = await _repository.GetById(userId) as User
                ?? throw new EntityNotFoundException("User not found!");
            return result.ToResponse();
        }

        public async Task<IEnumerable<UserSummaryResponse>> GetAll()
        {
            var result = await _repository.GetAll() as List<User> ?? new List<User>();
            return result.Select(u => u.ToSummaryResponse());
        }

        public async Task<UserResponse> Create(CreateUserRequest createRequest)
        {
            var user = createRequest.ToUser();
            user.Id = Guid.NewGuid();
            user.CreatedAt = DateTime.UtcNow;

            var role = await _roleRepository.GetById(user.RoleId) as Role
                ?? throw new EntityNotFoundException("Role not found!");

            user.AssignToRole(role);

            var result = await _repository.Create(user) as User
                ?? throw new InvalidOperationException("Failed to create User.");

            return result.ToResponse();
        }

        public async Task<UserResponse> Update(Guid userId, UpdateUserRequest updateRequest)
        {
            var updated = updateRequest.ToUser(userId);
            var existing = await _repository.GetById(userId) as User
                ?? throw new EntityNotFoundException("User not found!");

            existing.Update(updated);

            var result = await _repository.Update(existing) as User
                ?? throw new InvalidOperationException("Failed to update User.");

            return result.ToResponse();
        }

        public async Task<UserResponse> AddDepartment(Guid userId, Guid departmentId)
        {
            var user = await _repository.GetById(userId) as User
                ?? throw new EntityNotFoundException("User not found!");

            var department = await _departmentRepository.GetById(departmentId) as Department
                ?? throw new EntityNotFoundException("Department not found!");

            user.AssignToDepartment(department);

            await _repository.SaveChanges();

            return user.ToResponse();
        }

        public async Task<UserResponse> RemoveDepartment(Guid userId)
        {
            var user = await _repository.GetById(userId) as User
                ?? throw new EntityNotFoundException("User not found!");

            user.RemoveFromDepartment();

            await _repository.SaveChanges();

            return user.ToResponse();
        }

        public async Task Delete(Guid userId)
        {
            await _repository.Delete(userId);
        }
    }
}
