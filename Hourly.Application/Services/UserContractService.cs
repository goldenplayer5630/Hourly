using Hourly.Abstractions.Repositories;
using Hourly.Abstractions.Services;
using Hourly.Domain.Entities;
using Hourly.Domain.Mappers;
using Hourly.Shared.Contracts.Requests.UserContractRequests;
using Hourly.Shared.Contracts.Responses.UserContractResponses;
using Hourly.Shared.Exceptions;

namespace Hourly.Domain.Services
{
    public class UserContractService : IUserContractService
    {
        private readonly IUserContractRepository _repository;
        private readonly IUserRepository _userRepository;

        public UserContractService(
            IUserContractRepository repository,
            IUserRepository userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public async Task<UserContractResponse> GetById(Guid userContractId)
        {
            var result = await _repository.GetById(userContractId) as UserContract
                ?? throw new EntityNotFoundException("UserContract not found!");

            return result.ToResponse();
        }

        public async Task<IEnumerable<UserContractSummaryResponse>> GetAll()
        {
            var contracts = await _repository.GetAll() as List<UserContract> ?? new();
            return contracts.Select(c => c.ToSummaryResponse());
        }

        public async Task<IEnumerable<UserContractSummaryResponse>> FilterUserContracts(Guid? userId, int? year, int? month)
        {
            var contracts = await _repository.FilterUserContracts(userId, year, month) as List<UserContract> ?? new();
            return contracts.Select(c => c.ToSummaryResponse());
        }

        public async Task<UserContractResponse> Create(CreateUserContractRequest request)
        {
            var userContract = request.ToUserContract();
            userContract.Id = Guid.NewGuid();
            userContract.CreatedAt = DateTime.UtcNow;

            userContract.Validate();

            var user = await _userRepository.GetById(userContract.UserId) as User
                ?? throw new EntityNotFoundException("User not found!");

            userContract.AssignToUser(user);

            var result = await _repository.Create(userContract) as UserContract
                ?? throw new InvalidOperationException("Failed to create UserContract.");

            return result.ToResponse();
        }

        public async Task<UserContractResponse> Update(Guid id, UpdateUserContractRequest request)
        {
            var updated = request.ToUserContract(id);
            var existing = await _repository.GetById(updated.Id) as UserContract
                ?? throw new EntityNotFoundException("UserContract not found!");

            var user = await _userRepository.GetById(updated.UserId) as User
                ?? throw new EntityNotFoundException("User not found!");

            existing.Update(updated);
            existing.AssignToUser(user);

            var result = await _repository.Update(existing) as UserContract
                ?? throw new InvalidOperationException("Failed to update UserContract.");

            return result.ToResponse();
        }

        public async Task Delete(Guid userContractId)
        {
            await _repository.Delete(userContractId);
        }
    }
}
