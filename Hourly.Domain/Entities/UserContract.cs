using Hourly.Abstractions.Entities;
using Hourly.Shared.Enums;
using Hourly.Shared.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hourly.Domain.Entities
{
    public class UserContract : IUserContract
    {
        private readonly List<IWorkSession> _workSessions = new();

        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public ContractType ContractType { get; set; }
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
        public IUser User { get; set; } = null!;
        public IReadOnlyCollection<IWorkSession> WorkSessions => _workSessions.AsReadOnly();

        public void Update(IUserContract updatedContract)
        {
            UserId = updatedContract.UserId;
            Name = updatedContract.Name;
            ContractType = updatedContract.ContractType;
            IsActive = updatedContract.IsActive;
            MinWeeklyHours = updatedContract.MinWeeklyHours;
            MaxWeeklyHours = updatedContract.MaxWeeklyHours;
            GrossHourlyRate = updatedContract.GrossHourlyRate;
            HolidayHoursPercentage = updatedContract.HolidayHoursPercentage;
            MonthlyPaidHolidayHours = updatedContract.MonthlyPaidHolidayHours;
            StartDate = updatedContract.StartDate;
            EndDate = updatedContract.EndDate;
            ContractFilePath = updatedContract.ContractFilePath;
            Description = updatedContract.Description;
            UpdatedAt = DateTime.UtcNow;

            Validate();
        }

        public void AssignToUser(IUser user)
        {
            User = user;
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
