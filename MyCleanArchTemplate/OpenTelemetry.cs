using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace MyCleanArchTemplate.Web;

public static class OpenTelemetry
{
    public static IHostApplicationBuilder ConfigureOpenTelemetry(this IHostApplicationBuilder builder)
    {
        builder.Logging.AddOpenTelemetry(x =>
        {
            x.IncludeScopes = true;
            x.IncludeFormattedMessage = true;
        });

        builder.Services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService("MyCleanArchTemplate.Api"))
            .WithLogging(logging =>
            {
                logging.AddOtlpExporter();
            })
            .WithMetrics(metrics =>
            {
                metrics.AddRuntimeInstrumentation()
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation();

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
                    .AddEntityFrameworkCoreInstrumentation();

                tracing.AddOtlpExporter();
            });


        return builder;
    }

}