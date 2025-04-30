using MyCleanArchTemplate.Domain.Customers;

namespace MyCleanArchTemplate.Application.Customers.GetCustomer;

public class GetCustomerQueryHandler : Abstractions.Messaging.IQueryHandler<GetCustomerQuery, Customer>
{

}
