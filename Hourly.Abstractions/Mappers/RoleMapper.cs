using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hourly.Abstractions.Contracts.Responses.RoleResponses;
using Hourly.Shared.Entities;

namespace Hourly.Abstractions.Mappers
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
                Users = entity.Users.Select(u => u.ToSummaryResponse()).ToList(),
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
