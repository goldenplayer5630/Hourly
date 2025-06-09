using Hourly.Shared.Contracts.Responses.UserResponses;
using Hourly.Shared.Contracts.Responses.WorkSessionResponses;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.Shared.Contracts.Responses.UserContractResponses
{
    public class UserContractResponse : UserContractSummaryResponse
    {
        // Navigation properties
        [ForeignKey("UserId")]
        public UserSummaryResponse User { get; set; }

        public ICollection<WorkSessionSummaryResponse> WorkSessions { get; set; } = new List<WorkSessionSummaryResponse>();
    }
}
