using Hourly.Domain.Entities;
using Hourly.Shared.Contracts.Requests.GitCommitRequests;
using Hourly.Shared.Contracts.Responses.GitCommitResponses;

namespace Hourly.Domain.Mappers
{
    public static partial class GitCommitMapper
    {
        public static GitCommitResponse ToResponse(this GitCommit entity)
        {
            return new GitCommitResponse
            {
                Id = entity.Id,
                RepositoryId = entity.RepositoryId,
                Repository = (entity.Repository as GitRepository ?? throw new Exception()).ToSummaryResponse(),
                ExtCommitId = entity.ExtCommitId,
                ExtCommitShortId = entity.ExtCommitShortId,
                Title = entity.Title,
                Comment = entity.Comment,
                AuthorId = entity.AuthorId,
                Author = (entity.Author as User ?? throw new Exception()).ToSummaryResponse(),
                AuthoredDate = entity.AuthoredDate,
                WebUrl = entity.WebUrl,
                WorkSessions = (entity.WorkSessions as List<WorkSession> ?? throw new Exception()).Select(ws => ws.ToSummaryResponse()).ToList(),
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static GitCommitSummaryResponse ToSummaryResponse(this GitCommit entity)
        {
            return new GitCommitSummaryResponse
            {
                Id = entity.Id,
                RepositoryId = entity.RepositoryId,
                ExtCommitId = entity.ExtCommitId,
                ExtCommitShortId = entity.ExtCommitShortId,
                Title = entity.Title,
                Comment = entity.Comment,
                AuthorId = entity.AuthorId,
                AuthoredDate = entity.AuthoredDate,
                WebUrl = entity.WebUrl,
            };
        }

        public static GitCommit ToGitCommit(this CreateGitCommitRequest response)
        {
            return new GitCommit
            {
                ExtCommitId = response.ExtCommitId,
                ExtCommitShortId = response.ExtCommitShortId,
                Title = response.Title,
                Comment = response.Comment,
                AuthorId = response.AuthorId,
                AuthoredDate = response.AuthoredDate,
                WebUrl = response.WebUrl,
            };
        }
    }
}
