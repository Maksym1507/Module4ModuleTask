namespace Module4ModuleTask.Data.Entities
{
    public class ShipperEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Phone { get; set; }

        public List<OrderEntity> Orders { get; set; } = null!;
    }
}
