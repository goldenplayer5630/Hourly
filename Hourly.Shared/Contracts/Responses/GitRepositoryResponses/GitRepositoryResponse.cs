using Hourly.Shared.Contracts.Responses.GitCommitResponses;
using System.ComponentModel.DataAnnotations;

namespace Hourly.Shared.Contracts.Responses.GitRepositoryResponses
{
    public class GitRepositoryResponse
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string ExtRepositoryId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Namespace { get; set; }

        [Required]
        public string WebUrl { get; set; }

        public ICollection<GitCommitSummaryResponse> GitCommits { get; set; } = new List<GitCommitSummaryResponse>();

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
