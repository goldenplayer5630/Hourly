using Hourly.Data.Persistence.Converters;
using Hourly.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hourly.Data.Persistence.EntityConfigurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired();

            builder.Property(x => x.Permissions)
                .IsRequired();

            builder.HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.CreatedAt)
                .IsRequired()
                .HasConversion(DateTimeConverter.UtcDateTimeConverter);

            builder.Property(x => x.UpdatedAt)
                .HasConversion(DateTimeConverter.NullableUtcDateTimeConverter);

        }
    }

}
