using MyCleanArchTemplate.Application;
using MyCleanArchTemplate.Adapter.WebApi;
using MyCleanArchTemplate.Adapter.Persistence;
using MyCleanArchTemplate.Infrastructure;
using MyCleanArchTemplate.Web;
using HealthChecks.UI.Client;
using Serilog;
using StackExchange.Redis;
using MyCleanArchTemplate.Adapter.Kafka;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication()
    .AddPresentation()
    .AddInfrastructure()
    .AddPersistence(builder.Configuration)
    .AddKafka(builder.Configuration);

IConnectionMultiplexer redisConnectionMultiplexer = await ConnectionMultiplexer.ConnectAsync(builder.Configuration.GetConnectionString("Redis"));
builder.Services.AddSingleton(redisConnectionMultiplexer);
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.ConnectionMultiplexerFactory = () => Task.FromResult(redisConnectionMultiplexer);
});

builder.ConfigureOpenTelemetry();

builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapHealthChecks("health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseSerilogRequestLogging();

app.MapAllEndpoints();

app.Run();

