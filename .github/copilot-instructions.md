# Project Guidelines

## Code Style

Follow standard C# conventions. Use records for immutable data, sealed classes for handlers.

## Architecture

Clean Architecture with layers: Domain (business logic), Application (CQRS handlers), Adapters (technical implementations), Infrastructure (cross-cutting).

Dependency flow: Domain → Application → Adapters/Infrastructure.

Use Result<T> for error handling, Mediator for CQRS.

## Build and Test

See [README.md](README.md) for Docker Compose commands and database setup.

Run `Update-Database` for EF migrations.

## Conventions

- Commands: sealed records implementing ICommand<Result<T>>
- Handlers: sealed classes implementing ICommandHandler<TCommand, TResult>
- Validation: FluentValidation with pipeline behavior
- Naming: Domain-prefixed error codes (e.g., "Customers.NotFound")
- Folder structure: Group by feature (Customers/CreateCustomer/)

Avoid hardcoded connection strings; use DI configuration.
