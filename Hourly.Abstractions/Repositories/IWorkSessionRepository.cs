using Hourly.Shared.Entities;

namespace Hourly.Abstractions.Repositories
{
    public interface IWorkSessionRepository
    {
        Task<WorkSession?> GetById(Guid workSessionId);
        Task<IEnumerable<WorkSession>> GetAll();
        Task Create(WorkSession workSession);
        Task Update(WorkSession workSession);
        Task Delete(Guid workSessionId);
    }
}
