using Hourly.Shared.Contracts.Requests.UserContractRequests;
using System.Runtime.CompilerServices;
using Hourly.Shared.Contracts.Responses.UserContractResponses;
using Hourly.Shared.Entities;

namespace Hourly.Shared.Mappers
{
    public static partial class UserContractMapper
    {
        public static UserContract ToUserContract(this CreateUserContractRequest request)
        {
            return new UserContract
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                Name = request.Name,
                ContractType = request.ContractType,
                MinMonthlyHours = request.MinMonthlyHours,
                MaxMonthlyHours = request.MaxMonthlyHours,
                GrossHourlyRate = request.GrossHourlyRate,
                HolidayHoursPercentage = request.HolidayHoursPercentage,
                MonthlyPaidHolidayHours = request.MonthlyPaidHolidayHours,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                ContractFilePath = request.ContractFilePath,
                Description = request.Description,
            };
        }

        public static UserContract ToUserContract(this UpdateUserContractRequest request)
        {
            return new UserContract
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                Name = request.Name,
                ContractType = request.ContractType,
                MinMonthlyHours = request.MinMonthlyHours,
                MaxMonthlyHours = request.MaxMonthlyHours,
                GrossHourlyRate = request.GrossHourlyRate,
                HolidayHoursPercentage = request.HolidayHoursPercentage,
                MonthlyPaidHolidayHours = request.MonthlyPaidHolidayHours,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                ContractFilePath = request.ContractFilePath,
                Description = request.Description
            };
        }

        public static UserContractResponse ToResponse(this UserContract entity)
        {
            return new UserContractResponse
            {
                Id = entity.Id,
                UserId = entity.UserId,
                Name = entity.Name,
                ContractType = entity.ContractType,
                MinMonthlyHours = entity.MinMonthlyHours,
                MaxMonthlyHours = entity.MaxMonthlyHours,
                GrossHourlyRate = entity.GrossHourlyRate,
                HolidayHoursPercentage = entity.HolidayHoursPercentage,
                MonthlyPaidHolidayHours = entity.MonthlyPaidHolidayHours,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                ContractFilePath = entity.ContractFilePath,
                Description = entity.Description,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                User = entity.User.ToSummaryResponse(),
            };
        }
    }
}
