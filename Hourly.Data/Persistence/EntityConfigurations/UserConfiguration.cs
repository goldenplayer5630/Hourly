using Hourly.Data.Persistence.Converters;
using Hourly.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hourly.Data.Persistence.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Email)
                .IsRequired();

            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.GitEmail)
                .IsRequired(false);

            builder.Property(x => x.GitUsername)
                .IsRequired(false);

            builder.Property(x => x.GitAccessToken)
                .IsRequired(false);

            builder.Property(x => x.TVTAccruedHours)
                .IsRequired();

            builder.HasOne(x => x.Role)
                .WithMany(r => r.Users)
                .IsRequired()
                .HasForeignKey(x => x.RoleId);

            builder.HasOne(x => x.Department)
                .WithMany(d => d.Users)
                .IsRequired(false)
                .HasForeignKey(x => x.DepartmentId);

            builder.HasMany(x => x.WorkSessions)
                .WithOne(ws => ws.User)
                .HasForeignKey(ws => ws.UserId);

            builder.HasMany(x => x.GitCommits)
                .WithOne(gc => gc.Author)
                .HasForeignKey(gc => gc.AuthorId);

            builder.HasMany(x => x.Contracts)
                .WithOne(uc => uc.User)
                .HasForeignKey(uc => uc.UserId);

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasConversion(DateTimeConverter.UtcDateTimeConverter);

            builder.Property(x => x.UpdatedAt)
                .HasConversion(DateTimeConverter.NullableUtcDateTimeConverter);

        }
    }

}

