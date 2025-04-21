using Hourly.Shared.Entities;

namespace Hourly.Abstractions.Services
{
    public interface IWorkSessionService
    {
        Task<IEnumerable<WorkSession>> GetAll();
        Task<WorkSession> GetById(Guid workSessionId);
        Task<IEnumerable<WorkSession>> Filter(Guid? userId, int? year, int? month);
        Task<WorkSession> Create(WorkSession workSession);
        Task<WorkSession> AddGitcommit(Guid workSessionId, Guid gitCommitId);
        Task<WorkSession> RemoveGitCommit(Guid workSessionId, Guid gitCommitId);
        Task<WorkSession> Update(WorkSession workSession);
        Task Delete(Guid workSessionId);
    }
}
