using Microsoft.AspNetCore.Mvc;
using DeliveryService.Data;
using DeliveryService.Models;

namespace DeliveryService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryController : ControllerBase
    {
        private readonly DeliveryDbContext _context;

        public DeliveryController(DeliveryDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult CreateDelivery(Delivery delivery)
        {
            _context.Deliveries.Add(delivery);
            _context.SaveChanges();
            return Ok(delivery);
        }

        [HttpGet]
        public IActionResult GetDeliveries()
        {
            var deliveries = _context.Deliveries.ToList();
            return Ok(deliveries);
        }
    }
}