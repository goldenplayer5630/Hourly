using Hourly.Abstractions.Repositories;
using Hourly.Abstractions.Services;
using Hourly.Shared.Entities;
using Hourly.Shared.Exceptions;

namespace Hourly.Domain.Services
{
    public class WorkSessionService : IWorkSessionService
    {
        private readonly IWorkSessionRepository _repository;
        private readonly IGitCommitRepository _gitCommitRepository;
        private readonly IUserRepository _userRepository;

        public WorkSessionService(IWorkSessionRepository repository, IGitCommitRepository gitCommitRepository, IUserRepository userRepository)
        {
            _repository = repository;
            _gitCommitRepository = gitCommitRepository;
            _userRepository = userRepository;
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

        public async Task<IEnumerable<WorkSession>> Filter(Guid? userId, int? year, int? month, bool? wbso)
        {
            return await _repository.Filter(userId, year, month, wbso);
        }

        public async Task<WorkSession> Create(WorkSession workSession, IEnumerable<Guid> gitCommitIds)
        {
            workSession.Id = Guid.NewGuid();
            workSession.CreatedAt = DateTime.UtcNow;

            workSession.Validate();

            var user = await _userRepository.GetById(workSession.UserContractId)
                ?? throw new EntityNotFoundException("User contract not found!");

            if (workSession.WBSO && !gitCommitIds.Any())
                throw new DomainValidationException("At least one GitCommit is required for WBSO sessions.");

            foreach (var commitId in gitCommitIds.Distinct())
            {
                var commit = await _gitCommitRepository.GetById(commitId)
                    ?? throw new EntityNotFoundException($"GitCommit {commitId} not found!");

                workSession.AddGitCommit(commit);
            }

            return await _repository.Create(workSession);
        }

        public async Task<WorkSession> Update(WorkSession workSession, IEnumerable<Guid> gitCommitIds)
        {
            workSession.UpdatedAt = DateTime.UtcNow;

            workSession.Validate();

            var user = await _userRepository.GetById(workSession.UserContractId)
                ?? throw new EntityNotFoundException("User contract not found!");

            if (workSession.WBSO && !gitCommitIds.Any())
                throw new DomainValidationException("At least one GitCommit is required for WBSO sessions.");

            foreach (var commit in workSession.GitCommits.ToList())
            {
                var gitCommit = await _gitCommitRepository.GetById(commit.Id)
                    ?? throw new EntityNotFoundException($"GitCommit {commit.Id} not found!");

                workSession.RemoveGitCommit(gitCommit);
            }

            foreach (var commitId in gitCommitIds.Distinct())
            {
                var commit = await _gitCommitRepository.GetById(commitId)
                    ?? throw new EntityNotFoundException($"GitCommit {commitId} not found!");

                workSession.AddGitCommit(commit);
            }

            return await _repository.Update(workSession);
        }

        public async Task<WorkSession> AddGitCommit(Guid workSessionId, Guid gitCommitId)
        {
            var workSession = await _repository.GetById(workSessionId)
                ?? throw new EntityNotFoundException("WorkSession not found!");

            return await AddGitCommit(workSession, gitCommitId);
        }

        private async Task<WorkSession> AddGitCommit(WorkSession workSession, Guid gitCommitId)
        {
            var gitCommit = await _gitCommitRepository.GetById(gitCommitId)
                ?? throw new EntityNotFoundException("GitCommit not found in WorkSession!");

            workSession.AddGitCommit(gitCommit);

            await _repository.SaveChanges();

            return workSession;
        }

        public async Task<WorkSession> RemoveGitCommit(Guid workSessionId, Guid gitCommitId)
        {
            var workSession = await _repository.GetById(workSessionId)
                ?? throw new EntityNotFoundException("WorkSession not found!");

            return await RemoveGitCommit(workSession, gitCommitId);
        }

        private async Task<WorkSession> RemoveGitCommit(WorkSession workSession, Guid gitCommitId)
        {
            var gitCommit = await _gitCommitRepository.GetById(gitCommitId)
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
