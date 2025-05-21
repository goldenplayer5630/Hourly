using Bogus;
using Hourly.Shared.Entities;

namespace Hourly.IntergrationTests.Utilities.Factories
{
    internal class WorkSessionFactory
    {
        public static List<WorkSession> CreateWorkSessions(List<Guid> userIds)
        {
            var workSessions = new List<WorkSession>();

            foreach (var userId in userIds)
            {
                var workSessionFaker = new Faker<WorkSession>()
                    .RuleFor(c => c.Id, f => Guid.NewGuid())
                    .RuleFor(c => c.UserId, _ => userId)
                    .RuleFor(c => c.TaskDescription, f => f.Lorem.Sentence())
                    .RuleFor(c => c.Factor, f => f.Random.Float(1.0f, 2.0f))
                    .RuleFor(c => c.StartTime, f =>
                    {
                        var past = f.Date.Past(1).ToUniversalTime().Date;
                        var hour = f.Random.Int(6, 14); // reasonable workday start
                        var minute = f.PickRandom(0, 15, 30, 45);
                        return new DateTime(past.Year, past.Month, past.Day, hour, minute, 0, DateTimeKind.Utc);
                    })
                    .RuleFor(c => c.EndTime, (f, c) =>
                    {
                        var durationInHours = f.Random.Int(2, 8);
                        var end = c.StartTime.AddHours(durationInHours);
                        var minute = f.PickRandom(0, 15, 30, 45);
                        end = new DateTime(end.Year, end.Month, end.Day, end.Hour, minute, 0, DateTimeKind.Utc);

                        if (end <= c.StartTime)
                        {
                            // Ensure end is strictly after start
                            end = c.StartTime.AddHours(1);
                            end = new DateTime(end.Year, end.Month, end.Day, end.Hour, 0, 0, DateTimeKind.Utc);
                        }

                        return end;
                    })
                    .RuleFor(c => c.BreakTime, f => f.PickRandom(0.25f, 0.5f, 0.75f, 1f))
                    .RuleFor(c => c.WBSO, f => f.Random.Bool())
                    .RuleFor(c => c.Locked, _ => false)
                    .RuleFor(c => c.OtherRemarks, f => f.Lorem.Sentence())
                    .RuleFor(c => c.CreatedAt, f => f.Date.Past(1).ToUniversalTime())
                    .RuleFor(c => c.UpdatedAt, f => f.Date.Recent(1).ToUniversalTime())
                    .FinishWith((f, c) =>
                    {
                        var totalEffectiveHours = c.RawEffectiveHours;

                        // Round to the nearest 0.25 hour for TVT increments
                        float maxTVT = (float)Math.Floor(totalEffectiveHours * 4) / 4f;

                        if (maxTVT > 0 && f.Random.Bool())
                        {
                            c.TVTAccruedHours = f.Random.Float(0f, maxTVT);
                            c.TVTAccruedHours = (float)Math.Floor(c.TVTAccruedHours * 4) / 4f;
                            c.TVTUsedHours = 0;
                        }
                        else if (maxTVT > 0)
                        {
                            c.TVTUsedHours = f.Random.Float(0f, maxTVT);
                            c.TVTUsedHours = (float)Math.Floor(c.TVTUsedHours * 4) / 4f;
                            c.TVTAccruedHours = 0;
                        }
                        else
                        {
                            c.TVTAccruedHours = 0;
                            c.TVTUsedHours = 0;
                        }

                        // Final validation
                        c.Validate();
                    });

                workSessions.AddRange(workSessionFaker.Generate(10));
            }

            return workSessions;
        }
    }
}
