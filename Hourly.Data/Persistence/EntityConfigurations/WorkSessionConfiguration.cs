using Hourly.Data.Persistence.Converters;
using Hourly.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hourly.Data.Persistence.EntityConfigurations
{
    public class WorkSessionConfiguration : IEntityTypeConfiguration<WorkSession>
    {
        public void Configure(EntityTypeBuilder<WorkSession> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.TaskDescription)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(x => x.StartTime)
                .IsRequired();

            builder.Property(x => x.EndTime)
                .IsRequired();

            builder.Property(x => x.BreakTime)
                .IsRequired();

            builder.Property(x => x.Factor)
                .IsRequired();

            builder.Property(x => x.WBSO)
                .IsRequired();

            builder.Property(x => x.Locked)
                .IsRequired();

            builder.Property(x => x.OtherRemarks)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(x => x.TVTAccruedHours)
                .IsRequired();

            builder.Property(x => x.TVTUsedHours)
                .IsRequired();

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasConversion(DateTimeConverter.UtcDateTimeConverter);

            builder.Property(x => x.UpdatedAt)
                .HasConversion(DateTimeConverter.NullableUtcDateTimeConverter);

            builder.HasOne(x => x.UserContract)
                .WithMany("_workSessions")
                .HasForeignKey(x => x.UserContractId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Ignore(ws => ws.GitCommits);

            builder.HasMany<GitCommit>("_gitCommits")
                .WithMany("_workSessions")
                .UsingEntity(j => j.ToTable("GitCommitWorkSessions"));
        }
    }
}
