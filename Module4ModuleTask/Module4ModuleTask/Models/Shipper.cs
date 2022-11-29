namespace Module4ModuleTask.Models
{
    public class Shipper
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Phone { get; set; }

        public List<Order>? Orders { get; set; }
    }
}
