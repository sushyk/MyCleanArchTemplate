using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace MyCleanArchTemplate.Adapter.WebApi;

internal record WeatherForecast(string city, DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


internal static class WeatherEndpoints
{
    static string[] summaries = [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    internal static void MapWeatherEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/weatherforecast", (string city, int days, ILoggerFactory loggerFactory) =>
        {
            var logger = loggerFactory.CreateLogger("WeatherEndpoints");
           
            var forecasts = Enumerable.Range(1, days).Select(index =>
                new WeatherForecast
                (
                    city,
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
                .ToArray();

            logger.LogInformation("Retrieved {WeatherCount} weather forecasts for {City}", forecasts.Length, city);

            return Results.Ok(forecasts);
        })
        .WithName("GetWeatherForecast");
    }
}
