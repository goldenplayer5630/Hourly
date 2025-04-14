using Hourly.Shared.Contracts.Responses.UserResponses;
using System.ComponentModel.DataAnnotations;

namespace Hourly.Shared.Contracts.Responses.DepartmentResponses
{
    public class DepartmentResponse
    {
        [Key]
        public Guid Id { get; init; }

        [Required]
        public string Name { get; set; }

        public ICollection<UserSummaryResponse> Users { get; set; } = new List<UserSummaryResponse>();

        [Required]
        public DateTime CreatedAt { get; init; }

        public DateTime? UpdatedAt { get; init; }
    }
}
