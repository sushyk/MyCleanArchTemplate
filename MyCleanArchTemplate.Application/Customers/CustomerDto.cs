using MyCleanArchTemplate.Application.Abstractions.Persistence;

namespace MyCleanArchTemplate.Application.Customers;

public sealed record CustomerDto(long CustomerId, string Name, string Email) : IDto;
