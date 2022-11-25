using Module4ModuleTask.Models;

namespace Module4ModuleTask.Data.Entities
{
    public class OrderDetailsEntity
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public OrderEntity? Order { get; set; }

        public int ProductId { get; set; }

        public ProductEntity? Product { get; set; }

        public int OrderNumber { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public DiscountType Discount { get; set; }

        public decimal Total { get; set; }

        public int Size { get; set; }

        public string Color { get; set; } = null!;

        public bool Fulfilled { get; set; }

        public DateTime ShipDate { get; set; }

        public DateTime BillDate { get; set; }
    }
}
