using Bogus;

namespace Hourly.IntergrationTests.Utilities.Factories
{
    internal class RoleFactory
    {
        public static List<Role> CreateRoles()
        {
            var roles = new[] { "Admin", "User", "Guest", "Overflow1", "Overflow2" };

            var count = roles.Length;

            var index = 0;

            var faker = new Faker<Role>()
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.Name, f => roles[index++])
                .RuleFor(c => c.Permissions, f => f.Random.ListItem(new[] { "Read", "Write", "Execute" }))
                .RuleFor(u => u.CreatedAt, f => f.Date.Past().ToUniversalTime())
                .RuleFor(u => u.UpdatedAt, f => f.Date.Recent().ToUniversalTime());
            return faker.Generate(count);
        }
    }
}
