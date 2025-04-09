using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Data.Persistence.Converters
{
    internal static class DateTimeConverter
    {
        public static readonly ValueConverter<DateTime, DateTime> UtcDateTimeConverter =
            new ValueConverter<DateTime, DateTime>(
                v => DateTime.SpecifyKind(v.ToUniversalTime(), DateTimeKind.Utc), // write
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc) // read
            );

        public static readonly ValueConverter<DateTime?, DateTime?> NullableUtcDateTimeConverter =
            new ValueConverter<DateTime?, DateTime?>(
                v => v.HasValue ? DateTime.SpecifyKind(v.Value.ToUniversalTime(), DateTimeKind.Utc) : v,
                v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v
            );

    }
}
