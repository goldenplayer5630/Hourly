using Hourly.Domain.Contracts.Responses.LockedMonthResponses;
using Hourly.Domain.Contracts.Responses.UserResponses;
using Hourly.Domain.Contracts.Responses.WorkSessionResponses;
using Hourly.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.Domain.Contracts.Responses.UserContractResponses
{
    public class UserContractResponse : UserContractSummaryResponse
    {
        // Navigation properties
        [ForeignKey(nameof(UserId))]
        public UserSummaryResponse User { get; init; } = null!;

        public ICollection<WorkSessionSummaryResponse> WorkSessions { get; init; } = new List<WorkSessionSummaryResponse>();

        public ICollection<LockedMonthSummaryResponse> LockedMonths { get; init; } = new List<LockedMonthSummaryResponse>();
    }
}
