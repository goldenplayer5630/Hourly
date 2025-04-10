using Hourly.Abstractions.Repositories;
using Hourly.Abstractions.Services;
using Hourly.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User?> GetById(Guid userId)
        {
            return await _repository.GetById(userId);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task Create(User user)
        {
            await _repository.Create(user);
        }

        public async Task Update(User user)
        {
            await _repository.Update(user);
        }

        public async Task Delete(Guid userId)
        {
            await _repository.Delete(userId);
        }
    }
}
