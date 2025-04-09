using Hourly.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;

namespace Hourly.Tests.Data.Utilities.Factories
{
    internal class DepartmentFactory
    {
        public static List<Department> CreateDepartments(int count)
        {
            var departmentFaker = new Faker<Department>()
                .RuleFor(d => d.Id, f => Guid.NewGuid())
                .RuleFor(d => d.Name, f => f.Commerce.Department())
                .RuleFor(u => u.CreatedAt, f => f.Date.Past().ToUniversalTime())
                .RuleFor(u => u.UpdatedAt, f => f.Date.Recent().ToUniversalTime());
            return departmentFaker.Generate(count);
        }
    }
}
