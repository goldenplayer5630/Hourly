using Hourly.Data.Persistence.Converters;
using Hourly.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hourly.Data.Persistence.EntityConfigurations
{
    public class GitCommitConfiguration : IEntityTypeConfiguration<GitCommit>
    {
        public void Configure(EntityTypeBuilder<GitCommit> builder)
        {
            builder.ToTable("git_commits");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.ExtCommitId)
                .IsRequired()
                .HasColumnName("ext_commit_id");

            builder.Property(x => x.ExtCommitShortId)
                .HasMaxLength(50)
                .HasColumnName("ext_commit_short_id");

            builder.Property(x => x.Title)
                .HasMaxLength(200)
                .HasColumnName("title");

            builder.Property(x => x.Comment)
                .HasColumnName("comment");

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasConversion(DateTimeConverter.UtcDateTimeConverter)
                .HasColumnName("created_at");

            builder.Property(x => x.UpdatedAt)
                .HasConversion(DateTimeConverter.NullableUtcDateTimeConverter)
                .HasColumnName("updated_at");

            builder.Property(x => x.AuthoredDate)
                .IsRequired()
                .HasConversion(DateTimeConverter.UtcDateTimeConverter)
                .HasColumnName("authored_date");

            builder.Property(x => x.WebUrl)
                .HasMaxLength(500)
                .HasColumnName("web_url");

            builder.Property(x => x.AuthorId)
                .HasColumnName("author_id");

            builder.Property(x => x.RepositoryId)
                .HasColumnName("repository_id");

            builder.HasOne(x => x.Author)
                .WithMany(a => a.GitCommits)
                .HasForeignKey(x => x.AuthorId);

            builder.HasOne(x => x.Repository)
                .WithMany(r => r.GitCommits)
                .HasForeignKey(x => x.RepositoryId);

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
