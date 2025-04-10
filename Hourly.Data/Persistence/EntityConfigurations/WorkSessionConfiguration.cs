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

            builder.HasOne(x => x.User)
                .WithMany(u => u.WorkSessions)
                .HasForeignKey(x => x.UserId);

            builder.HasMany(x => x.GitCommits)
                .WithMany(x => x.WorkSessions)
                .UsingEntity(j => j.ToTable("GitCommitWorkSessions"));

            builder.Property(x => x.TaskDescription)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(x => x.StartTime)
                .IsRequired();

            builder.Property(x => x.EndTime)
                .IsRequired();

            builder.Property(x => x.Factor)
                .IsRequired();

            builder.Property(x => x.WBSO)
                .IsRequired();

            builder.Property(x => x.OtherRemarks)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasConversion(DateTimeConverter.UtcDateTimeConverter);

            builder.Property(x => x.UpdatedAt)
                .HasConversion(DateTimeConverter.NullableUtcDateTimeConverter);

        }
    }
}
