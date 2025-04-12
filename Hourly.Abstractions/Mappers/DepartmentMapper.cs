using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hourly.Shared.Entities;
using Hourly.Abstractions.Contracts.Responses.DepartmentResponse;
using Hourly.Abstractions.Contracts.Requests.DepartmentRequests;

namespace Hourly.Abstractions.Mappers
{
    public static partial class DepartmentMapper
    {
        public static DepartmentResponse ToResponse(this Department department)
        {
            return new DepartmentResponse
            {
                Id = department.Id,
                Name = department.Name,
                Users = department.Users,
                CreatedAt = department.CreatedAt,
                UpdatedAt = department.UpdatedAt
            };
        }

        public static Department ToDepartment(this CreateDepartmentRequest request)
        {
            return new Department
            {
                Name = request.Name
            };
        }

        public static Department ToDepartment(this UpdateDepartmentRequest request, Guid id)
        {
            return new Department
            {
                Id = id,
                Name = request.Name
            };
        }

    }
}
