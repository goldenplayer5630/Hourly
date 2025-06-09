using Hourly.Abstractions.Repositories;
using Hourly.Abstractions.Services;
using Hourly.Domain.Entities;
using Hourly.Domain.Mappers;
using Hourly.Shared.Contracts.Requests.WorkSessionRequests;
using Hourly.Shared.Contracts.Responses.WorkSessionResponses;
using Hourly.Shared.Exceptions;

namespace Hourly.Domain.Services
{
    public class WorkSessionService : IWorkSessionService
    {
        private readonly IWorkSessionRepository _repository;
        private readonly IGitCommitRepository _gitCommitRepository;
        private readonly IUserContractRepository _userContractRepository;

        public WorkSessionService(IWorkSessionRepository repository, IGitCommitRepository gitCommitRepository, IUserContractRepository userContractRepository)
        {
            _repository = repository;
            _gitCommitRepository = gitCommitRepository;
            _userContractRepository = userContractRepository;
        }

        public async Task<WorkSessionResponse> GetById(Guid workSessionId)
        {
            var result = await _repository.GetById(workSessionId) as WorkSession
                ?? throw new EntityNotFoundException("WorkSession not found!");
            return result.ToResponse();
        }

        public async Task<IEnumerable<WorkSessionSummaryResponse>> GetAll()
        {
            var result = await _repository.GetAll() as List<WorkSession> ?? new List<WorkSession>();
            return result.Select(ws => ws.ToSummaryResponse());
        }

        public async Task<IEnumerable<WorkSessionSummaryResponse>> Filter(Guid? userContractId, int? year, int? month, bool? wbso)
        {
            var result = await _repository.Filter(userContractId, year, month, wbso) as List<WorkSession> ?? new List<WorkSession>();
            return result.Select(ws => ws.ToSummaryResponse());
        }

        public async Task<WorkSessionResponse> Create(CreateWorkSessionRequest createRequest, IEnumerable<Guid> gitCommitIds)
        {
            var workSession = createRequest.ToWorkSession();
            workSession.Id = Guid.NewGuid();
            workSession.CreatedAt = DateTime.UtcNow;

            workSession.Validate();

            var userContract = await _userContractRepository.GetById(workSession.UserContractId) as UserContract
                ?? throw new EntityNotFoundException("User contract not found!");

            if (workSession.WBSO && !gitCommitIds.Any())
                throw new DomainValidationException("At least one GitCommit is required for WBSO sessions.");

            foreach (var commitId in gitCommitIds.Distinct())
            {
                var commit = await _gitCommitRepository.GetById(commitId) as GitCommit
                    ?? throw new EntityNotFoundException($"GitCommit {commitId} not found!");

                workSession.AddGitCommit(commit);
            }

            workSession.AssignToUserContract(userContract);

            var result = await _repository.Create(workSession) as WorkSession
                ?? throw new InvalidOperationException("Failed to create WorkSession.");


            return result.ToResponse();
        }

        public async Task<WorkSessionResponse> Update(Guid id, UpdateWorkSessionRequest updateRequest, IEnumerable<Guid> gitCommitIds)
        {
            var updated = updateRequest.ToWorkSession(id);
            var existing = await _repository.GetById(updated.Id) as WorkSession
                ?? throw new EntityNotFoundException("WorkSession not found!");

            var userContract = await _userContractRepository.GetById(updated.UserContractId) as UserContract
                ?? throw new EntityNotFoundException("UserContract not found!");

            existing.Update(updated);

            if (existing.WBSO && !gitCommitIds.Any())
                throw new DomainValidationException("At least one GitCommit is required for WBSO sessions.");

            existing.AssignToUserContract(userContract);

            // Replace commit links
            existing.GitCommits.Clear();

            foreach (var commitId in gitCommitIds)
            {
                var commit = await _gitCommitRepository.GetById(commitId) as GitCommit;
                if (commit == null)
                    throw new EntityNotFoundException($"GitCommit {commitId} not found!");

                existing.AddGitCommit(commit);
            }

            var result = await _repository.Update(existing) as WorkSession
                ?? throw new InvalidOperationException("Failed to update WorkSession.");

            return result.ToResponse();
        }

        public async Task<WorkSessionResponse> AddGitCommit(Guid workSessionId, Guid gitCommitId)
        {
            var workSession = await _repository.GetById(workSessionId) as WorkSession
                ?? throw new EntityNotFoundException("WorkSession not found!");

            var result = await AddGitCommit(workSession, gitCommitId);

            return result.ToResponse();
        }

        private async Task<WorkSession> AddGitCommit(WorkSession workSession, Guid gitCommitId)
        {
            var gitCommit = await _gitCommitRepository.GetById(gitCommitId) as GitCommit
                ?? throw new EntityNotFoundException("GitCommit not found in WorkSession!");

            workSession.AddGitCommit(gitCommit);

            await _repository.SaveChanges();

            return workSession;
        }

        public async Task<WorkSessionResponse> RemoveGitCommit(Guid workSessionId, Guid gitCommitId)
        {
            var workSession = await _repository.GetById(workSessionId) as WorkSession
                ?? throw new EntityNotFoundException("WorkSession not found!");

            var result = await RemoveGitCommit(workSession, gitCommitId);

            return result.ToResponse();
        }

        private async Task<WorkSession> RemoveGitCommit(WorkSession workSession, Guid gitCommitId)
        {
            var gitCommit = await _gitCommitRepository.GetById(gitCommitId) as GitCommit
                ?? throw new EntityNotFoundException("GitCommit not found in WorkSession!");

            workSession.RemoveGitCommit(gitCommit);

            await _repository.SaveChanges();

            return workSession;
        }

        public async Task Delete(Guid workSessionId)
        {
            await _repository.Delete(workSessionId);
        }
    }
}
