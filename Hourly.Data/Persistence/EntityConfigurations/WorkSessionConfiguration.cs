using Hourly.Shared.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            builder.HasMany(x => x.WorkSessionGitCommits)
                .WithOne(wsc => wsc.WorkSession)
                .HasForeignKey(wsc => wsc.WorkSessionId);

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
        }
    }
}
