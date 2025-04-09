using Bogus;
using Hourly.Shared.Models;

namespace Hourly.IntergrationTests.Utilities.Factories
{
    internal static class UserFactory
    {
        public static List<User> CreateUsers(int count, List<Guid> departmentIds, List<Guid> roleIds)
        {

            var faker = new Faker<User>()
                .RuleFor(u => u.Id, f => Guid.NewGuid())
                .RuleFor(u => u.Name, f => f.Name.FullName())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.RoleId, f => f.PickRandom(roleIds))
                .RuleFor(u => u.DepartmentId, f => f.PickRandom(departmentIds))
                .RuleFor(u => u.GitEmail, f => f.Internet.Email())
                .RuleFor(u => u.GitUsername, f => f.Internet.UserName())
                .RuleFor(u => u.GitAccessToken, f => f.Internet.Password())
                .RuleFor(u => u.CreatedAt, f => f.Date.Past().ToUniversalTime())
                .RuleFor(u => u.UpdatedAt, f => f.Date.Recent().ToUniversalTime());

            return faker.Generate(count);
        }
    }
}
