using Hourly.Shared.Contracts.Requests.WorkSessionRequests;
using Hourly.Shared.Contracts.Responses.WorkSessionResponses;

namespace Hourly.Abstractions.Services
{
    public interface IWorkSessionService
    {
        Task<IEnumerable<WorkSessionSummaryResponse>> GetAll();
        Task<WorkSessionResponse> GetById(Guid workSessionId);
        Task<IEnumerable<WorkSessionSummaryResponse>> Filter(Guid? userContractId, int? year, int? month, bool? wbso);
        Task<WorkSessionResponse> Create(CreateWorkSessionRequest workSession, IEnumerable<Guid> gitCommitIds);
        Task<WorkSessionResponse> AddGitCommit(Guid workSessionId, Guid gitCommitId);
        Task<WorkSessionResponse> RemoveGitCommit(Guid workSessionId, Guid gitCommitId);
        Task<WorkSessionResponse> Update(Guid id, UpdateWorkSessionRequest workSession, IEnumerable<Guid> gitCommitIds);
        Task Delete(Guid workSessionId);
    }
}
