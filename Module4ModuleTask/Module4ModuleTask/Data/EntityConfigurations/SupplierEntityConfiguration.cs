using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module4ModuleTask.Data.Entities;

namespace Module4ModuleTask.Data.EntityConfigurations
{
    public class SupplierEntityConfiguration : IEntityTypeConfiguration<SupplierEntity>
    {
        public void Configure(EntityTypeBuilder<SupplierEntity> builder)
        {
            builder.HasKey(h => h.Id);
            builder.Property(p => p.CompanyName).IsRequired();
            builder.Property(p => p.Address).IsRequired();
            builder.Property(p => p.City).IsRequired();
            builder.Property(p => p.Country).IsRequired();
        }
    }
}
