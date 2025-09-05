using Hourly.Domain.Contracts.Responses.SummaryResponse;
using Hourly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Domain.Mappers
{
    public static partial class MonthlySummaryMapper
    {
        public static MonthlySummaryResponse ToResponse(this MonthlySummary summary)
        {
            return new MonthlySummaryResponse
            {
                UserId = summary.UserContractId,
                Year = summary.Year,
                Month = summary.Month,
                TotalRawEffectiveHours = summary.TotalRawEffectiveHours,
                TotalNetEffectiveHours = summary.TotalNetEffectiveHours,
                TotalTVTHoursAccrued = summary.TotalTVTHoursAccrued,
                TotalTVTHoursUsed = summary.TotalTVTHoursUsed
            };
        }
    }
}
