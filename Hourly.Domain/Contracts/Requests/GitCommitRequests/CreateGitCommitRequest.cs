using System.ComponentModel.DataAnnotations;

namespace Hourly.Domain.Contracts.Requests.GitCommitRequests
{
    public class CreateGitCommitRequest
    {
        [Required]
        public Guid GitRepositoryId { get; init; }

        [Required]
        public string ExtCommitId { get; init; } = string.Empty;

        [Required]
        public string ExtCommitShortId { get; init; } = string.Empty;

        [Required]
        public string Title { get; init; } = string.Empty;

        public string? Comment { get; init; }

        [Required]
        public Guid AuthorId { get; set; }

        public DateTime AuthoredDate { get; set; }

        [Required]
        public string WebUrl { get; init; } = string.Empty;
    }
}
