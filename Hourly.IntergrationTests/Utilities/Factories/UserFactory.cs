using Bogus;
using Hourly.Domain.Entities;
using Hourly.Domain.Enums;

namespace Hourly.IntergrationTests.Utilities.Factories
{
    internal static class UserFactory
    {
        public static List<User> CreateUsers(int count, List<Department> departments)
        {
            var faker = new Faker();

            var users = new List<User>();

            for (int i = 0; i < count; i++)
            {
                var department = faker.PickRandom(departments);

                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Name = faker.Name.FullName(),
                    Email = faker.Internet.Email(),
                    GitEmail = faker.Internet.Email(),
                    GitUsername = faker.Internet.UserName(),
                    GitAccessToken = faker.Internet.Password(),
                    Role = faker.PickRandom<UserRole>(),
                    CreatedAt = faker.Date.Past().ToUniversalTime(),
                    UpdatedAt = faker.Date.Recent().ToUniversalTime(),
                };

                user.AssignToDepartment(department);

                users.Add(user);
            }

            return users;
        }
    }
}
