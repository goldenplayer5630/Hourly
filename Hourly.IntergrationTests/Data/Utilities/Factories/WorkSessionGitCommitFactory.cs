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
            var usedPairs = new HashSet<(Guid, Guid)>();
            var random = new Random();

            foreach (Guid workSessionId in workSessionIds)
            {
                var availableGitCommitIds = gitCommitIds.OrderBy(_ => random.Next()).ToList();

                foreach (var gitCommitId in availableGitCommitIds)
                {
                    var pair = (workSessionId, gitCommitId);

                    if (usedPairs.Contains(pair))
                    {
                        continue;
                    }

                    var workSessionGitCommitFaker = new Faker<WorkSessionGitCommit>()
                        .RuleFor(c => c.WorkSessionId, f => workSessionId)
                        .RuleFor(c => c.GitCommitId, f => gitCommitId)
                        .RuleFor(u => u.CreatedAt, f => f.Date.Past().ToUniversalTime())
                        .RuleFor(u => u.UpdatedAt, f => f.Date.Recent().ToUniversalTime());

                    workSessionGitCommits.Add(workSessionGitCommitFaker.Generate());

                    usedPairs.Add(pair);

                    if (workSessionGitCommits.Count(c => c.WorkSessionId == workSessionId) >= 10)
                    {
                        break;
                    }
                }
            }
            return workSessionGitCommits;
        }
    }
}
