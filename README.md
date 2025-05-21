# MyCleanArchTemplate
My clean architecture template for .NET projects

- [ ] <b>Docker Compose:</b> Add docker-compose.yaml file and include aspire dashboard in it.
- [ ] <b>Empty Projects:</b>
      Create empty projects for Domain, Application, Infrastructure and Presentation layers
- [ ] <b>OpenTelemetry:</b> Instrument app to export metrics and traces in OpenTelemetry format
- [ ] <b>Serilog:</b> Add Serilog logging with enrichers. Make Serilog read from configuration and configure OpenTelemetry sink
- [ ] Mediator
- [ ] FluentValidation. Configure ValidationPipelineBehavior


# Setup:

1. In your terminal, cd to MyCleanArchTemplate/MyCleanArchTemplate.Web and run `docker compose up -d`.
2. Open Package Manager Console in Visual Studio and run the command `Update-Database` to run all migrations.

# Notes

1. Aspire Dashboard is used to analyse telemetery from the app. Its available at localhost:18888
2. You can access Redis Insight at localhost:8001