using Hourly.Shared.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Abstractions.Contracts.Responses.DepartmentResponses
{
    public class DepartmentSummaryResponse
    {
        [Key]
        public Guid Id { get; init; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime CreatedAt { get; init; }

        public DateTime? UpdatedAt { get; init; }
    }
}
