using Microsoft.EntityFrameworkCore;
using MusicPracticeTracker.Api.Configuration;
using MusicPracticeTracker.Api.Data;
using MusicPracticeTracker.Api.Repositories;
using MusicPracticeTracker.Api.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = DatabaseConfiguration.GetConnectionString(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:4200",
                "https://localhost:4200",
                "http://127.0.0.1:4200",
                "https://127.0.0.1:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySQL(connectionString));

builder.Services.AddScoped<IInstrumentRepository, InstrumentRepository>();
builder.Services.AddScoped<IPracticeSessionRepository, PracticeSessionRepository>();
builder.Services.AddScoped<IGoalRepository, GoalRepository>();

builder.Services.AddScoped<IInstrumentService, InstrumentService>();
builder.Services.AddScoped<IPracticeSessionService, PracticeSessionService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IGoalService, GoalService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("Frontend");
app.MapControllers();
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();
