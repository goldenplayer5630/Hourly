using Hourly.Shared.Entities;

namespace Hourly.Abstractions.Services
{
    public interface IWorkSessionService
    {
        Task<IEnumerable<WorkSession>> GetAll();
        Task<WorkSession> GetById(Guid workSessionId);
        Task<WorkSession> Create(WorkSession workSession);
        Task<WorkSession> Update(WorkSession workSession);
        Task Delete(Guid workSessionId);
    }
}
