using Hourly.Domain.Contracts.Responses.SummaryResponse;
using Hourly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Domain.Mappers
{
    public static partial class YearlySummaryMapper
    {
        public static YearlySummaryResponse ToResponse(this YearlySummary yearlySummary)
        {
            return new YearlySummaryResponse
            {
                UserId = yearlySummary.UserId,
                Year = yearlySummary.Year,
                TotalRawEffectiveHours = yearlySummary.TotalRawEffectiveHours,
                TotalNetEffectiveHours = yearlySummary.TotalNetEffectiveHours,
                TotalTVTHoursAccrued = yearlySummary.TotalTVTHoursAccrued,
                TotalTVTHoursUsed = yearlySummary.TotalTVTHoursUsed,
                MonthlySummaries = yearlySummary.MonthlySummaries?.Select(ms => ms.ToResponse()).ToList() ?? new List<MonthlySummaryResponse>()
            };
        }
    }
}
