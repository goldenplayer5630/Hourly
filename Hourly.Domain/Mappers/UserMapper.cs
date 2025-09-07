using Hourly.Domain.Contracts.Requests.UserRequests;
using Hourly.Domain.Contracts.Responses.UserResponses;
using Hourly.Domain.Entities;

namespace Hourly.Domain.Mappers
{
    public static partial class UserMapper
    {
        public static UserResponse ToResponse(this User entity)
        {
            return new UserResponse
            {
                Id = entity.Id,
                ExternalOid = entity.ExternalOid,
                Name = entity.Name,
                Email = entity.Email,
                Role = entity.Role,
                DepartmentId = entity.DepartmentId,
                Department = entity.Department?.ToSummaryResponse(),
                GitEmail = entity.GitEmail,
                GitUsername = entity.GitUsername,
                GitAccessToken = entity.GitAccessToken,
                GitCommits = entity.GitCommits.Select(gc => gc.ToSummaryResponse()).ToList(),
                Contracts = entity.Contracts.Select(uc => uc.ToSummaryResponse()).ToList(),
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static UserSummaryResponse ToSummaryResponse(this User entity)
        {
            return new UserSummaryResponse
            {
                Id = entity.Id,
                ExternalOid = entity.ExternalOid,
                Name = entity.Name,
                Email = entity.Email,
                Role = entity.Role,
                DepartmentId = entity.DepartmentId,
                GitEmail = entity.GitEmail,
                GitUsername = entity.GitUsername,
                GitAccessToken = entity.GitAccessToken,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
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
                Role = request.Role,
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
                Role = request.Role,
            };
        }
    }
}
