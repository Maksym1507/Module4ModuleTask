using Module4ModuleTask.Models;

namespace Module4ModuleTask.Data.Entities
{
    public class OrderEntity
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public CustomerEntity? Customer { get; set; }

        public int PaymentId { get; set; }

        public PaymentEntity? Payment { get; set; }

        public DateTime OrderDate { get; set; }

        public DateTime ShipDate { get; set; }

        public DateTime RequiredDate { get; set; }

        public int ShipperId { get; set; }

        public ShipperEntity? Shipper { get; set; }

        public int SalesTax { get; set; }

        public TransactStatus TransactStatus { get; set; }

        public bool Fulfilled { get; set; }

        public bool Deleted { get; set; }

        public bool Paid { get; set; }

        public DateTime PaymentDate { get; set; }

        public List<OrderDetailsEntity> OrderDetails { get; set; } = null!;
    }
}
