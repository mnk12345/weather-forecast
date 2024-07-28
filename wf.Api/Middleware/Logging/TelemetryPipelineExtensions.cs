using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace wf.Api.Middleware.Logging;

public static class TelemetryPipelineExtensions
{
    public static IHostApplicationBuilder ConfigureTelemetry(this IHostApplicationBuilder builder)
    {
        builder.Logging.AddOpenTelemetry(x =>
        {
            x.IncludeFormattedMessage = true;
            x.IncludeScopes = true;
            x.AddOtlpExporter();

            if (builder.Environment.IsDevelopment())
            {
                x.AddConsoleExporter();
            }
        });

        builder.Services.AddOpenTelemetry()
            .WithLogging()
            .WithMetrics(x =>
            {
                x.AddAspNetCoreInstrumentation();
                x.AddRuntimeInstrumentation();
                x.AddHttpClientInstrumentation();

                x.AddMeter(
                    "Microsoft.AspNetCore.Hosting",
                    "Microsoft.AspNetCore.Server.Kestrel",
                    "System.Net.Http"
                );
            })
            .WithTracing(x =>
            {
                if (builder.Environment.IsDevelopment())
                {
                    x.SetSampler<AlwaysOnSampler>();
                }

                x.AddAspNetCoreInstrumentation();
                x.AddHttpClientInstrumentation();
            });

        builder.Services.Configure<OpenTelemetryLoggerOptions>(x => x.AddOtlpExporter());
        builder.Services.ConfigureOpenTelemetryMeterProvider(x => x.AddOtlpExporter());
        builder.Services.ConfigureOpenTelemetryTracerProvider(x => x.AddOtlpExporter());

        return builder;
    }
}
