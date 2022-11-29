namespace Module4ModuleTask.Models
{
    public class Supplier
    {
        public int Id { get; set; }

        public string CompanyName { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string City { get; set; } = null!;

        public string? State { get; set; }

        public int PostalCode { get; set; }

        public string Country { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Fax { get; set; }

        public string? Email { get; set; }

        public DiscountType DiscountType { get; set; }

        public string? Notes { get; set; }

        public bool DiscountAvailable { get; set; }

        public List<Product>? Products { get; set; }
    }
}
