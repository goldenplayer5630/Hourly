using Hourly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Abstractions.Services
{
    public interface ISummaryService
    {
        public Task<YearlySummary> GenerateYearlySummary(Guid userId, int year);

        public Task<MonthlySummary> GenerateMonthlySummary(Guid userId, int year, int month);
    }
}
