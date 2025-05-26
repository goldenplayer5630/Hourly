using Hourly.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Hourly.Shared.Exceptions;

namespace Hourly.Shared.Entities
{
    public class UserContract
    {
        private readonly List<WorkSession> _workSessions = new();

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
        public double MinWeeklyHours { get; set; }
        [Required]
        public double MaxWeeklyHours { get; set; }

        public double MinimumHoursPerMonth 
        {
            get
            {
                return (MinWeeklyHours * 52) / 12;
            }
        }

        public double MaximumHoursPerMonth
        {
            get
            {
                return (MaxWeeklyHours * 52) / 12;
            }
        }

        public double? GrossHourlyRate { get; set; }
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

        // Navigation properties
        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
        public IReadOnlyCollection<WorkSession> WorkSessions => _workSessions.AsReadOnly();

        public void AssignToUser(User user)
        {
            if (UserId == user.Id)
            {
                throw new DomainValidationException("Contract is already assigned to this user.");
            }
            UserId = user.Id;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Validate()
        {
            if (MinWeeklyHours < 0 || MaxWeeklyHours < 0)
                throw new DomainValidationException("Weekly hours cannot be negative.");
            if (MinWeeklyHours > MaxWeeklyHours)
                throw new DomainValidationException("Minimum weekly hours cannot exceed maximum weekly hours.");
            if (StartDate > EndDate)
                throw new DomainValidationException("Start date cannot be after end date.");
        }
    }
}
