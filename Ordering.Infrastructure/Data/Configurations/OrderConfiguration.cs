using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;
using Ordering.Domain.Modles;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Id).HasConversion(orderId => orderId.Value, dbId => OrderId.Of(dbId));
            builder.HasMany<OrderItem>().WithOne()
                 .HasForeignKey(oi => oi.OrderId)
                 .IsRequired();
            builder.HasOne<Customer>().WithMany()
                .HasForeignKey(o => o.CustomerId)
                .IsRequired();

            builder.ComplexProperty(o => o.OrderName, nameBuilder =>
            {
                nameBuilder.Property(on => on.Value)
                .HasColumnName(nameof(Order.OrderName))
                .HasMaxLength(100)
                .IsRequired();
            });

            builder.ComplexProperty(o => o.ShipingAddres, addressBuilder =>
            {
                addressBuilder.Property(ad => ad.FirstName)
                .HasMaxLength(50)
                .IsRequired();

                addressBuilder.Property(ad => ad.LastName)
                .HasMaxLength(50)
                .IsRequired();

                addressBuilder.Property(ad => ad.EmailAddres)
                .HasMaxLength(50);

                addressBuilder.Property(ad => ad.AddressLine)
                .HasMaxLength(180)
                .IsRequired();

                addressBuilder.Property(ad => ad.Country)
                .HasMaxLength(50);

                addressBuilder.Property(ad => ad.State)
                .HasMaxLength(50);

                addressBuilder.Property(ad => ad.ZipCode)
                .HasMaxLength(5);
            });

            builder.ComplexProperty(o => o.BillingAddres, addressBuilder =>
            {
                addressBuilder.Property(ad => ad.FirstName)
                .HasMaxLength(50)
                .IsRequired();

                addressBuilder.Property(ad => ad.LastName)
                .HasMaxLength(50)
                .IsRequired();

                addressBuilder.Property(ad => ad.EmailAddres)
                .HasMaxLength(50);

                addressBuilder.Property(ad => ad.AddressLine)
                .HasMaxLength(180)
                .IsRequired();

                addressBuilder.Property(ad => ad.Country)
                .HasMaxLength(50);

                addressBuilder.Property(ad => ad.State)
                .HasMaxLength(50);

                addressBuilder.Property(ad => ad.ZipCode)
                .HasMaxLength(5);
            });

            builder.ComplexProperty(o => o.Payment, paymentBuilder =>
            {
                paymentBuilder.Property(p => p.CardName)
                .HasMaxLength(50)
                .IsRequired();

                paymentBuilder.Property(p => p.CardNumber)
                .HasMaxLength(24)
                .IsRequired();

                paymentBuilder.Property(p => p.Expiration)
                .HasMaxLength(10);

                paymentBuilder.Property(p => p.CVV)
                .HasMaxLength(3)
                .IsRequired();

                paymentBuilder.Property(p => p.PaymentMethod);

            });

            builder.Property(o => o.Status)
                .HasDefaultValue(OrderStatus.Draft)
                .HasConversion(s => s.ToString(),
                    dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));

            builder.Property(o => o.TotlaPrice);

        }
    }
}
