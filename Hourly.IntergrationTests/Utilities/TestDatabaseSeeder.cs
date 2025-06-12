using Hourly.IntergrationTests.Utilities.Factories;

namespace Hourly.IntergrationTests.Utilities
{
    public static class TestDatabaseSeeder
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // Creating simulation data for seeding database


            var departments = DepartmentFactory.CreateDepartments(3);
            var roles = RoleFactory.CreateRoles();
            var departmentIds = departments.Select(d => d.Id).ToList();
            var roleIds = roles.Select(r => r.Id).ToList();
            var users = UserFactory.CreateUsers(10, departments, roles);
            var userIds = users.Select(u => u.Id).ToList();
            var userContracts = UserContractFactory.CreateUserContracts(users);
            var gitRepositories = GitRepositoryFactory.CreateGitRepositories(20);
            var gitRepositoryIds = gitRepositories.Select(g => g.Id).ToList();
            var gitCommits = GitCommitFactory.CreateGitCommits(users, gitRepositories);
            var gitCommitIds = gitCommits.Select(g => g.Id).ToList();
            var workSessions = WorkSessionFactory.CreateWorkSessions(userContracts, 100);
            var workSessionIds = workSessions.Select(w => w.Id).ToList();
            GCWSLinkFactory.LinkCommitsToWorkSessions(workSessions, gitCommits, 5);

            // Adding data to testdatabase
            context.Users.AddRange(users);
            context.Roles.AddRange(roles);
            context.Departments.AddRange(departments);
            context.GitRepositories.AddRange(gitRepositories);
            context.GitCommits.AddRange(gitCommits);
            context.WorkSessions.AddRange(workSessions);
            context.UserContracts.AddRange(userContracts);

            // Saving data to testdatabase
            await context.SaveChangesAsync();
        }
    }
}
