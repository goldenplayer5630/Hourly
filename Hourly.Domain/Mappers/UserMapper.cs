using Hourly.Domain.Entities;
using Hourly.Shared.Contracts.Requests.UserRequests;
using Hourly.Shared.Contracts.Responses.UserResponses;

namespace Hourly.Domain.Mappers
{
    public static partial class UserMapper
    {
        public static UserResponse ToResponse(this User entity)
        {
            return new UserResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                RoleId = entity.RoleId,
                Role = (entity.Role as Role ?? throw new Exception()).ToSummaryResponse(),
                DepartmentId = entity.DepartmentId,
                Department = (entity.Department as Department ?? throw new Exception())?.ToSummaryResponse(),
                GitEmail = entity.GitEmail,
                GitUsername = entity.GitUsername,
                GitAccessToken = entity.GitAccessToken,
                TVTHourBalance = entity.TVTHourBalance,
                GitCommits = (entity.GitCommits as List<GitCommit> ?? throw new Exception()).Select(gc => gc.ToSummaryResponse()).ToList(),
                Contracts = (entity.Contracts as List<UserContract> ?? throw new Exception()).Select(uc => uc.ToSummaryResponse()).ToList(),
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static UserSummaryResponse ToSummaryResponse(this User entity)
        {
            return new UserSummaryResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                RoleId = entity.RoleId,
                Role = (entity.Role as Role ?? throw new Exception()).ToSummaryResponse(),
                DepartmentId = entity.DepartmentId,
                GitEmail = entity.GitEmail,
                GitUsername = entity.GitUsername,
                GitAccessToken = entity.GitAccessToken,
                TVTHourBalance = entity.TVTHourBalance,
            };
        }

        public static User ToUser(this CreateUserRequest request)
        {
            return new User
            {
                Name = request.Name,
                Email = request.Email,
                GitEmail = request.GitEmail,
                GitUsername = request.GitUsername,
                GitAccessToken = request.GitAccessToken,
                TVTHourBalance = request.TVTHourBalance,
                RoleId = request.RoleId,
            };
        }

        public static User ToUser(this UpdateUserRequest request, Guid id)
        {
            return new User
            {
                Id = id,
                Name = request.Name,
                Email = request.Email,
                GitEmail = request.GitEmail,
                GitUsername = request.GitUsername,
                GitAccessToken = request.GitAccessToken,
                TVTHourBalance = request.TVTHourBalance,
                RoleId = request.RoleId,
            };
        }
    }
}
