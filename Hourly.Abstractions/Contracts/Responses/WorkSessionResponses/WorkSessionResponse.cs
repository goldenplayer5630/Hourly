using Hourly.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Hourly.Abstractions.Contracts.Responses.GitCommitResponses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hourly.Abstractions.Contracts.Responses.UserResponses;

namespace Hourly.Abstractions.Contracts.Responses.WorkSessionResponses
{
    public class WorkSessionResponse
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public UserSummaryResponse? User { get; set; }

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

        public ICollection<GitCommitSummaryResponse> GitCommits { get; set; } = new List<GitCommitSummaryResponse>();

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
