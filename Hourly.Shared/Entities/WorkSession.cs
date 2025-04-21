using Hourly.Shared.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.Shared.Entities
{
    public class WorkSession
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

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

        public float Duration
        {
            get { return (float)((EndTime - StartTime).TotalHours) * Factor; } // Fix for CS0029
        }

        public bool WBSO { get; set; }

        public string? OtherRemarks { get; set; }

        public ICollection<GitCommit> GitCommits { get; set; } = new List<GitCommit>();

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public void AddGitCommit(GitCommit gitCommit)
        {
            if (GitCommits.Any(gc => gc.Id == gitCommit.Id))
            {
                throw new DomainValidationException("Git commit is already associated with this work session.");
            }

            GitCommits.Add(gitCommit);
        }

        public void RemoveGitCommit(GitCommit gitCommit)
        {
            if (!GitCommits.Any(gc => gc.Id == gitCommit.Id))
            {
                throw new DomainValidationException("Git commit is not associated with this work session.");
            }
            GitCommits.Remove(gitCommit);
        }
    }
}
