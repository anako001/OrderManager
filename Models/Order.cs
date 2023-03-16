namespace OrderManager.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string CustomerName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedByUsername { get; set; }
    }
}
