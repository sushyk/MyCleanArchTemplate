using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MyCleanArchTemplate.Adapter.WebApi.Requests;
using MyCleanArchTemplate.Application.Customers.CreateCustomer;
using MyCleanArchTemplate.Application.Customers.GetCustomer;
using MyCleanArchTemplate.Core.Shared;
using MyCleanArchTemplate.Domain.Customers;

namespace MyCleanArchTemplate.Adapter.WebApi.Endpoints;

internal static class CustomerEndpoints
{
    internal static void MapCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/customers/{customerId:long}", async (long customerId, ISender sender, CancellationToken token) =>
        {
            GetCustomerQuery query = new(customerId);
            Result<Customer> customerResult = await sender.Send(query, token);

            if (customerResult.IsFailure)
            {
                return Results.NotFound(customerResult.Error);
            }

            return Results.Ok(customerResult.Value);
        })
        .WithName("GetCustomer")
        .WithTags("Customers");

        app.MapPost("customers", async (ISender sender, CreateCustomerRequest request, CancellationToken token) =>
        {
            CreateCustomerCommand command = new(request.Name, request.Email);
            Result<Customer> customerResult = await sender.Send(command, token);

            if (customerResult.IsFailure)
            {
                return Results.InternalServerError(customerResult.Error);
            }

            return Results.Ok(customerResult.Value);
        })
        .WithName("CreateCustomer")
        .WithTags("Customers");
    }
}
