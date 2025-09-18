using Application.AutoMapper;
using Application.Persistance;
using Application.Queries.Users;
using Application.Services;
using Hangfire;
using Hangfire.MemoryStorage;
using Infrastructure.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<DbContextImitation>();

builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(GetUserQuery).Assembly);
});

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<MappingProfile>();
}, typeof(MappingProfile).Assembly);

// Add Hangfire with in-memory storage
builder.Services.AddHangfire(config =>
    config.UseMemoryStorage());

// Add Hangfire server
builder.Services.AddHangfireServer();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// For job monitoring
app.UseHangfireDashboard("/hangfire");

app.UseHttpsRedirection();

app.MapControllers();

//Creates a recurring job which runs every 2 minutes
RecurringJob.AddOrUpdate<ITaskService>("Reassign and reevaluate tasks", (taskService) => taskService.ReAssingAndReAvaluate(), "*/10 * * * *");

app.Run();
