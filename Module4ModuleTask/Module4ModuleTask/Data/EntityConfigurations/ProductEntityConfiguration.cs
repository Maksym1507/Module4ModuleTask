using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module4ModuleTask.Data.Entities;

namespace Module4ModuleTask.Data.EntityConfigurations
{
    public class ProductEntityConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.HasKey(h => h.Id);
            builder.Property(p => p.SupplierId).IsRequired();
            builder.Property(p => p.CategoryId).IsRequired();
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.UnitPrice).HasColumnType("money").IsRequired();
            builder.Property(p => p.Color).IsRequired();

            builder.HasOne(h => h.Supplier)
                .WithMany(h => h.Products)
                .HasForeignKey(h => h.SupplierId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(h => h.Category)
                .WithMany(h => h.Products)
                .HasForeignKey(h => h.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
