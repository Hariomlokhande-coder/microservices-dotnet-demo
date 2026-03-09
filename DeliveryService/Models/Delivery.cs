namespace DeliveryService.Models
{
    public class Delivery
    {
        public int DeliveryId { get; set; }

        public int OrderId { get; set; }

        public string Address { get; set; }

        public string Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}