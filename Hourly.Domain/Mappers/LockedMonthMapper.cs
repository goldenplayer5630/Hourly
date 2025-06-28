using Hourly.Domain.Contracts.Responses.LockedMonthResponses;
using Hourly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Domain.Mappers
{
    public static partial class LockedMonthMapper
    {
        public static LockedMonthResponse ToResponse(this LockedMonth entity)
        {
            return new LockedMonthResponse
            {
                Id = entity.Id,
                UserContractId = entity.UserContractId,
                Year = entity.Year,
                Month = entity.Month,
                UserContract = entity.UserContract
            };
        }

        public static LockedMonthSummaryResponse ToSummaryResponse(this LockedMonth entity)
        {
            return new LockedMonthSummaryResponse
            {
                Id = entity.Id,
                UserContractId = entity.UserContractId,
                Year = entity.Year,
                Month = entity.Month,
            };
        }
    }
}
