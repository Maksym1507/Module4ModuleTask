using Module4ModuleTask.Models;

namespace Module4ModuleTask.Data.Entities
{
    public class ProductEntity
    {
        public int Id { get; set; }

        public int SKU { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public SupplierEntity? Supplier { get; set; }

        public int SupplierId { get; set; }

        public CategoryEntity? Category { get; set; }

        public int CategoryId { get; set; }

        public int QuantityPerUnit { get; set; }

        public decimal UnitPrice { get; set; }

        public int Size { get; set; }

        public string Color { get; set; } = null!;

        public DiscountType Discount { get; set; }

        public string? Picture { get; set; }

        public int Ranking { get; set; }

        public string? Note { get; set; }

        public List<OrderDetailsEntity> OrderDetails { get; set; } = null!;
    }
}
