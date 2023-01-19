using Core.Infrastructure;
using Customers.Domain;
using Customers.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Customers.Infrastructure;

public class DbCustomerRepository : DbEntityRepository<Customer, CustomersContext>, ICustomerRepository
{
    public DbCustomerRepository(CustomersContext context) : base(context)
    {
    }

    //public override async Task<IEnumerable<Customer>> GetAllAsync()
    //{
    //    return await entities.Include(p=>p.WorkAddress).ToListAsync();
    //}

    //public override async Task<IEnumerable<Customer>> GetAllAsync()
    //{
    //    return await entities.IgnoreAutoIncludes().ToListAsync();
    //}

    public async Task<Customer> GetByNameAsync(string name)
    {
        return await entities.SingleOrDefaultAsync(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public override async Task UpdateAsync(Customer entity)
    {
        context.Entry(entity).Property(p => p.Name).IsModified = true;        

        await context.SaveChangesAsync(); // UPDATE set Name = 'abc' WHERE Id = 1
    }


}