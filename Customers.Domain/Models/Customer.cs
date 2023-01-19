using Core.Domain;

namespace Customers.Domain.Models;

public class Customer : BaseEntity
{
    public string Name { get; set; }
    public Address WorkAddress { get; set; }
}
