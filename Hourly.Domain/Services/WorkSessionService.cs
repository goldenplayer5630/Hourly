using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hourly.Abstractions.Data.Repositories;
using Hourly.Abstractions.Domain.Services;
using Hourly.Shared.Models;

namespace Hourly.Domain.Services
{
    public class WorkSessionService : IWorkSessionService
    {
        private readonly IWorkSessionRepository _repository;

        public WorkSessionService(IWorkSessionRepository repository)
        {
            _repository = repository;
        }

        public async Task<WorkSession?> GetById(Guid workSessionId)
        {
            return await _repository.GetById(workSessionId);
        }

        public async Task<IEnumerable<WorkSession>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task Create(WorkSession workSession)
        {
            await _repository.Create(workSession);
        }

        public async Task Update(WorkSession workSession)
        {
            await _repository.Update(workSession);
        }

        public async Task Delete(Guid workSessionId)
        {
            await _repository.Delete(workSessionId);
        }
    }
}
