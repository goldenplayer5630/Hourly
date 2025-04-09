using Bogus;
using Hourly.Shared.Models;

namespace Hourly.IntergrationTests.Utilities.Factories
{
    internal class GitRepositoryFactory
    {
        public static List<GitRepository> CreateGitRepositories(int count)
        {
            var faker = new Faker<GitRepository>()
                .RuleFor(r => r.Id, f => Guid.NewGuid())
                .RuleFor(r => r.ExtRepositoryId, f => f.Random.AlphaNumeric(8))
                .RuleFor(r => r.Name, f => f.Company.CompanyName())
                .RuleFor(r => r.Namespace, f => f.Company.CompanySuffix())
                .RuleFor(r => r.WebUrl, f => f.Internet.Url())
                .RuleFor(u => u.CreatedAt, f => f.Date.Past().ToUniversalTime())
                .RuleFor(u => u.UpdatedAt, f => f.Date.Recent().ToUniversalTime());
            return faker.Generate(count);
        }
    }
}
