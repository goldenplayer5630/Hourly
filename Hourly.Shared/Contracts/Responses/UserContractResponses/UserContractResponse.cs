using Hourly.Shared.Contracts.Responses.UserResponses;
using Hourly.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.Shared.Contracts.Responses.UserContractResponses
{
    public class UserContractResponse
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

        // Navigation properties
        [ForeignKey("UserId")]
        public UserSummaryResponse User { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
