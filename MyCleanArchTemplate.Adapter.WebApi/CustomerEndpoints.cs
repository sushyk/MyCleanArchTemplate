using Mediator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MyCleanArchTemplate.Application.Customers.GetCustomer;
using MyCleanArchTemplate.Core.Shared;
using MyCleanArchTemplate.Domain.Customers;

namespace MyCleanArchTemplate.Adapter.WebApi;

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
                return Results.NotFound(customerResult.Error.Description);
            }

            return Results.Ok(customerResult.Value);
        })
        .WithName("GetCustomer");
    }
}
