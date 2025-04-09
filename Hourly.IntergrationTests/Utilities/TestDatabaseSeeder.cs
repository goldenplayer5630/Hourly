using Hourly.IntergrationTests.Utilities.Factories;
using Microsoft.EntityFrameworkCore;

namespace Hourly.IntergrationTests.Utilities
{
    internal class TestDatabaseSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // Creating simulation data for seeding database


            var departments = DepartmentFactory.CreateDepartments(3);
            var roles = RoleFactory.CreateRoles();
            var departmentIds = departments.Select(d => d.Id).ToList();
            var roleIds = roles.Select(r => r.Id).ToList();
            var users = UserFactory.CreateUsers(10, departmentIds, roleIds);
            var userIds = users.Select(u => u.Id).ToList();
            var gitRepositories = GitRepositoryFactory.CreateGitRepositories(20);
            var gitRepositoryIds = gitRepositories.Select(g => g.Id).ToList();
            var gitCommits = GitCommitFactory.CreateGitCommits(userIds, gitRepositoryIds);
            var gitCommitIds = gitCommits.Select(g => g.Id).ToList();
            var workSessions = WorkSessionFactory.CreateWorkSessions(userIds);
            var workSessionIds = workSessions.Select(w => w.Id).ToList();
            GCWSLinkFactory.LinkCommitsToWorkSessions(workSessions, gitCommits, 5);

            // Adding data to testdatabase
            context.Users.AddRange(users);
            context.Roles.AddRange(roles);
            context.Departments.AddRange(departments);
            context.GitRepositories.AddRange(gitRepositories);
            context.GitCommits.AddRange(gitCommits);
            context.WorkSessions.AddRange(workSessions);

            // Saving data to testdatabase
            await context.SaveChangesAsync();


            var workSessionGitCommits = await context.WorkSessions.ToListAsync();
        }
    }
}
