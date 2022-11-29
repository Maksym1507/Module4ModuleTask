using Module4ModuleTask.Data.Entities;

namespace Module4ModuleTask.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public int Class { get; set; }

        public int Room { get; set; }

        public string? Building { get; set; }

        public string Address { get; set; } = null!;

        public string City { get; set; } = null!;

        public string? State { get; set; }

        public int PostalCode { get; set; }

        public string Country { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string Password { get; set; } = null!;

        public string? CreditCard { get; set; }

        public List<Order>? Orders { get; set; }
    }
}
