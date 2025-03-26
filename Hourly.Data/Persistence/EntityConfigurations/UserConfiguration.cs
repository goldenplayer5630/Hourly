using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Hourly.Shared.Models;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Email)
            .IsRequired();

        builder.HasOne(x => x.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(x => x.RoleId);

        builder.HasOne(x => x.Department)
            .WithMany(d => d.Users)
            .HasForeignKey(x => x.DepartmentId);

        builder.HasMany(x => x.WorkSessions)
            .WithOne(ws => ws.User)
            .HasForeignKey(ws => ws.UserId);

        builder.HasMany(x => x.GitCommits)
            .WithOne(gc => gc.Author)
            .HasForeignKey(gc => gc.AuthorId);
    }
}
