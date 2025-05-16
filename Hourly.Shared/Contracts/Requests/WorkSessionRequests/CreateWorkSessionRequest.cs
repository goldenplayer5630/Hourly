using System.ComponentModel.DataAnnotations;

namespace Hourly.Shared.Contracts.Requests.WorkSessionRequests
{
    public class CreateWorkSessionRequest
    {
        [Required]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Task description is required.")]
        public string TaskDescription { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public float Factor { get; set; }

        public bool WBSO { get; set; }

        public bool Locked { get; set; }

        public string? OtherRemarks { get; set; }

        public float? TVTAccruedHours { get; set; }

        public float? TVTUsedHours { get; set; }

        public List<Guid> GitCommitIds { get; set; } = new List<Guid>();
    }
}
