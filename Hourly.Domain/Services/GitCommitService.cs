using Hourly.Abstractions.Exceptions;
using Hourly.Abstractions.Repositories;
using Hourly.Abstractions.Services;
using Hourly.Shared.Entities;

namespace Hourly.Domain.Services
{
    public class GitCommitService : IGitCommitService
    {
        private readonly IGitCommitRepository _repository;
        private readonly IGitRepositoryRepository _gitRepositoryRepository;
        private readonly IUserRepository _userRepository;

        public GitCommitService(IGitCommitRepository repository, IGitRepositoryRepository gitRepositoryRepository, IUserRepository userRepository)
        {
            _repository = repository;
            _gitRepositoryRepository = gitRepositoryRepository;
            _userRepository = userRepository;
        }

        public async Task<GitCommit> GetById(Guid gitCommitId)
        {
            return await _repository.GetById(gitCommitId)
                ?? throw new EntityNotFoundException("GitCommit not found!");
        }

        public async Task<IEnumerable<GitCommit>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<GitCommit> Create(GitCommit gitCommit)
        {
            gitCommit.Id = Guid.NewGuid();
            gitCommit.CreatedAt = DateTime.UtcNow;

            var gitRepository = await _gitRepositoryRepository.GetById(gitCommit.RepositoryId)
                ?? throw new EntityNotFoundException("GitRepository not found!");

            var author = await _userRepository.GetById(gitCommit.AuthorId)
                ?? throw new EntityNotFoundException("User not found!");

            gitCommit.AssignToRepository(gitRepository);
            gitCommit.AssignToAuthor(author);

            return await _repository.Create(gitCommit);
        }

        public async Task Delete(Guid gitCommitId)
        {
            await _repository.Delete(gitCommitId);
        }
    }
}
