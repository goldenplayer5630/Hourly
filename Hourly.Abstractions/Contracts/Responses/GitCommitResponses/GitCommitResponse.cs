using Hourly.Abstractions.Contracts.Responses.GitRepositoryResponses;
using Hourly.Abstractions.Contracts.Responses.UserResponses;
using Hourly.Abstractions.Contracts.Responses.WorkSessionResponses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.Abstractions.Contracts.Responses.GitCommitResponses
{
    public class GitCommitResponse
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid RepositoryId { get; internal set; }

        [Required]
        [ForeignKey("RepositoryId")]
        public GitRepositorySummaryResponse Repository { get; internal set; }

        [Required]
        public string ExtCommitId { get; set; }

        [Required]
        public string ExtCommitShortId { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Comment { get; set; }

        [Required]
        public Guid AuthorId { get; set; }

        [Required]
        [ForeignKey("AuthorId")]
        public UserSummaryResponse Author { get; set; }

        [Required]
        public string WebUrl { get; set; }

        public ICollection<WorkSessionSummaryResponse> WorkSessions { get; set; } = new List<WorkSessionSummaryResponse>();

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
