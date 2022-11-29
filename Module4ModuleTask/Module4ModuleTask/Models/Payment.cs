namespace Module4ModuleTask.Models
{
    public class Payment
    {
        public int Id { get; set; }

        public PaymentType PaymentType { get; set; }

        public bool Allowed { get; set; }
    }
}
