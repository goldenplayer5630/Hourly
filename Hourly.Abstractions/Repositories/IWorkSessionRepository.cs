using Hourly.Shared.Entities;

namespace Hourly.Abstractions.Repositories
{
    public interface IWorkSessionRepository
    {
        Task<WorkSession?> GetById(Guid workSessionId);
        Task<IEnumerable<WorkSession>> GetAll();
        Task<IEnumerable<WorkSession>> Filter(Guid? userId, int? year, int? month);
        Task<WorkSession> Create(WorkSession workSession);
        Task<WorkSession> Update(WorkSession workSession);
        Task Delete(Guid workSessionId);
        Task SaveChanges();
    }
}
