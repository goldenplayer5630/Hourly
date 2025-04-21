using Hourly.Shared.Contracts.Responses.UserResponses;
using System.ComponentModel.DataAnnotations;

namespace Hourly.Shared.Contracts.Responses.DepartmentResponses
{
    public class DepartmentResponse : DepartmentSummaryResponse
    {
        public ICollection<UserSummaryResponse> Users { get; set; } = new List<UserSummaryResponse>();
    }
}
