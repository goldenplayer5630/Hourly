using Hourly.Data.Persistence.Converters;
using Hourly.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasConversion(DateTimeConverter.UtcDateTimeConverter);

            builder.Property(x => x.UpdatedAt)
                .HasConversion(DateTimeConverter.NullableUtcDateTimeConverter);

        }
    }
}
