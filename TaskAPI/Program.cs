using ChallengeTask.Infrastructure.Data.Repositories;
using Infrastructure.Data.Repositories;
using TaskAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IRepository<Organization>, ApiOrganizationRepository<Organization>>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
