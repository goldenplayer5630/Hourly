using Hourly.Shared.Contracts.Responses.UserResponses;
using System.ComponentModel.DataAnnotations;

namespace Hourly.Shared.Contracts.Responses.RoleResponses
{
    public class RoleResponse : RoleSummaryResponse
    {
        public ICollection<UserSummaryResponse> Users { get; set; } = new List<UserSummaryResponse>();
    }
}
