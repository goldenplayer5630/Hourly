using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hourly.Abstractions.Repositories;
using Hourly.Abstractions.Services;
using Hourly.Shared.Entities;
using Hourly.Shared.Exceptions;

namespace Hourly.Domain.Services
{
    public class UserContractService : IUserContractService
    {
        private readonly IUserContractRepository _repository;

        public UserContractService(IUserContractRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserContract> GetById(Guid userContractId)
        {
            return await _repository.GetById(userContractId)
                ?? throw new EntityNotFoundException("UserContract not found!");
        }

        public async Task<IEnumerable<UserContract>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<UserContract> Create(UserContract userContract)
        {
            userContract.Id = Guid.NewGuid();
            userContract.CreatedAt = DateTime.UtcNow;
            return await _repository.Create(userContract);
        }

        public async Task<UserContract> Update(UserContract userContract)
        {
            userContract.UpdatedAt = DateTime.UtcNow;
            return await _repository.Update(userContract);
        }

        public async Task Delete(Guid userContractId)
        {
            await _repository.Delete(userContractId);
        }
    }
}
