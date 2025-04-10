using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.Shared.Entities
{
    public class GitCommit
    {
        [Key]
        public Guid Id { get; init; }

        [Required]
        public Guid RepositoryId { get; init; }

        [Required]
        [ForeignKey("RepositoryId")]
        public GitRepository Repository { get; init; }

        [Required]
        public string ExtCommitId { get; init; }

        [Required]
        public string ExtCommitShortId { get; init; }

        [Required]
        public string Title { get; init; }

        public string? Comment { get; init; }

        [Required]
        public Guid AuthorId { get; set; }

        [Required]
        [ForeignKey("AuthorId")]
        public User Author { get; set; }

        [Required]
        public string WebUrl { get; init; }

        public ICollection<WorkSession> WorkSessions { get; set; } = new List<WorkSession>();

        [Required]
        public DateTime CreatedAt { get; init; }

        public DateTime? UpdatedAt { get; init; }
    }
}
