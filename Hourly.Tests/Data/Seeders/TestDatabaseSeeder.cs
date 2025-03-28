using Hourly.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Tests.Seeders
{
    internal class TestDatabaseSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            if (!context.Roles.Any())
            {
                var adminRole = new Role { Id = Guid.NewGuid(), Name = "Admin" };
                var employeeRole = new Role { Id = Guid.NewGuid(), Name = "Employee" };

                context.Roles.AddRange(adminRole, employeeRole);
            }

            if (!context.Departments.Any())
            {
                var dept = new Department { Id = Guid.NewGuid(), Name = "Engineering" };
                context.Departments.Add(dept);
            }

            if (!context.Users.Any())
            {
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Name = "Test User",
                    Email = "test@example.com",
                    RoleId = context.Roles.First().Id,
                    DepartmentId = context.Departments.First().Id
                };

                context.Users.Add(user);
            }

            await context.SaveChangesAsync();
        }
    }
}
