using Bogus;
using Hourly.Shared.Entities;

namespace Hourly.IntergrationTests.Utilities.Factories
{
    internal class GitCommitFactory
    {
        public static List<GitCommit> CreateGitCommits(List<User> authors, List<GitRepository> repositories)
        {
            var gitCommits = new List<GitCommit>();
            var faker = new Faker();

            foreach (var author in authors)
            {
                for (int i = 0; i < 10; i++)
                {
                    var repo = faker.PickRandom(repositories);

                    var commit = new GitCommit
                    {
                        Id = Guid.NewGuid(),
                        ExtCommitId = faker.Random.AlphaNumeric(40),
                        ExtCommitShortId = "", // set below
                        Title = faker.Lorem.Sentence(5, 7),
                        AuthoredDate = faker.Date.Between(DateTime.UtcNow.AddDays(-3), DateTime.UtcNow),
                        Comment = faker.Lorem.Paragraph(),
                        WebUrl = faker.Internet.Url(),
                        CreatedAt = faker.Date.Past().ToUniversalTime(),
                        UpdatedAt = faker.Date.Recent().ToUniversalTime()
                    };

                    commit.ExtCommitShortId = commit.ExtCommitId.Substring(0, 7);
                    commit.AssignToAuthor(author);
                    commit.AssignToRepository(repo);

                    gitCommits.Add(commit);
                }
            }

            return gitCommits;
        }
    }
}
