using Hourly.Data.Persistence.Converters;
using Hourly.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hourly.Data.Persistence.EntityConfigurations
{
    public class UserContractConfiguration : IEntityTypeConfiguration<UserContract>
    {
        public void Configure(EntityTypeBuilder<UserContract> builder)
        {
            builder.HasKey(uc => uc.Id);

            builder.Property(uc => uc.Id)
                .IsRequired();

            builder.Property(uc => uc.UserId)
                .IsRequired();

            builder.Property(uc => uc.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(uc => uc.ContractType)
                .IsRequired();

            builder.Property(uc => uc.IsActive)
                .IsRequired();

            builder.Property(uc => uc.MinWeeklyHours)
                .IsRequired();

            builder.Property(uc => uc.MaxWeeklyHours)
                .IsRequired();

            builder.Property(uc => uc.GrossHourlyRate)
                .IsRequired(false);

            builder.Property(uc => uc.HolidayHoursPercentage)
                 .IsRequired(false);

            builder.Property(uc => uc.MonthlyPaidHolidayHours)
                .IsRequired();

            builder.Property(uc => uc.StartDate)
                .IsRequired()
                .HasConversion(DateTimeConverter.UtcDateTimeConverter);

            builder.Property(uc => uc.EndDate)
                .IsRequired(false)
                .HasConversion(DateTimeConverter.NullableUtcDateTimeConverter);

            builder.Property(uc => uc.ContractFilePath)
                .IsRequired(false)
                .HasMaxLength(500);

            builder.Property(uc => uc.Description)
                                .IsRequired(false)
                .HasMaxLength(1000);

            builder.Property(uc => uc.CreatedAt)
                .IsRequired()
                .HasConversion(DateTimeConverter.UtcDateTimeConverter);

            builder.Property(uc => uc.UpdatedAt)
                .HasConversion(DateTimeConverter.NullableUtcDateTimeConverter);

            builder.HasOne(uc => uc.User)
                .WithMany()
                .HasForeignKey(uc => uc.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}