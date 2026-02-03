using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Services;
using TaskManager.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=tasks.db"));

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    db.Database.EnsureCreated(); // ensure DB exists

    if (!db.TaskItems.Any())
    {
        db.TaskItems.AddRange(
            new TaskItem
            {
                Title = "Finish assignment",
                Description = "Complete Task Management API",
                DueDate = DateTime.UtcNow.AddDays(2),
                Priority = TaskPriority.High,
                Status = TaskStatus.Pending
            },
            new TaskItem
            {
                Title = "Read documentation",
                Description = "Read EF Core docs",
                DueDate = DateTime.UtcNow.AddDays(1),
                Priority = TaskPriority.Medium,
                Status = TaskStatus.InProgress
            },
            new TaskItem
            {
                Title = "Test API",
                Description = "Test GET, POST, PUT, DELETE endpoints",
                DueDate = DateTime.UtcNow.AddDays(3),
                Priority = TaskPriority.Low,
                Status = TaskStatus.Pending
            }
        );

        db.SaveChanges();
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapControllers();
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

