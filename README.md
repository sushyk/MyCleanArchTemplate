# MyCleanArchTemplate
My clean architecture template for .NET projects

- [ ] <b>Docker Compose:</b> Add docker-compose.yaml file and include aspire dashboard in it.
- [ ] <b>Empty Projects:</b>
      Create empty projects for Domain, Application, Infrastructure and Presentation layers
- [ ] <b>OpenTelemetry:</b> Instrument app to export metrics and traces in OpenTelemetry format
- [ ] <b>Serilog:</b> Add Serilog logging with enrichers. Make Serilog read from configuration and configure OpenTelemetry sink
- [ ] Mediator
- [ ] FluentValidation. Configure ValidationPipelineBehavior


# Quick Start:
                     
1. In your terminal, cd to MyCleanArchTemplate/MyCleanArchTemplate.Web and run docker compose using a profile to start all serivces.
Two profiles are available depending on what observability stack you would like to run.
    - If you want to view telemetry in Aspire dashboard, run docker compose using the aspire profile: `docker compose --profile aspire up -d` 
    - To shut down containers: `docker compose --profile aspire down`
    - If you want to view telemetry in Grafana, run docker compose using the lgtm profile: `docker compose --profile lgtm up -d`
    - To shut down containers: `docker compose --profile lgtm down`
1. Open Package Manager Console in Visual Studio and run the command `Update-Database` to run all migrations.

# Notes

1. Aspire Dashboard is available at localhost:18888
1. Grafana is available at localhost: 3000
1. Redis Insight is available at localhost:8001