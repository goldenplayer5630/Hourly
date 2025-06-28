using Hourly.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hourly.Abstractions.Services
{
    public interface ITVTHoursService
    {
        Task<UserContract> UpdateTVTHourBalance(UserContract userContract, float hours);
    }
}
