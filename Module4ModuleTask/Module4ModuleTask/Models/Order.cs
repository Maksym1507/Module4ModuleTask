namespace Module4ModuleTask.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int PaymentId { get; set; }

        public Payment? Payment { get; set; }

        public DateTime ShipDate { get; set; }

        public DateTime RequiredDate { get; set; }

        public int ShipperId { get; set; }

        public Shipper? Shipper { get; set; }

        public int SalesTax { get; set; }

        public TransactStatus TransactStatus { get; set; }

        public bool Fulfilled { get; set; }

        public bool Deleted { get; set; }

        public bool Paid { get; set; }

        public DateTime PaymentDate { get; set; }

        public IEnumerable<OrderItem>? OrderDetails { get; set; }
    }
}
