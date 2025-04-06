using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Hourly.Shared.Models;

namespace Hourly.Tests.Data.Utilities.Factories
{
    internal class GitRepositoryFactory
    {
        public static List<GitRepository> CreateGitRepositories(int count)
        {
            var faker = new Faker<GitRepository>()
                .RuleFor(r => r.Id, Guid.NewGuid())
                .RuleFor(r => r.ExtRepositoryId, f => f.Random.AlphaNumeric(8))
                .RuleFor(r => r.Name, f => f.Company.CompanyName())
                .RuleFor(r => r.Namespace, f => f.Company.CompanySuffix())
                .RuleFor(r => r.WebUrl, f => f.Internet.Url())
                .RuleFor(r => r.CreatedAt, f => f.Date.Recent())
                .RuleFor(r => r.UpdatedAt, f => f.Date.Recent());
            return faker.Generate(count);
        }
    }
}
