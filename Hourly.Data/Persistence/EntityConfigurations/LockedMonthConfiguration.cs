using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Hourly.Domain.Entities;

namespace Hourly.Data.Persistence.EntityConfigurations
{
    public class LockedMonthConfiguration : IEntityTypeConfiguration<LockedMonth>
    {
        public void Configure(EntityTypeBuilder<LockedMonth> builder)
        {
            builder.ToTable("locked_month");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");

            builder.Property(x => x.UserContractId)
                .IsRequired()
                .HasColumnName("user_contract_id");

            builder.Property(x => x.Year)
                .IsRequired()
                .HasColumnName("year");

            builder.Property(x => x.Month)
                .IsRequired()
                .HasColumnName("month");

            builder.HasOne(x => x.UserContract)
                .WithMany(x => x.LockedMonths)
                .HasForeignKey(x => x.UserContractId);
        }
    }
}
