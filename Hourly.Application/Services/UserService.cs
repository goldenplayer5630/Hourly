using Hourly.Abstractions.Repositories;
using Hourly.Abstractions.Services;
using Hourly.Domain.Entities;
using Hourly.Domain.Exceptions;

namespace Hourly.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IDepartmentRepository _departmentRepository;

        public UserService(IUserRepository repository, IDepartmentRepository departmentRepository)
        {
            _repository = repository;
            _departmentRepository = departmentRepository;
        }

        public async Task<User> GetById(Guid userId)
        {
            return await _repository.GetById(userId)
                ?? throw new EntityNotFoundException("User not found!");
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<User> Create(User user)
        {
            user.Id = Guid.NewGuid();
            user.CreatedAt = DateTime.UtcNow;

            var result = await _repository.Create(user);
            return result;
        }

        public async Task<User> AddDepartment(Guid userId, Guid departmentId)
        {
            var user = await _repository.GetById(userId)
                ?? throw new EntityNotFoundException("User not found!");

            var department = await _departmentRepository.GetById(departmentId)
                ?? throw new Exception("Department not found!");

            user.AssignToDepartment(department);

            await _repository.Update(user);

            return user;
        }

        public async Task<User> RemoveDepartment(Guid userId)
        {
            var user = await _repository.GetById(userId)
                ?? throw new EntityNotFoundException("User not found!");

            user.RemoveFromDepartment();

            await _repository.Update(user);

            return user;
        }

        public async Task<User> Update(User user)
        {
            var existing = await _repository.GetById(user.Id)
                ?? throw new EntityNotFoundException("User not found!");

            existing.Update(user);

            var result = await _repository.Update(existing);
            return result;
        }

        public async Task Delete(Guid userId)
        {
            await _repository.Delete(userId);
        }
    }
}
