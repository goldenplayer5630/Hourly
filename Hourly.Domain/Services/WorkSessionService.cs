using Hourly.Abstractions.Exceptions;
using Hourly.Abstractions.Repositories;
using Hourly.Abstractions.Services;
using Hourly.Shared.Entities;

namespace Hourly.Domain.Services
{
    public class WorkSessionService : IWorkSessionService
    {
        private readonly IWorkSessionRepository _repository;

        public WorkSessionService(IWorkSessionRepository repository)
        {
            _repository = repository;
        }

        public async Task<WorkSession> GetById(Guid workSessionId)
        {
            return await _repository.GetById(workSessionId)
                ?? throw new EntityNotFoundException("WorkSession not found!");
        }

        public async Task<IEnumerable<WorkSession>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<WorkSession> Create(WorkSession workSession)
        {
            workSession.Id = Guid.NewGuid();
            workSession.CreatedAt = DateTime.UtcNow;
            return await _repository.Create(workSession);
        }

        public async Task<WorkSession> Update(WorkSession workSession)
        {
            workSession.UpdatedAt = DateTime.UtcNow;
            return await _repository.Update(workSession);
        }

        public async Task Delete(Guid workSessionId)
        {
            await _repository.Delete(workSessionId);
        }
    }
}
