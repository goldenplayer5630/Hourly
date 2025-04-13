using Hourly.Abstractions.Contracts.Responses.UserResponses;
using Hourly.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
