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
        public static List<Department> CreateDepartments(List<Guid> managerIds, int count)
        {
            var departments = new List<Department>();
            var random = new Random();

            for (int i = 0; i < count; i++)
            {
                var managerId = managerIds[random.Next(managerIds.Count)];
                var departmentFaker = new Faker<Department>()
                    .RuleFor(d => d.Id, f => Guid.NewGuid())
                    .RuleFor(d => d.Name, f => f.Commerce.Department())
                    .RuleFor(d => d.ManagerId, (f, d) => managerId)
                    .RuleFor(d => d.CreatedAt, f => f.Date.Past())
                    .RuleFor(d => d.UpdatedAt, f => f.Date.Recent());
                departments.Add(departmentFaker.Generate());
            }

            return departments;
        }
    }
}
