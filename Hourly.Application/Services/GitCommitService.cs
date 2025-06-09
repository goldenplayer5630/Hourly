using Hourly.Abstractions.Repositories;
using Hourly.Abstractions.Services;
using Hourly.Domain.Entities;
using Hourly.Domain.Mappers;
using Hourly.Shared.Contracts.Requests.GitCommitRequests;
using Hourly.Shared.Contracts.Responses.GitCommitResponses;
using Hourly.Shared.Exceptions;

namespace Hourly.Domain.Services
{
    public class GitCommitService : IGitCommitService
    {
        private readonly IGitCommitRepository _repository;
        private readonly IGitRepositoryRepository _gitRepositoryRepository;
        private readonly IUserRepository _userRepository;

        public GitCommitService(
            IGitCommitRepository repository,
            IGitRepositoryRepository gitRepositoryRepository,
            IUserRepository userRepository)
        {
            _repository = repository;
            _gitRepositoryRepository = gitRepositoryRepository;
            _userRepository = userRepository;
        }

        public async Task<GitCommitResponse> GetById(Guid gitCommitId)
        {
            var result = await _repository.GetById(gitCommitId) as GitCommit
                ?? throw new EntityNotFoundException("GitCommit not found!");

            return result.ToResponse();
        }

        public async Task<IEnumerable<GitCommitSummaryResponse>> GetAll()
        {
            var result = await _repository.GetAll() as List<GitCommit> ?? new();
            return result.Select(c => c.ToSummaryResponse());
        }

        public async Task<IEnumerable<GitCommitSummaryResponse>> Filter(Guid? repositoryId, Guid? authorId, DateTime? authoredDate)
        {
            var commits = await _repository.Filter(repositoryId, authorId, authoredDate) as List<GitCommit> ?? new();
            return commits.Select(c => c.ToSummaryResponse());
        }

        public async Task<GitCommitResponse> Create(CreateGitCommitRequest request)
        {
            var commit = request.ToGitCommit();
            commit.Id = Guid.NewGuid();
            commit.CreatedAt = DateTime.UtcNow;

            var gitRepository = await _gitRepositoryRepository.GetById(commit.RepositoryId) as GitRepository
                ?? throw new EntityNotFoundException("GitRepository not found!");

            var author = await _userRepository.GetById(commit.AuthorId) as User
                ?? throw new EntityNotFoundException("User not found!");

            commit.AssignToRepository(gitRepository);
            commit.AssignToAuthor(author);

            var result = await _repository.Create(commit) as GitCommit
                ?? throw new InvalidOperationException("Failed to create GitCommit.");

            return result.ToResponse();
        }

        public async Task Delete(Guid gitCommitId)
        {
            await _repository.Delete(gitCommitId);
        }
    }
}
