using Bogus;
using Hourly.Shared.Models;

namespace Hourly.IntergrationTests.Utilities.Factories
{
    internal class GitCommitFactory
    {
        public static List<GitCommit> CreateGitCommits(List<Guid> authorIds, List<Guid> repositoryIds)
        {
            var GitCommits = new List<GitCommit>();

            foreach (Guid authorId in authorIds)
            {
                var authorFaker = new Faker<GitCommit>()
                    .RuleFor(c => c.Id, f => Guid.NewGuid())
                    .RuleFor(c => c.AuthorId, authorId)
                    .RuleFor(c => c.RepositoryId, f => f.PickRandom(repositoryIds))
                    .RuleFor(c => c.ExtCommitId, f => f.Random.AlphaNumeric(40))
                    .RuleFor(c => c.ExtCommitShortId, (f, c) => c.ExtCommitId.Substring(0, 7))
                    .RuleFor(c => c.Title, f => f.Lorem.Sentence(5, 7))
                    .RuleFor(c => c.Comment, f => f.Lorem.Paragraph())
                    .RuleFor(c => c.WebUrl, f => f.Internet.Url())
                    .RuleFor(u => u.CreatedAt, f => f.Date.Past().ToUniversalTime())
                    .RuleFor(u => u.UpdatedAt, f => f.Date.Recent().ToUniversalTime());
                GitCommits.AddRange(authorFaker.Generate(10));
            }

            return GitCommits;
        }
    }
}
