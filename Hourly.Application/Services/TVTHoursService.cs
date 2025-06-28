using Hourly.Abstractions.Repositories;
using Hourly.Abstractions.Services;
using Hourly.Domain.Entities;
using Hourly.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Application.Services
{
    public class TVTHoursService : ITVTHoursService
    {
        private readonly IUserContractRepository _repository;

        public TVTHoursService(IUserContractRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserContract> UpdateTVTHourBalance(UserContract userContract, float hours)
        {
            if (userContract == null)
                throw new ArgumentNullException(nameof(userContract));

            var existing = await _repository.GetById(userContract.Id)
                ?? throw new EntityNotFoundException("UserContract not found!");

            existing.UpdateTVTHours(hours);

            return await _repository.Update(existing);
        }
    }
}
