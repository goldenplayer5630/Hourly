using Hourly.Abstractions.Contracts.Responses.UserResponses;
using System.ComponentModel.DataAnnotations;

namespace Hourly.Abstractions.Contracts.Responses.RoleResponses
{
    public class RoleResponse
    {
        [Key]
        public Guid Id { get; init; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Permissions { get; set; } // Store as JSON string

        public ICollection<UserSummaryResponse> Users { get; set; } = new List<UserSummaryResponse>();

        [Required]
        public DateTime CreatedAt { get; init; }

        public DateTime? UpdatedAt { get; init; }
    }
}
