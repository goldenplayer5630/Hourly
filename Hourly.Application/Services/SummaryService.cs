using Hourly.Abstractions.Services;
using Hourly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Hourly.Application.Services
{
    public class SummaryService : ISummaryService
    {
        private readonly IWorkSessionService _workSessionService;

        public SummaryService(IWorkSessionService workSessionService)
        {
            _workSessionService = workSessionService;
        }

        public async Task<MonthlySummary> GenerateMonthlySummary(Guid userContractId, int year, int month)
        {
            var workSessions = await _workSessionService.Filter(userContractId, year, month, null);

            var rawEffectiveHours = workSessions.Sum(ws => ws.RawEffectiveHours);
            var netEffectiveHOurs = workSessions.Sum(ws => ws.NetEffectiveHours);
            var tvtAccruedHours = workSessions.Sum(ws => ws.TVTAccruedHours);
            var tvtUsedHours = workSessions.Sum(ws => ws.TVTUsedHours);

            return new MonthlySummary
            {
                Year = year,
                Month = month,
                TotalRawEffectiveHours = rawEffectiveHours,
                TotalNetEffectiveHours = netEffectiveHOurs,
                TotalTVTHoursAccrued = tvtAccruedHours,
                TotalTVTHoursUsed = tvtUsedHours,
                UserContractId = userContractId
            };
        }

        public async Task<YearlySummary> GenerateYearlySummary(Guid userContractId, int year)
        {
            var workSessions = await _workSessionService.Filter(userContractId, year, null, null);

            var rawEffectiveHours = workSessions.Sum(ws => ws.RawEffectiveHours);
            var netEffectiveHOurs = workSessions.Sum(ws => ws.NetEffectiveHours);
            var tvtAccruedHours = workSessions.Sum(ws => ws.TVTAccruedHours);
            var tvtUsedHours = workSessions.Sum(ws => ws.TVTUsedHours);

            var monthlySummaries = new List<MonthlySummary>();
            for (int month = 1; month <= 12; month++)
            {
                var monthlySummary = await GenerateMonthlySummary(userContractId, year, month);
                monthlySummaries.Add(monthlySummary);
            }

            return new YearlySummary
            {
                Year = year,
                TotalRawEffectiveHours = rawEffectiveHours,
                TotalNetEffectiveHours = netEffectiveHOurs,
                TotalTVTHoursAccrued = tvtAccruedHours,
                TotalTVTHoursUsed = tvtUsedHours,
                UserContractId = userContractId,
                MonthlySummaries = monthlySummaries
            };
        }
    }
}
