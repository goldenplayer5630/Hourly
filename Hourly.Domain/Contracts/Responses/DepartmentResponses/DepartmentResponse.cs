using Hourly.Domain.Contracts.Responses.UserResponses;

namespace Hourly.Domain.Contracts.Responses.DepartmentResponses
{
    public class DepartmentResponse : DepartmentSummaryResponse
    {
        public ICollection<UserSummaryResponse> Users { get; set; } = new List<UserSummaryResponse>();
    }
}
