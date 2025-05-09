using MyCleanArchTemplate.Application;
using MyCleanArchTemplate.Adapter.WebApi;
using MyCleanArchTemplate.Adapter.Persistence;
using MyCleanArchTemplate.Infrastructure;
using MyCleanArchTemplate.Web;
using HealthChecks.UI.Client;
using Serilog;
using MyCleanArchTemplate.Web.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureOpenTelemetry();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication()
    .AddPresentation()
    .AddPersistence(builder.Configuration)
    .AddInfrastructure();
;
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

