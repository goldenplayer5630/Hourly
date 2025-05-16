using Bogus;
using Hourly.Shared.Entities;

namespace Hourly.IntergrationTests.Utilities.Factories
{
    internal class WorkSessionFactory
    {
        public static List<WorkSession> CreateWorkSessions(List<Guid> userIds)
        {
            var workSessions = new List<WorkSession>();

            foreach (Guid userId in userIds)
            {
                var workSessionFaker = new Faker<WorkSession>()
                    .RuleFor(c => c.Id, f => Guid.NewGuid())
                    .RuleFor(c => c.UserId, f => userId)
                    .RuleFor(c => c.TaskDescription, f => f.Lorem.Paragraph())
                    .RuleFor(c => c.StartTime, f => f.Date.Recent().ToUniversalTime())
                    .RuleFor(c => c.EndTime, (f, c) => c.StartTime.AddHours(f.Random.Int(1, 8)).ToUniversalTime())
                    .RuleFor(c => c.Factor, (f, c) => f.Random.Float(0.5f, 2.0f))
                    .RuleFor(c => c.WBSO, f => f.Random.Bool())
                    .RuleFor(c => c.Locked, f => false)
                    .RuleFor(c => c.TVTAccruedHours, f => f.Random.Float(0, 100))
                    .RuleFor(c => c.TVTUsedHours, f => f.Random.Float(0, 100))
                    .RuleFor(c => c.OtherRemarks, f => f.Lorem.Paragraph())
                    .RuleFor(u => u.CreatedAt, f => f.Date.Past().ToUniversalTime())
                    .RuleFor(u => u.UpdatedAt, f => f.Date.Recent().ToUniversalTime());
                workSessions.AddRange(workSessionFaker.Generate(10));
            }

            return workSessions;
        }
    }
}
