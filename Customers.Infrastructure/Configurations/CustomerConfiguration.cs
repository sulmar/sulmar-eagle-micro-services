using Customers.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers.Infrastructure.Configurations;

internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder
            .Property(p => p.Id).HasColumnName("CustomerId");

        builder
            .Property(p => p.Name).IsRequired().HasMaxLength(50);

        builder
            .Navigation(p => p.WorkAddress).AutoInclude();

        
    }
}
