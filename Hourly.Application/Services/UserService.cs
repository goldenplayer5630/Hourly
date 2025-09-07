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

        public async Task<IEnumerable<User>> GetAll() => await _repository.GetAll();

        public async Task<User?> GetByExternalOid(Guid externalOid)
            => await _repository.GetByExternalOid(externalOid);

        public async Task<User> BootstrapOrUpdate(Guid externalOid, string? email, string? name)
        {
            var user = await _repository.GetByExternalOid(externalOid);
            if (user is null)
            {
                user = new User
                {
                    Id = Guid.NewGuid(),
                    ExternalOid = externalOid,
                    Email = email,
                    Name = name,
                    CreatedAt = DateTime.UtcNow,
                };
                return await _repository.Create(user);
            }

            // Refresh profile info on every login
            if (!string.IsNullOrWhiteSpace(email)) user.Email = email;
            if (!string.IsNullOrWhiteSpace(name)) user.Name = name;

            return await _repository.Update(user);
        }

        public async Task<User> Create(User user)
        {
            user.Id = Guid.NewGuid();               // leave as-is for manual creates
            user.CreatedAt = DateTime.UtcNow;
            return await _repository.Create(user);
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
            return await _repository.Update(existing);
        }

        public async Task Delete(Guid userId) => await _repository.Delete(userId);
    }
}
