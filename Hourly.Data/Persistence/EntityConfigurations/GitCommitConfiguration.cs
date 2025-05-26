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

            builder.Ignore(gc => gc.WorkSessions);

            builder.HasMany<WorkSession>("_workSessions")
                .WithMany("_gitCommits")
                .UsingEntity(j => j.ToTable("GitCommitWorkSessions"));


        }
    }
}
