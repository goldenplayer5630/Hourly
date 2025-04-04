using Hourly.Shared.Models;
using Hourly.Tests.Data.Utilities.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Tests.Data.Utilities
{
    internal class TestDatabaseSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // Creating simulation data for seeding database
            var departments = DepartmentFactory.CreateDepartments(5);


            // Adding data to testdatabase
            context.Departments.AddRange(departments);

            // Saving data to testdatabase
            await context.SaveChangesAsync();
        }
    }
}
