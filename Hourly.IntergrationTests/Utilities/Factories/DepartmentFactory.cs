using Bogus;

namespace Hourly.IntergrationTests.Utilities.Factories
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
