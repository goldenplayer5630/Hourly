using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.Shared.Models
{
    public class WorkSessionGitCommit
    {
        [Key]
        public Guid WorkSessionId { get; init; }

        [Key]
        public Guid GitCommitId { get; init; }

        [Required]
        [ForeignKey("WorkSessionId")]
        public WorkSession WorkSession { get; init; }

        [Required]
        [ForeignKey("GitCommitId")]
        public GitCommit GitCommit { get; init; }

        [Required]
        public DateTime CreatedAt { get; init; }

        public DateTime? UpdatedAt { get; init; }
    }
}
