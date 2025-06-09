using Hourly.Domain.Contracts.Responses.UserResponses;

namespace Hourly.Domain.Contracts.Responses.RoleResponses
{
    public class RoleResponse : RoleSummaryResponse
    {
        public ICollection<UserSummaryResponse> Users { get; set; } = new List<UserSummaryResponse>();
    }
}
