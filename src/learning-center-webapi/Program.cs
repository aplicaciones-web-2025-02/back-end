using System.Reflection;
using learning_center_webapi.Contexts.Security.Application.QueryServices;
using learning_center_webapi.Contexts.Enrolments.Application.CommandServices;
using learning_center_webapi.Contexts.Enrolments.Application.QueryServices;
using learning_center_webapi.Contexts.Enrolments.Domain.Infraestructure;
using learning_center_webapi.Contexts.Enrolments.Infraestructure;
using learning_center_webapi.Contexts.Security.Application.CommandServices;
using learning_center_webapi.Contexts.Security.Domain.Infraestructure;
using learning_center_webapi.Contexts.Security.Infraestructure;
using learning_center_webapi.Contexts.Shared.Domain.Filters;
using learning_center_webapi.Contexts.Shared.Domain.Repositories;
using learning_center_webapi.Contexts.Shared.Domain.Services;
using learning_center_webapi.Contexts.Shared.Infraestructure.Persistence.Configuration;
using Microsoft.Extensions.Localization;
using learning_center_webapi.Contexts.Shared.Infraestructure.Repositories;
using learning_center_webapi.Contexts.Tutorials.Application.ACL;
using learning_center_webapi.Contexts.Tutorials.Application.CommandServices;
using learning_center_webapi.Contexts.Tutorials.Application.QueryServices;
using learning_center_webapi.Contexts.Tutorials.Domain.Commands;
using learning_center_webapi.Contexts.Tutorials.Domain.Infraestructure;
using learning_center_webapi.Contexts.Tutorials.Domain.Queries;
using learning_center_webapi.Contexts.Tutorials.Infraestructure;
using learning_center_webapi.Contexts.Tutorials.Interfaces.REST.ACL;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});


// Database configuration
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

// Configurar servicios de localización
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[] { "en", "es"};
    options.SetDefaultCulture(supportedCultures[0])
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);
});

// Dependency injection Shared
builder.Services.AddTransient<ITutorialFacade, TutorialFacade>();

// Dependency injection Tutorials
builder.Services.AddTransient<ITutorialRepository, TutorialRepository>();
builder.Services.AddTransient<ITutorialQueryService, TutorialQueryService>();
builder.Services.AddTransient<ITutorialCommandService, TutorialCommandService>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();

// Dependency injection Enrolments
builder.Services.AddTransient<IEnrolmentRepository, EnrolmentRepository>();
builder.Services.AddTransient<IEnrolmentQueryService, EnrolmentQueryService>();
builder.Services.AddTransient<IEnrolmentCommandService, EnrolmentCommandService>();

// Dependency injection Security
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserCommandService, UserCommandService>();
builder.Services.AddTransient<IUserQueryService, UserQueryService>();
builder.Services.AddTransient<IPasswordHashService, PasswordHashService>();
builder.Services.AddTransient<IJwtTokenService, JwtTokenService>();


builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>();
});

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "learning center API",
        Description = "An ASP.NET Core Web API for managing tutorials and enrollments",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
    
    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


var app = builder.Build();


//swagger
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}


// Inicializar el servicio de localización
using (var scope = app.Services.CreateScope())
{
    var localizerFactory = scope.ServiceProvider.GetRequiredService<IStringLocalizerFactory>();
    LocalizationService.Initialize(localizerFactory);
}

app.UseRequestLocalization();
app.UseCors("AllowAll");

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<LearningCenterContext>();
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment()) 
    app.MapOpenApi();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();