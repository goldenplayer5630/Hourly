using Hourly.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace Hourly.Shared.Contracts.Requests.UserContractRequests
{
    public class UpdateUserContractRequest
    {
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
    }
}
