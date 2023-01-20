using Customers.Domain.Models;
using Customers.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Customers.Infrastructure;

public class CustomersContext : DbContext
{
    public CustomersContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfiguration(new CustomerConfiguration())
            .ApplyConfiguration(new AddressConfiguration());

        //modelBuilder
        //    .Entity<ApplicationUser>().ToTable("ApplicationUsers", t => t.ExcludeFromMigrations());

        base.OnModelCreating(modelBuilder);
    }


}
