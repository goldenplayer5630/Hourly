using Hourly.Shared.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.Shared.Entities
{
    public class WorkSession
    {
        private List<GitCommit> _gitCommits = new List<GitCommit>();

        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; private set; }

        [Required]
        public string TaskDescription { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public float Factor { get; set; }

        public float TotalEffectiveHours
        {
            get
            {
                return (float)(((EndTime - StartTime).TotalHours) * Factor);
            }
        }

        public float NetEffectiveHours
        {
            get
            {
                return TotalEffectiveHours + (TVTUsedHours ?? 0) - (TVTAccruedHours ?? 0);
            }
        }

        public bool WBSO { get; set; }

        public bool Locked { get; set; }

        public string? OtherRemarks { get; set; }

        public float? TVTAccruedHours { get; set; }

        public float? TVTUsedHours { get; set; }

        public IReadOnlyCollection<GitCommit> GitCommits => _gitCommits;

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public void AddGitCommit(GitCommit gitCommit)
        {
            if (GitCommits.Any(gc => gc.Id == gitCommit.Id))
            {
                throw new DomainValidationException("Git commit is already associated with this work session.");
            }

            _gitCommits.Add(gitCommit);
        }

        public void RemoveGitCommit(GitCommit gitCommit)
        {
            if (!GitCommits.Any(gc => gc.Id == gitCommit.Id))
            {
                throw new DomainValidationException("Git commit is not associated with this work session.");
            }
            _gitCommits.Remove(gitCommit);
        }
    }
}
