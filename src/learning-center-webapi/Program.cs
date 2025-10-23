using learning_center_webapi.Contexts.Shared.Infraestructure.Persistence.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var connectionString = builder.Configuration.GetConnectionString("learningCenter")
    ?? throw new InvalidOperationException("No se encontró la cadena de conexión 'learningCenter'.");

builder.Services.AddDbContext<LearningCenterContext>(options =>
{
    options.UseMySQL(connectionString);

    if (builder.Environment.IsDevelopment())
    {
        options.LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    }
    else if (builder.Environment.IsProduction())
    {
        options.LogTo(Console.WriteLine, LogLevel.Error)
            .EnableDetailedErrors();
    }
});

var app = builder.Build();

// Ensure DB is created
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<LearningCenterContext>();
    context.Database.EnsureCreated();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();