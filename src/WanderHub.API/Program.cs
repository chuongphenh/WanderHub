using Carter;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Serilog;
using WanderHub.API.DependencyInjection.Extensions;
using WanderHub.API.Middleware;
using WanderHub.Application.DependencyInjection.Extensions;
using WanderHub.Persistence.DependencyInjection.Extensions;
using WanderHub.Persistence.DependencyInjection.Options;

var builder = WebApplication.CreateBuilder(args);

// Add configuration
Log.Logger = new LoggerConfiguration().ReadFrom
    .Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging
    .ClearProviders()
    .AddSerilog();

builder.Host.UseSerilog();


// Add Carter module
builder.Services.AddCarter();

//// => Use in case for ControllerAPI define in DistributedSystem.Presentation
//builder
//    .Services
//    .AddControllers()
//    .AddApplicationPart(DistributedSystem.Presentation.AssemblyReference.Assembly);

// Add Swagger
builder.Services
        .AddSwaggerGenNewtonsoftSupport()
        .AddFluentValidationRulesToSwagger()
        .AddEndpointsApiExplorer()
        .AddSwaggerAPI();

builder.Services
    .AddApiVersioning(options => options.ReportApiVersions = true)
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });
builder.Services.AddJwtAuthenticationAPI(builder.Configuration);

builder.Services.AddMediatRApplication();
builder.Services.AddAutoMapperApplication();

// Configure Options and SQL => Remember mapcarter
builder.Services.AddInterceptorPersistence();
builder.Services.ConfigureSqlServerRetryOptionsPersistence(builder.Configuration.GetSection(nameof(SqlServerRetryOptions)));
builder.Services.AddSqlServerPersistence();
builder.Services.AddRepositoryPersistence();

// Add Middleware => Remember using middleware
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

var app = builder.Build();

// Using middleware
app.UseMiddleware<ExceptionHandlingMiddleware>();



/// Add API Endpoint with carter module
app.MapCarter();

// Configure the HTTP request pipeline. 
if (builder.Environment.IsDevelopment() || builder.Environment.IsStaging())
    app.UseSwaggerAPI(); // => After MapCarter => Show Version

try
{
await app.RunAsync();
Log.Information("Stopped cleanly");
}
catch (Exception ex)
{
Log.Fatal(ex, "An unhandled exception occured during bootstrapping");
await app.StopAsync();
}
finally
{
Log.CloseAndFlush();
await app.DisposeAsync();
}

public partial class Program { }
