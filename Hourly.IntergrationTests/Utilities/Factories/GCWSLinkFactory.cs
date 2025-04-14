using Hourly.Shared.Entities;

namespace Hourly.IntergrationTests.Utilities.Factories
{
    internal static class GCWSLinkFactory
    {
        public static void LinkCommitsToWorkSessions(List<WorkSession> workSessions, List<GitCommit> gitCommits, int maxCommitsPerSession = 5)
        {
            var rand = new Random();

            foreach (var session in workSessions)
            {
                int count = rand.Next(1, maxCommitsPerSession + 1);
                var randomCommits = gitCommits.OrderBy(_ => rand.Next()).Take(count);

                foreach (var commit in randomCommits)
                {
                    // Only add if not already linked
                    if (!session.GitCommits.Contains(commit))
                    {
                        session.GitCommits.Add(commit);
                    }
                }
            }
        }
    }
}
