using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Hourly.Shared.Models;

namespace Hourly.Tests.Data.Utilities.Factories
{
    internal class WorkSessionGitCommitFactory
    {
        public static List<WorkSessionGitCommit> CreateWorkSessionGitCommits(List<Guid> workSessionIds, List<Guid> gitCommitIds)
        {
            var workSessionGitCommits = new List<WorkSessionGitCommit>();
            foreach (Guid workSessionId in workSessionIds)
            {
                var workSessionGitCommitFaker = new Faker<WorkSessionGitCommit>()
                    .RuleFor(c => c.WorkSessionId, workSessionId)
                    .RuleFor(c => c.GitCommitId, f => f.PickRandom(gitCommitIds))
                    .RuleFor(d => d.CreatedAt, f => f.Date.Past())
                    .RuleFor(d => d.UpdatedAt, f => f.Date.Recent());
                workSessionGitCommits.AddRange(workSessionGitCommitFaker.Generate(10));
            }
            return workSessionGitCommits;
        }
    }
}
