using Microsoft.AspNetCore.Routing;

namespace MyCleanArchTemplate.Adapter.WebApi;

public static class EndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapAllEndpoints(this IEndpointRouteBuilder builder)
    {
        builder.MapWeatherEndpoints();
        builder.MapCustomerEndpoints();

        return builder;
    }
}