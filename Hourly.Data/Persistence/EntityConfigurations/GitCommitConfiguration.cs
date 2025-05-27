using Hourly.Data.Persistence.Converters;
using Hourly.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hourly.Data.Persistence.EntityConfigurations
{
    public class GitCommitConfiguration : IEntityTypeConfiguration<GitCommit>
    {
        public void Configure(EntityTypeBuilder<GitCommit> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.ExtCommitId)
                .IsRequired();

            builder.Property(x => x.ExtCommitShortId)
                .HasMaxLength(50);

            builder.Property(x => x.Title)
                .HasMaxLength(200);

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.Property(x => x.WebUrl)
                .HasMaxLength(500);

            builder.HasOne(x => x.Author)
                .WithMany("_gitCommits")
                .HasForeignKey(x => x.AuthorId);

            builder.HasOne(x => x.Repository)
                .WithMany("_gitCommits")
                .HasForeignKey(x => x.RepositoryId);

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasConversion(DateTimeConverter.UtcDateTimeConverter);

            builder.Property(x => x.UpdatedAt)
                .HasConversion(DateTimeConverter.NullableUtcDateTimeConverter);

            builder.HasMany(gc => gc.WorkSessions)
                .WithMany(ws => ws.GitCommits)
                .UsingEntity<Dictionary<string, object>>(
                    "git_commit_work_sessions",

                    j => j.HasOne<WorkSession>()
                          .WithMany()
                          .HasForeignKey("work_session_id")
                          .OnDelete(DeleteBehavior.Cascade),

                    j => j.HasOne<GitCommit>()
                          .WithMany()
                          .HasForeignKey("git_commit_id")
                          .OnDelete(DeleteBehavior.Cascade),

                    j =>
                    {
                        j.HasKey("git_commit_id", "work_session_id");
                        j.ToTable("git_commit_work_sessions");
                    });


        }
    }
}
