using Core.Domain;

namespace Customers.Domain.Models;

public class Address : BaseEntity
{
    public string City { get; set; }
    public string Country { get; set; }
}