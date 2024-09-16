using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using NLog;
using TestTask.Application.Common.Interfaces;
using TestTask.Application.Common.Utilites;
using TestTask.Infrastructure;
using TestTask.Infrastructure.Middlewares;
using TestTask.Infrastructure.Options;

namespace TestTask.Api.Middlewares;

public static class RegisterDependentServices
{
    public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<ApiExceptionFilter>();
        });

        builder.Services.ConfigOptions(builder.Configuration);
        builder.Services.ConfigLogger();

        builder.Services.AddSwaggerGen(config =>
        {
            config.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Insert / get entity API",
                Version = "v1"
            });

            config.EnableAnnotations();

            var filePath = Path.Combine(System.AppContext.BaseDirectory, "TestTask.Api.xml");
            config.IncludeXmlComments(filePath);
        });

        return builder;
    }

    private static void ConfigOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<LoggingOption>(configuration.GetSection(LoggingOption.SectionName));
    }

    private static void ConfigLogger(this IServiceCollection services)
    {
        services.AddTransient<ICustomLogger>(sp =>
        {
            var loggingOptions = sp.GetRequiredService<IOptions<LoggingOption>>().Value;
            return new PfmsLogger(LogManager.GetLogger("Logger"), loggingOptions.LogLevel.Default);
        });
    }
}
