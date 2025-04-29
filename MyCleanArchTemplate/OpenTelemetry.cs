using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Sinks.OpenTelemetry;

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
        builder.Services.AddOpenTelemetry()
            .ConfigureResource(resource =>
            {
                resource.AddService("MyCleanArchTemplate.Api");
                resource.AddTelemetrySdk();
                resource.AddAttributes(new Dictionary<string, object>()
                {
                    ["deployment.environment"] = builder.Environment.EnvironmentName
                });
            })
            .WithMetrics(metrics =>
            {
                metrics.AddRuntimeInstrumentation()
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddSqlClientInstrumentation();

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
                    .AddSqlClientInstrumentation();

                tracing.AddOtlpExporter();
            });


        return builder;
    }

}