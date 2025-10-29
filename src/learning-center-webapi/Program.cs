using learning_center_webapi.Contexts.Enrolments.Application.CommandServices;
using learning_center_webapi.Contexts.Enrolments.Application.QueryServices;
using learning_center_webapi.Contexts.Enrolments.Domain.Infraestructure;
using learning_center_webapi.Contexts.Enrolments.Infraestructure;
using learning_center_webapi.Contexts.Shared.Domain.Repositories;
using learning_center_webapi.Contexts.Shared.Infraestructure.Persistence.Configuration;
using learning_center_webapi.Contexts.Shared.Infraestructure.Repositories;
using learning_center_webapi.Contexts.Tutorials.Application.CommandServices;
using learning_center_webapi.Contexts.Tutorials.Application.QueryServices;
using learning_center_webapi.Contexts.Tutorials.Domain.Commands;
using learning_center_webapi.Contexts.Tutorials.Domain.Infraestructure;
using learning_center_webapi.Contexts.Tutorials.Domain.Queries;
using learning_center_webapi.Contexts.Tutorials.Infraestructure;
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
        options.LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    else if (builder.Environment.IsProduction())
        options.LogTo(Console.WriteLine, LogLevel.Error)
            .EnableDetailedErrors();
});


//Dependency injection Tutorials
builder.Services.AddTransient<ITutorialRepository, TutorialRepository>();
builder.Services.AddTransient<ITutorialQueryService, TutorialQueryService>();
builder.Services.AddTransient<ITutorialCommandService, TutorialCommandService>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

//Dependency injection Enrolments
builder.Services.AddTransient<IEnrolmentRepository, EnrolmentRepository>();
builder.Services.AddTransient<IEnrolmentQueryService, EnrolmentQueryService>();
builder.Services.AddTransient<IEnrolmentCommandService, EnrolmentCommandService>();


var app = builder.Build();

// Ensure DB is created
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<LearningCenterContext>();
    context.Database.EnsureCreated();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.MapOpenApi();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();