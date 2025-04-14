using Hourly.IntergrationTests.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql("Host=localhost;Port=5432;Database=mydatabase;Username=admin;Password=admin123")); // or read from config

        services.AddLogging(); // Optional, if your seeder logs
    })
    .Build();

using var scope = host.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

try
{
    await TestDatabaseSeeder.SeedAsync(context);
    Console.WriteLine("Database seeded.");
}
catch (Exception ex)
{
    Console.WriteLine($"Failed to seed database: {ex.Message}");
    throw;
}
