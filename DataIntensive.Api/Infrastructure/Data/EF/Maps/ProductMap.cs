using DataIntensive.Api.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataIntensive.Api.Infrastructure.Data.EF.Maps
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.Property(x => x.Description)
                .HasMaxLength(100);

            builder.Property(x => x.Price)
                .HasPrecision(18, 2);
        }
    }
}