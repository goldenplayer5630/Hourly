using Bogus;
using Hourly.Shared.Entities;
using Hourly.Shared.Enums;

namespace Hourly.IntergrationTests.Utilities.Factories
{
    public class UserContractFactory
    {
        public static List<UserContract> CreateUserContracts(List<Guid> userIds)
        {
            var contractTypes = Enum.GetValues(typeof(ContractTypes)).Cast<ContractTypes>().Where(ct => ct != ContractTypes.Undefined).ToArray();
            var contracts = new List<UserContract>();
            var faker = new Faker();

            foreach (var userId in userIds)
            {
                int contractCount = faker.Random.Int(1, 3);
                for (int i = 0; i < contractCount; i++)
                {
                    var startDate = faker.Date.Past(2);
                    var endDate = faker.Date.Future();

                    contracts.Add(new UserContract
                    {
                        Id = Guid.NewGuid(),
                        UserId = userId,
                        Name = faker.Name.JobTitle(),
                        ContractType = faker.PickRandom(contractTypes),
                        IsActive = i == 0,
                        MinMonthlyHours = faker.Random.Int(40, 80),
                        MaxMonthlyHours = faker.Random.Int(81, 160),
                        GrossHourlyRate = faker.Random.Float(15, 100),
                        HolidayHoursPercentage = faker.Random.Bool() ? faker.Random.Int(0, 30) : null,
                        MonthlyPaidHolidayHours = faker.Random.Bool(),
                        StartDate = startDate,
                        EndDate = endDate,
                        ContractFilePath = faker.System.FilePath(),
                        Description = faker.Lorem.Sentence(),
                        CreatedAt = faker.Date.Past(3, startDate),
                        UpdatedAt = faker.Random.Bool(0.5f) ? faker.Date.Recent(30, DateTime.Now) : null
                    });
                }
            }

            return contracts;
        }
    }
}
