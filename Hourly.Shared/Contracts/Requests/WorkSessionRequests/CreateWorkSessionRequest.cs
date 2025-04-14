using System.ComponentModel.DataAnnotations;

namespace Hourly.Shared.Contracts.Requests.WorkSessionRequests
{
    public class CreateWorkSessionRequest
    {
        [Required]
        public Guid UserId { get; set; }

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
    }
}
