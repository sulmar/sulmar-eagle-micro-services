using Customers.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Customers.Infrastructure.Configurations;

internal class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder
            .Property(p => p.Country).IsRequired().HasMaxLength(50);

        builder
            .Property(p => p.City).IsRequired().HasMaxLength(50);
    }
}
