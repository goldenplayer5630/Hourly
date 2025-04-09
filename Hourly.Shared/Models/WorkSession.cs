using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.Shared.Models
{
    public class WorkSession
    {
        [Key]
        public Guid Id { get; init; }

        [Required]
        public Guid UserId { get; init; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [Required]
        public string TaskDescription { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public float Factor { get; set; }

        public bool WBSO { get; set; }

        public string? OtherRemarks { get; set; }

        public ICollection<GitCommit> GitCommits { get; set; } = new List<GitCommit>();

        [Required]
        public DateTime CreatedAt { get; init; }

        public DateTime? UpdatedAt { get; init; }
    }
}
