using Hourly.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<IUser> Users { get; set; }
    public DbSet<IRole> Roles { get; set; }
    public DbSet<IDepartment> Departments { get; set; }
    public DbSet<IWorkSession> WorkSessions { get; set; }
    public DbSet<IGitRepository> GitRepositories { get; set; }
    public DbSet<IGitCommit> GitCommits { get; set; }
    public DbSet<IUserContract> UserContracts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
