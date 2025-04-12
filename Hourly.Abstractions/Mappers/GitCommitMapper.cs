using Hourly.Abstractions.Contracts.Requests.GitCommitRequests;
using Hourly.Abstractions.Contracts.Responses.GitCommitResponse;
using Hourly.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Abstractions.Mappers
{
    public static partial class GitCommitMapper
    {
        public static GitCommitResponse ToResponse(this GitCommit entity)
        {
            return new GitCommitResponse
            {
                Id = entity.Id,
                RepositoryId = entity.RepositoryId,
                ExtCommitId = entity.ExtCommitId,
                ExtCommitShortId = entity.ExtCommitShortId,
                Title = entity.Title,
                Comment = entity.Comment,
                AuthorId = entity.AuthorId,
                WebUrl = entity.WebUrl,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static GitCommit ToGitCommit(this CreateGitCommitRequest response)
        {
            return new GitCommit
            {
                RepositoryId = response.RepositoryId,
                ExtCommitId = response.ExtCommitId,
                ExtCommitShortId = response.ExtCommitShortId,
                Title = response.Title,
                Comment = response.Comment,
                AuthorId = response.AuthorId,
                WebUrl = response.WebUrl,
            };
        }
    }
}
