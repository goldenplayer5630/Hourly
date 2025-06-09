using Hourly.Domain.Contracts.Responses.GitCommitResponses;
using Hourly.Domain.Contracts.Responses.UserContractResponses;
using Hourly.Domain.Contracts.Responses.UserResponses;
using Hourly.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.Domain.Contracts.Responses.WorkSessionResponses
{
    public class WorkSessionResponse : WorkSessionSummaryResponse
    {

        [ForeignKey("UserContractId")]
        public UserContractSummaryResponse? UserContract { get; set; }

        public ICollection<GitCommitResponse> GitCommits { get; set; } = new List<GitCommitResponse>();

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
