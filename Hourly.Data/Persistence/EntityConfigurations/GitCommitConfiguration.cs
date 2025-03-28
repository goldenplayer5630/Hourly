﻿using Hourly.Shared.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                .WithMany(u => u.GitCommits)
                .HasForeignKey(x => x.AuthorId);

            builder.HasOne(x => x.Repository)
                .WithMany(r => r.GitCommits)
                .HasForeignKey(x => x.RepositoryId);

            builder.HasMany(x => x.WorkSessionGitCommits)
                .WithOne(wsc => wsc.GitCommit)
                .HasForeignKey(wsc => wsc.GitCommitId);
        }
    }
}
