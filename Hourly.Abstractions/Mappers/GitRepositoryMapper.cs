using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hourly.Abstractions.Contracts.Responses.GitRepositoryResponses;
using Hourly.Shared.Entities;
using Hourly.Abstractions.Contracts.Requests.GitRepositoryRequests;


namespace Hourly.Abstractions.Mappers
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
                GitCommits = entity.GitCommits.Select(gc => gc.ToSummaryResponse()).ToList(),
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
