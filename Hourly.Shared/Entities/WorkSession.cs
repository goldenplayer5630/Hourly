using Hourly.Shared.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.Shared.Entities
{
    public class WorkSession
    {
        private List<GitCommit> _gitCommits = new();

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

        public float TVTAccruedHours { get; set; } = 0;
        public float TVTUsedHours { get; set; } = 0;

        public bool WBSO { get; set; }
        public bool Locked { get; set; }
        public string? OtherRemarks { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public IReadOnlyCollection<GitCommit> GitCommits => _gitCommits;

        public float TotalEffectiveHours
        {
            get
            {
                var total = (float)((EndTime - StartTime).TotalHours * Factor);
                if (total < 0)
                    throw new DomainValidationException("Total effective hours cannot be negative.");
                return total;
            }
        }

        public float NetEffectiveHours
        {
            get
            {
                var net = TotalEffectiveHours + TVTUsedHours - TVTAccruedHours;
                if (net < 0)
                    throw new DomainValidationException("Net effective hours cannot be negative.");
                return net;
            }
        }

        public void AddGitCommit(GitCommit gitCommit)
        {
            if (_gitCommits.Any(gc => gc.Id == gitCommit.Id))
                throw new DomainValidationException("Git commit is already associated with this work session.");
            _gitCommits.Add(gitCommit);
        }

        public void RemoveGitCommit(GitCommit gitCommit)
        {
            if (!_gitCommits.Any(gc => gc.Id == gitCommit.Id))
                throw new DomainValidationException("Git commit is not associated with this work session.");
            _gitCommits.Remove(gitCommit);
        }

        public void Validate()
        {
            if (StartTime >= EndTime)
                throw new DomainValidationException("Start time must be before end time.");

            if (Factor < 0)
                throw new DomainValidationException("Factor cannot be negative.");

            if (TVTAccruedHours > 0 && TVTUsedHours > 0)
                throw new DomainValidationException("Cannot both accrue and use TVT hours in the same work session.");

            if (TVTAccruedHours < 0)
                throw new DomainValidationException("TVTAccruedHours cannot be negative.");

            if (TVTUsedHours < 0)
                throw new DomainValidationException("TVTUsedHours cannot be negative.");

            var duration = (EndTime - StartTime).TotalHours;
            if ((TVTAccruedHours + TVTUsedHours) > duration)
                throw new DomainValidationException("Sum of TVTAccruedHours and TVTUsedHours cannot exceed total session duration.");

            if (!IsValid15MinuteInterval(StartTime.Minute) || !IsValid15MinuteInterval(EndTime.Minute))
                throw new DomainValidationException("Start and end time must be in 15-minute intervals.");

            var today = DateTime.UtcNow.Date;
            if (StartTime.Date > today || EndTime.Date > today)
                throw new DomainValidationException("Work session start and end times cannot be in the future.");
        }

        private bool IsValid15MinuteInterval(int minute)
        {
            return minute == 0 || minute == 15 || minute == 30 || minute == 45;
        }
    }
}
