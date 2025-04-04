using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hourly.Shared.Models;
using Bogus;

namespace Hourly.Tests.Data.Utilities.Factories
{
    internal static class UserFactory
    {
        public static List<User> CreateUsers(int count)
        {
            var faker = new Faker<User>()
                .RuleFor(u => u.Id, f => Guid.NewGuid())
                .RuleFor(u => u.Name, f => f.Name.FullName())
                .RuleFor(u => u.Email, f => f.Internet.Email())
                .RuleFor(u => u.RoleId, f => f.Random.Guid())
                .RuleFor(u => u.DepartmentId, f => f.Random.Guid())
                .RuleFor(u => u.GitEmail, f => f.Internet.Email())
                .RuleFor(u => u.GitUsername, f => f.Internet.UserName())
                .RuleFor(u => u.GitAccessToken, f => f.Internet.Password())
                .RuleFor(u => u.CreatedAt, f => f.Date.Past())
                .RuleFor(u => u.UpdatedAt, f => f.Date.Recent());

            return faker.Generate(count);
        }
    }
}
