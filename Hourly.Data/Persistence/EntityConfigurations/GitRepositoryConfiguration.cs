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
    public class GitRepositoryConfiguration : IEntityTypeConfiguration<GitRepository>
    {
        public void Configure(EntityTypeBuilder<GitRepository> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.ExtRepositoryId).IsRequired();
            builder.Property(x => x.Namespace).HasMaxLength(255);
            builder.Property(x => x.WebUrl).HasMaxLength(500);

            builder.HasMany(x => x.GitCommits)
                .WithOne(c => c.Repository)
                .HasForeignKey(c => c.RepositoryId);
        }
    }
}
