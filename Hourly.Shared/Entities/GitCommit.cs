using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.Shared.Entities
{
    public class GitCommit
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid RepositoryId { get; set; }

        [Required]
        [ForeignKey("RepositoryId")]
        public GitRepository Repository { get; set; }

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
        public User Author { get; set; }

        [Required]
        public string WebUrl { get; set; }

        public ICollection<WorkSession> WorkSessions { get; set; } = new List<WorkSession>();

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
