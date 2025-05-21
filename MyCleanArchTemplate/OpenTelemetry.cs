using Npgsql;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Sinks.OpenTelemetry;
using StackExchange.Redis;
using System.Reflection.PortableExecutable;

namespace MyCleanArchTemplate.Web;

public static class OpenTelemetry
{
    public static WebApplicationBuilder ConfigureOpenTelemetry(this WebApplicationBuilder builder)
    {
        return builder.ConfigureOpenTelemetryMetricsAndTracing().ConfigureSerilogLoggingForOpenTelemetry();
    }

    private static WebApplicationBuilder ConfigureSerilogLoggingForOpenTelemetry(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Host.UseSerilog((context, config) =>
        {
            config.ReadFrom.Configuration(context.Configuration);
        });

        return builder;
    }


    private static WebApplicationBuilder ConfigureOpenTelemetryMetricsAndTracing(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddOpenTelemetry()
            .ConfigureResource(resource =>
            {
                resource.AddService(builder.Environment.ApplicationName);
                resource.AddTelemetrySdk();
                resource.AddAttributes(new Dictionary<string, object>()
                {
                    ["deployment.environment.name"] = builder.Environment.EnvironmentName,
                    ["host.name"] = Environment.MachineName
                });
            })
            .WithMetrics(metrics =>
            {
                metrics.AddRuntimeInstrumentation()
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddSqlClientInstrumentation()
                    .AddNpgsqlInstrumentation();

                metrics.AddOtlpExporter();
            })
            .WithTracing(tracing =>
            {
                if (builder.Environment.IsDevelopment())
                {
                    tracing.SetSampler<AlwaysOnSampler>();
                }

                tracing.AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddEntityFrameworkCoreInstrumentation()
                    .AddSqlClientInstrumentation()
                    .AddNpgsql()
                    .AddRedisInstrumentation();

                tracing.AddOtlpExporter();
            });


        return builder;
    }

}