namespace OrderService.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public string ProductName { get; set; }

        public double Price { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}