using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module4ModuleTask.Data.Entities;

namespace Module4ModuleTask.Data.EntityConfigurations
{
    public class OrderEntityConfiguration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.HasKey(h => h.Id);
            builder.Property(p => p.PaymentId).IsRequired();
            builder.Property(p => p.CustomerId).IsRequired();
            builder.Property(p => p.ShipperId).IsRequired();

            builder.Property(p => p.OrderDate).HasColumnType("date");
            builder.Property(p => p.ShipDate).HasColumnType("date");
            builder.Property(p => p.RequiredDate).HasColumnType("date");
            builder.Property(p => p.PaymentDate).HasColumnType("date");

            builder.HasOne(h => h.Payment)
                .WithMany(w => w.Orders)
                .HasForeignKey(h => h.PaymentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(h => h.Customer)
                .WithMany(w => w.Orders)
                .HasForeignKey(h => h.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(h => h.Shipper)
                .WithMany(w => w.Orders)
                .HasForeignKey(h => h.ShipperId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
