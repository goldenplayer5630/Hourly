using Bogus;
using Hourly.Shared.Entities;

namespace Hourly.IntergrationTests.Utilities.Factories
{
    internal class WorkSessionFactory
    {
        public static List<WorkSession> CreateWorkSessions(List<UserContract> userContracts)
        {
            var workSessions = new List<WorkSession>();
            var faker = new Faker();

            foreach (var userContract in userContracts)
            {
                for (int i = 0; i < 10; i++)
                {
                    var start = faker.Date.Past(1).ToUniversalTime().Date;
                    var hour = faker.Random.Int(6, 14);
                    var minute = faker.PickRandom(0, 15, 30, 45);
                    var startTime = new DateTime(start.Year, start.Month, start.Day, hour, minute, 0, DateTimeKind.Utc);

                    var endTime = startTime.AddHours(faker.Random.Int(2, 8));
                    var endMinute = faker.PickRandom(0, 15, 30, 45);
                    endTime = new DateTime(endTime.Year, endTime.Month, endTime.Day, endTime.Hour, endMinute, 0, DateTimeKind.Utc);
                    if (endTime <= startTime)
                    {
                        endTime = startTime.AddHours(1);
                        endTime = new DateTime(endTime.Year, endTime.Month, endTime.Day, endTime.Hour, 0, 0, DateTimeKind.Utc);
                    }

                    var workSession = new WorkSession
                    {
                        Id = Guid.NewGuid(),
                        TaskDescription = faker.Lorem.Sentence(),
                        StartTime = startTime,
                        EndTime = endTime,
                        Factor = faker.PickRandom(1.0f, 1.5f, 2.0f),
                        WBSO = faker.Random.Bool(),
                        Locked = false,
                        OtherRemarks = faker.Lorem.Sentence(),
                        CreatedAt = faker.Date.Past(1).ToUniversalTime(),
                        UpdatedAt = faker.Date.Recent(1).ToUniversalTime()
                    };

                    workSession.AssignToUserContract(userContract);
                    var maxTVT = ((float)Math.Floor(workSession.RawEffectiveHours * 4) / 4f);

                    var availableTVTSteps = new List<float>();

                    if (maxTVT > 0)
                    {
                        // Generate TVT steps in increments of 0.25 up to maxTVT
                        for (float step = 0.25f; step <= maxTVT; step += 0.25f)
                        {
                            availableTVTSteps.Add(step);
                        }
                    }

                    if (availableTVTSteps.Any() && faker.Random.Bool())
                    {
                        workSession.TVTAccruedHours = faker.PickRandom(availableTVTSteps);
                        workSession.TVTUsedHours = 0;
                    }
                    else if (availableTVTSteps.Any())
                    {
                        workSession.TVTUsedHours = faker.PickRandom(availableTVTSteps);
                        workSession.TVTAccruedHours = 0;
                    }
                    else
                    {
                        workSession.TVTAccruedHours = 0;
                        workSession.TVTUsedHours = 0;
                    }

                    // Instead of a while loop, set BreakTime once using a filtered PickRandom
                    var possibleBreakTimes = new List<float> { 0f, 0.25f, 0.5f, 0.75f, 1f }
                        .Where(b => b !>= workSession.RawEffectiveHours)
                        .ToArray();
                    workSession.BreakTime = possibleBreakTimes.Length > 0
                        ? faker.PickRandom(possibleBreakTimes)
                        : 0f;

                    workSession.Validate();

                    workSessions.Add(workSession);
                }
            }

            return workSessions;
        }
    }
}
