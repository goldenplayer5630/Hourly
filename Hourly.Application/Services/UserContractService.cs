using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hourly.Abstractions.Repositories;
using Hourly.Abstractions.Services;
using Hourly.Domain.Entities;
using Hourly.Domain.Exceptions;

namespace Hourly.Application.Services
{
    public class UserContractService : IUserContractService
    {
        private readonly IUserContractRepository _repository;
        private readonly IUserRepository _userRepository;
        private readonly ILockedMonthRepository _lockedMonthRepository;
        private readonly IWorkSessionRepository _workSessionRepository;

        public UserContractService(IUserContractRepository repository, IUserRepository userRepository, ILockedMonthRepository lockedMonthRepository, IWorkSessionRepository workSessionRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
            _lockedMonthRepository = lockedMonthRepository;
            _workSessionRepository = workSessionRepository;
        }

        public async Task<UserContract> GetById(Guid userContractId)
        {
            return await _repository.GetById(userContractId)
                ?? throw new EntityNotFoundException("UserContract not found!");
        }

        public async Task<IEnumerable<UserContract>> FilterUserContracts(Guid? userId, int? year, int? month, bool? isActive)
        {
            return await _repository.FilterUserContracts(userId, year, month, isActive);
        }

        public async Task<IEnumerable<UserContract>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<UserContract> Create(UserContract userContract)
        {
            userContract.Id = Guid.NewGuid();
            userContract.CreatedAt = DateTime.UtcNow;

            userContract.Validate();

            var user = await _userRepository.GetById(userContract.UserId)
                ?? throw new EntityNotFoundException("User not found!");

            var activeContracts = await _repository.FilterUserContracts(user.Id, null, null, true);

            if (activeContracts.Any() && userContract.IsActive)
            {
                throw new DomainValidationException("User already has an active contract.");
            }

            userContract.AssignToUser(user);

            return await _repository.Create(userContract);
        }

        public async Task<UserContract> Update(UserContract userContract)
        {
            var existing = await _repository.GetById(userContract.Id)
                ?? throw new EntityNotFoundException("UserContract not found!");

            var user = await _userRepository.GetById(userContract.UserId)
                ?? throw new EntityNotFoundException("User not found!");

            var activeContracts = await _repository.FilterUserContracts(user.Id, null, null, true);

            if (activeContracts.Any() && userContract.IsActive)
            {
                if (activeContracts.FirstOrDefault()?.Id != userContract.Id)
                {
                    throw new DomainValidationException("User already has an active contract.");
                }
            }

            existing.Update(userContract);

            existing.AssignToUser(user);

            return await _repository.Update(existing);
        }

        public async Task<UserContract> AddLockedMonth(Guid userContractId, int year, int month)
        {
            var userContract = await _repository.GetById(userContractId)
                ?? throw new EntityNotFoundException("UserContract not found!");

            if (userContract.LockedMonths.Any(lm => lm.Year == year && lm.Month == month))
                throw new DomainValidationException($"Locked month {year}-{month} already exists for this user contract.");

            var workSessions = await _workSessionRepository.Filter(userContractId, year, month, null);

            if (workSessions.Any())
            {
                foreach (var session in workSessions)
                {
                    session.Locked = true;
                    var lockedSession = await _workSessionRepository.Update(session);
                }
            }

            var lockedMonth = new LockedMonth()
            {
                Id = Guid.NewGuid(),
                UserContractId = userContractId,
                Year = year,
                Month = month,
                UserContract = userContract
            };

            userContract.LockedMonths.Add(lockedMonth);
            await _lockedMonthRepository.Create(lockedMonth);
            await _repository.Update(userContract);
            return userContract;
        }

        public async Task<UserContract> RemoveLockedMonth(Guid userContractId, int year, int month)
        {
            var userContract = await _repository.GetById(userContractId)
                ?? throw new EntityNotFoundException("UserContract not found!");

            var lockedMonth = userContract.LockedMonths.FirstOrDefault(lm => lm.Year == year && lm.Month == month)
                ?? throw new DomainValidationException($"Locked month {year}-{month} does not exist for this user contract.");

            var workSessions = await _workSessionRepository.Filter(userContractId, year, month, null);

            if (workSessions.Any())
            {
                foreach (var session in workSessions)
                {
                    session.Locked = false;
                    await _workSessionRepository.Update(session);
                }
            }

            userContract.LockedMonths.Remove(lockedMonth);
            await _lockedMonthRepository.Delete(lockedMonth.Id);
            return userContract;
        }

        public async Task Delete(Guid userContractId)
        {
            await _repository.Delete(userContractId);
        }
    }
}