using Hourly.Domain.Entities;
using Hourly.Shared.Contracts.Responses.RoleResponses;

namespace Hourly.Domain.Mappers
{
    public static partial class RoleMapper
    {
        public static RoleResponse ToResponse(this Role entity)
        {
            return new RoleResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Permissions = entity.Permissions,
                Users = (entity.Users as List<User> ?? throw new Exception()).Select(u => u.ToSummaryResponse()).ToList(),
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }

        public static RoleSummaryResponse ToSummaryResponse(this Role entity)
        {
            return new RoleSummaryResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Permissions = entity.Permissions,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }
    }
}
