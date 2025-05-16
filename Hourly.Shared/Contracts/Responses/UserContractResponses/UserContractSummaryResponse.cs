using Hourly.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Shared.Contracts.Responses.UserContractResponses
{
    public class UserContractSummaryResponse
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public ContractTypes ContractType { get; set; }
        public bool IsActive { get; set; }

        [Required]
        public int MinMonthlyHours { get; set; }
        [Required]
        public int MaxMonthlyHours { get; set; }

        public float? GrossHourlyRate { get; set; }
        public int? HolidayHoursPercentage { get; set; }
        public bool MonthlyPaidHolidayHours { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? ContractFilePath { get; set; }
        public string? Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
