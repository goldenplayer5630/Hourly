using Hourly.Shared.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<WorkSession> WorkSessions { get; set; }
    public DbSet<GitRepository> GitRepositories { get; set; }
    public DbSet<GitCommit> GitCommits { get; set; }
    public DbSet<WorkSessionGitCommit> WorkSessionGitCommits { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
