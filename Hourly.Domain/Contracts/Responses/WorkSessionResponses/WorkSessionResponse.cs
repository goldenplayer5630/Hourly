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
        public UserContractSummaryResponse? UserContract { get; init; }

        public ICollection<GitCommitResponse> GitCommits { get; init; } = new List<GitCommitResponse>();
    }
}
