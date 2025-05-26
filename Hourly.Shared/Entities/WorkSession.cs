using Hourly.Shared.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.Shared.Entities
{
    public class WorkSession
    {
        protected List<GitCommit> _gitCommits = new();

        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserContractId { get; private set; }
        [ForeignKey("UserContractId")]
        public UserContract UserContract { get; private set; } = null!;

        [Required]
        public string TaskDescription { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        public float BreakTime { get; set; }

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

        public IReadOnlyCollection<GitCommit> GitCommits => _gitCommits.AsReadOnly();

        public float RawEffectiveHours
        {
            get
            {
                var total = (float)((EndTime - StartTime).TotalHours) - BreakTime;
                if (total < 0)
                    throw new DomainValidationException("Raw effective hours cannot be negative.");
                return total;
            }
        }

        public float NetEffectiveHours
        {
            get
            {
                if (TVTUsedHours > 0 && TVTAccruedHours > 0)
                    throw new DomainValidationException("Cannot both accrue and use TVT hours in the same work session.");

                var net = (TVTAccruedHours > 0 ? RawEffectiveHours - TVTAccruedHours : RawEffectiveHours + TVTUsedHours) * Factor;

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

        public void AssignToUserContract(UserContract userContract)
        {
            if (UserContractId == userContract.Id)
            {
                throw new DomainValidationException("Work session is already assigned to this user contract.");
            }

            UserContractId = userContract.Id;
            UserContract = userContract;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Validate()
        {
            if (StartTime > EndTime)
                throw new DomainValidationException("Start time must be before or equal to end time.");

            if (Factor < 0)
                throw new DomainValidationException("Factor cannot be negative.");

            if (TVTAccruedHours > 0 && TVTUsedHours > 0)
                throw new DomainValidationException("Cannot both accrue and use TVT hours in the same work session.");

            if (TVTAccruedHours < 0 || TVTUsedHours < 0)
                throw new DomainValidationException("TVTAccruedHours and TVTUsedHours cannot be negative.");

            if (RawEffectiveHours < 0 || NetEffectiveHours < 0)
                throw new DomainValidationException("Total effective and net effective hours cannot be negative.");

            if (BreakTime < 0)
                throw new DomainValidationException("Break time cannot be negative.");

            if (BreakTime >= RawEffectiveHours)
                throw new DomainValidationException("Break time cannot exceed net effective hours.");

            if (!IsValid15MinuteInterval(StartTime.Minute) || !IsValid15MinuteInterval(EndTime.Minute))
                throw new DomainValidationException("Start and end time must be in 15-minute intervals.");

            if (Math.Abs(TVTAccruedHours % 0.25) > 0.0001)
                throw new DomainValidationException("TVTAccruedHours and TVTUsedHours must be in increments of 15 minutes.");

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
