using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Hourly.Shared.Models;

namespace Hourly.Tests.Data.Utilities.Factories
{
    internal class RoleFactory
    {
        public static List<Role> CreateRoles()
        {
            var roles = new[] { "Admin", "User", "Guest", "Overflow1", "Overflow2"};

            var count = roles.Length;

            var index = 0;

            var faker = new Faker<Role>()
                .RuleFor(c => c.Id, Guid.NewGuid())
                .RuleFor(c => c.Name, f => roles[index++])
                .RuleFor(c => c.Permissions, f => f.Random.ListItem(new[] { "Read", "Write", "Execute" }))
                .RuleFor(c => c.CreatedAt, f => f.Date.Past())
                .RuleFor(c => c.UpdatedAt, f => f.Date.Recent());
            return faker.Generate(count);
        }
    }
}
