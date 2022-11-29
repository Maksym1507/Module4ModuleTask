using Module4ModuleTask.Models;

namespace Module4ModuleTask.Data.Entities
{
    public class PaymentEntity
    {
        public int Id { get; set; }

        public PaymentType PaymentType { get; set; }

        public bool Allowed { get; set; }

        public List<OrderEntity> Orders { get; set; } = null!;
    }
}
