using Hourly.Abstractions.Repositories;
using Hourly.Abstractions.Services;
using Hourly.Domain.Entities;
using Hourly.Domain.Mappers;
using Hourly.Shared.Contracts.Requests.GitRepositoryRequests;
using Hourly.Shared.Contracts.Responses.GitRepositoryResponses;
using Hourly.Shared.Exceptions;

namespace Hourly.Domain.Services
{
    public class GitRepositoryService : IGitRepositoryService
    {
        private readonly IGitRepositoryRepository _repository;

        public GitRepositoryService(IGitRepositoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<GitRepositoryResponse> GetById(Guid gitRepositoryId)
        {
            var result = await _repository.GetById(gitRepositoryId) as GitRepository
                ?? throw new EntityNotFoundException("GitRepository not found!");

            return result.ToResponse();
        }

        public async Task<IEnumerable<GitRepositorySummaryResponse>> GetAll()
        {
            var repos = await _repository.GetAll() as List<GitRepository> ?? new();
            return repos.Select(r => r.ToSummaryResponse());
        }

        public async Task<GitRepositoryResponse> Create(CreateGitRepositoryRequest request)
        {
            var repo = request.ToGitRepository();
            repo.Id = Guid.NewGuid();
            repo.CreatedAt = DateTime.UtcNow;

            var result = await _repository.Create(repo) as GitRepository
                ?? throw new InvalidOperationException("Failed to create GitRepository.");

            return result.ToResponse();
        }

        public async Task<GitRepositoryResponse> Update(Guid id, UpdateGitRepositoryRequest request)
        {
            var updated = request.ToGitRepository(id);
            var existing = await _repository.GetById(id) as GitRepository
                ?? throw new EntityNotFoundException("GitRepository not found!");

            existing.Update(updated);
            existing.UpdatedAt = DateTime.UtcNow;

            var result = await _repository.Update(existing) as GitRepository
                ?? throw new InvalidOperationException("Failed to update GitRepository.");

            return result.ToResponse();
        }

        public async Task Delete(Guid gitRepositoryId)
        {
            await _repository.Delete(gitRepositoryId);
        }
    }
}
