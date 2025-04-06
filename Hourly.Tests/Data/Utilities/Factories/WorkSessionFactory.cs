using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Hourly.Shared.Models;
using Org.BouncyCastle.Bcpg;

namespace Hourly.Tests.Data.Utilities.Factories
{
    internal class WorkSessionFactory
    {
        public static List<WorkSession> CreateWorkSessions(List<Guid> userIds)
        {
            var workSessions = new List<WorkSession>();

            foreach (Guid userId in userIds)
            {
                var workSessionFaker = new Faker<WorkSession>()
                    .RuleFor(c => c.Id, Guid.NewGuid())
                    .RuleFor(c => c.UserId, userId)
                    .RuleFor(c => c.TaskDescription, f => f.Lorem.Paragraph())
                    .RuleFor(c => c.StartTime, f => f.Date.Recent())
                    .RuleFor(c => c.EndTime, (f, c) => c.StartTime.AddHours(f.Random.Int(1, 8)))
                    .RuleFor(c => c.Factor, (f, c) => f.Random.Float(0.5f, 2.0f))
                    .RuleFor(c => c.WBSO, f => f.Random.Bool())
                    .RuleFor(c => c.OtherRemarks, f => f.Lorem.Paragraph())
                    .RuleFor(d => d.CreatedAt, f => f.Date.Past())
                    .RuleFor(d => d.UpdatedAt, f => f.Date.Recent());
                workSessions.AddRange(workSessionFaker.Generate(10));
            }

            return workSessions;
        }
    }
}
