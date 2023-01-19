using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customers.Infrastructure;

public class CustomersContextFactory : IDesignTimeDbContextFactory<CustomersContext>
{
    public CustomersContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CustomersContext>();
        optionsBuilder.UseSqlite("Filename=customers.db");

        return new CustomersContext(optionsBuilder.Options);
    }
}
