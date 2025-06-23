using Bogus;
using Hourly.Domain.Entities;
using Hourly.Domain.Enums;

namespace Hourly.IntergrationTests.Utilities.Factories
{
    public class UserContractFactory
    {
        public static List<UserContract> CreateUserContracts(List<User> users)
        {
            var contractTypes = Enum.GetValues(typeof(ContractType)).Cast<ContractType>().Where(ct => ct != ContractType.Undefined).ToArray();
            var contracts = new List<UserContract>();
            var faker = new Faker();

            foreach (var user in users)
            {
                int contractCount = faker.Random.Int(1, 3);
                for (int i = 0; i < contractCount; i++)
                {
                    var startDate = faker.Date.Past(2);
                    var endDate = faker.Date.Future();

                    // Ensure MinWeeklyHours < MaxWeeklyHours
                    int minWeeklyHours = faker.Random.Int(8, 32);
                    int maxWeeklyHours = faker.Random.Int((minWeeklyHours + 1), (minWeeklyHours + 8));

                    var contract = new UserContract
                    {
                        Id = Guid.NewGuid(),
                        Name = faker.Name.JobTitle(),
                        ContractType = faker.PickRandom(contractTypes),
                        IsActive = i == 0,
                        MinWeeklyHours = minWeeklyHours,
                        MaxWeeklyHours = maxWeeklyHours,
                        GrossHourlyRate = faker.Random.Float(15, 100),
                        HolidayHoursPercentage = faker.Random.Bool() ? faker.Random.Int(0, 30) : null,
                        MonthlyPaidHolidayHours = faker.Random.Bool(),
                        StartDate = startDate,
                        EndDate = endDate,
                        TVTHourBalance = faker.Random.Float(0, 20),
                        ContractFilePath = faker.System.FilePath(),
                        Description = faker.Lorem.Sentence(),
                        CreatedAt = faker.Date.Past(3, startDate),
                        UpdatedAt = faker.Random.Bool(0.5f) ? faker.Date.Recent(30, DateTime.Now) : null
                    };
                    contracts.Add(contract);
                    contract.AssignToUser(user);
                }
            }

            return contracts;
        }
    }
}
