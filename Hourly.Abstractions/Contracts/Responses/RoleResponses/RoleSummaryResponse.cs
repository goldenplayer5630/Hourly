using Hourly.Abstractions.Contracts.Responses.UserResponses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Abstractions.Contracts.Responses.RoleResponses
{
    public class RoleSummaryResponse
    {
        [Key]
        public Guid Id { get; init; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Permissions { get; set; } // Store as JSON string

        [Required]
        public DateTime CreatedAt { get; init; }

        public DateTime? UpdatedAt { get; init; }
    }
}
