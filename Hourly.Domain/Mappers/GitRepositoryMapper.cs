using Hourly.Domain.Entities;
using Hourly.Shared.Contracts.Requests.GitRepositoryRequests;
using Hourly.Shared.Contracts.Responses.GitRepositoryResponses;


namespace Hourly.Domain.Mappers
{
    public static partial class GitRepositoryMapper
    {
        public static GitRepositoryResponse ToResponse(this GitRepository entity)
        {
            return new GitRepositoryResponse
            {
                Id = entity.Id,
                ExtRepositoryId = entity.ExtRepositoryId,
                Name = entity.Name,
                Namespace = entity.Namespace,
                WebUrl = entity.WebUrl,
                GitCommits = (entity.GitCommits as List<GitCommit> ?? throw new Exception()).Select(gc => gc.ToSummaryResponse()).ToList(),
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static GitRepositorySummaryResponse ToSummaryResponse(this GitRepository entity)
        {
            return new GitRepositorySummaryResponse
            {
                Id = entity.Id,
                ExtRepositoryId = entity.ExtRepositoryId,
                Name = entity.Name,
                Namespace = entity.Namespace,
                WebUrl = entity.WebUrl,
            };
        }

        public static GitRepository ToGitRepository(this CreateGitRepositoryRequest request)
        {
            return new GitRepository
            {
                ExtRepositoryId = request.ExtRepositoryId,
                Name = request.Name,
                Namespace = request.Namespace,
                WebUrl = request.WebUrl
            };
        }

        public static GitRepository ToGitRepository(this UpdateGitRepositoryRequest request, Guid id)
        {
            return new GitRepository
            {
                Id = id,
                ExtRepositoryId = request.ExtRepositoryId,
                Name = request.Name,
                Namespace = request.Namespace,
                WebUrl = request.WebUrl
            };
        }
    }
}
