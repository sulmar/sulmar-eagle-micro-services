using Core.Domain;
using Customers.Domain.Models;

namespace Customers.Domain;

public interface ICustomerRepository : IEntityRepository<Customer>
{
    Task<Customer> GetByNameAsync(string name);
}
