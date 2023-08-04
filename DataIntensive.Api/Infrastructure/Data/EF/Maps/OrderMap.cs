using DataIntensive.Api.Domain.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataIntensive.Api.Infrastructure.Data.EF.Maps
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CardNumber)
                .HasMaxLength(100);

            builder.Property(x => x.CardCvv)
                .HasMaxLength(3);

            builder.Property(x => x.CardOwner)
                .HasMaxLength(250);

            builder.Property(x => x.CardOwnerDocument)
                .HasMaxLength(50);

            builder.Property(x => x.Total)
                .HasPrecision(18, 2);

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);
        }
    }
}