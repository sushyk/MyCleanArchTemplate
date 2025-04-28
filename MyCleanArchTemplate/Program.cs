using MyCleanArchTemplate.Application;
using MyCleanArchTemplate.Adapter.WebApi;
using MyCleanArchTemplate.Adapter.Persistence;
using MyCleanArchTemplate.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication()
    .AddPresentation()
    .AddPersistence();

builder.ConfigureOpenTelemetry();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapWeatherEndpoints();

app.Run();

