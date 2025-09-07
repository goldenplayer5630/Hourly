using Hourly.Abstractions.Repositories;
using Hourly.Abstractions.Services;
using Hourly.Data.Repositories;
using Hourly.Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Hourly.Api;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

var corsOptions = builder.Configuration
    .GetSection("CORS")
    .Get<CorsSettings>();

if (corsOptions == null)
    throw new InvalidOperationException("CORS settings not found in configuration.");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.Configure<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.TokenValidationParameters.ValidAudience =
        builder.Configuration["AzureAd:Audience"];
});

// 3.2 Authorization: require scope on protected endpoints
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
        policy.RequireClaim("scp", builder.Configuration["AzureAd:Scopes"]!));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORS", policy =>
    {
        policy.AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin();
    });
});

// Register repositories
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IGitCommitRepository, GitCommitRepository>();
builder.Services.AddScoped<IGitRepositoryRepository, GitRepositoryRepository>();
builder.Services.AddScoped<ILockedMonthRepository, LockedMonthRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IWorkSessionRepository, WorkSessionRepository>();
builder.Services.AddScoped<IUserContractRepository, UserContractRepository>();

// Register application services
builder.Services.AddScoped<ITVTHoursService, TVTHoursService>();

// Register entity services
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IGitCommitService, GitCommitService>();
builder.Services.AddScoped<IGitRepositoryService, GitRepositoryService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IWorkSessionService, WorkSessionService>();
builder.Services.AddScoped<IUserContractService, UserContractService>();
builder.Services.AddScoped<ISummaryService, SummaryService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


app.UseRouting();

app.UseCors("CORS");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
