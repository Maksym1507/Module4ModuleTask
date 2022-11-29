using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module4ModuleTask.Data.Entities;

namespace Module4ModuleTask.Data.EntityConfigurations
{
    public class OrderDetailsEntityConfiguration : IEntityTypeConfiguration<OrderDetailsEntity>
    {
        public void Configure(EntityTypeBuilder<OrderDetailsEntity> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.OrderId).IsRequired();
            builder.Property(p => p.ProductId).IsRequired();
            builder.Property(p => p.Quantity).IsRequired();

            builder.HasOne(h => h.Order)
                .WithMany(w => w.OrderDetails)
                .HasForeignKey(h => h.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(h => h.Product)
                .WithMany(w => w.OrderDetails)
                .HasForeignKey(h => h.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
