using Hourly.Data.Persistence.Converters;
using Hourly.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hourly.Data.Persistence.EntityConfigurations
{
    public class WorkSessionGitCommitConfiguration : IEntityTypeConfiguration<WorkSessionGitCommit>
    {
        public void Configure(EntityTypeBuilder<WorkSessionGitCommit> builder)
        {
            builder.HasKey(x => new { x.WorkSessionId, x.GitCommitId });

            builder.HasOne(x => x.WorkSession)
                .WithMany(ws => ws.WorkSessionGitCommits)
                .HasForeignKey(x => x.WorkSessionId);

            builder.HasOne(x => x.GitCommit)
                .WithMany(gc => gc.WorkSessionGitCommits)
                .HasForeignKey(x => x.GitCommitId);

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasConversion(DateTimeConverter.UtcDateTimeConverter);

            builder.Property(x => x.UpdatedAt)
                .HasConversion(DateTimeConverter.NullableUtcDateTimeConverter);

        }
    }
}
