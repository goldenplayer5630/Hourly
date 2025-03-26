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
        }
    }
}
