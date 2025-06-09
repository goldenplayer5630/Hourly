using Hourly.Shared.Enums;

namespace Hourly.Abstractions.Entities
{
    public interface IUserContract
    {
        Guid Id { get; }
        Guid UserId { get; }
        string Name { get; }
        ContractType ContractType { get; }
        bool IsActive { get; }

        double MinWeeklyHours { get; }
        double MaxWeeklyHours { get; }
        double MinimumHoursPerMonth { get; }
        double MaximumHoursPerMonth { get; }

        double? GrossHourlyRate { get; }
        int? HolidayHoursPercentage { get; }
        bool MonthlyPaidHolidayHours { get; }

        DateTime StartDate { get; }
        DateTime? EndDate { get; }
        string? ContractFilePath { get; }
        string? Description { get; }

        DateTime CreatedAt { get; }
        DateTime? UpdatedAt { get; }

        IUser User { get; }

        IReadOnlyCollection<IWorkSession> WorkSessions { get; }

        void AssignToUser(IUser user);
        void Validate();
        void Update(IUserContract updatedContract);
    }
}
