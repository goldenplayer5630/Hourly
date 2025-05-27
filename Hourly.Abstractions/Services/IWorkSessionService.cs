using Hourly.Shared.Entities;

namespace Hourly.Abstractions.Services
{
    public interface IWorkSessionService
    {
        Task<IEnumerable<WorkSession>> GetAll();
        Task<WorkSession> GetById(Guid workSessionId);
        Task<IEnumerable<WorkSession>> Filter(Guid? userContractId, int? year, int? month, bool? wbso);
        Task<WorkSession> Create(WorkSession workSession, IEnumerable<Guid> gitCommitIds);
        Task<WorkSession> AddGitCommit(Guid workSessionId, Guid gitCommitId);
        Task<WorkSession> RemoveGitCommit(Guid workSessionId, Guid gitCommitId);
        Task<WorkSession> Update(WorkSession workSession, IEnumerable<Guid> gitCommitIds);
        Task Delete(Guid workSessionId);
    }
}
