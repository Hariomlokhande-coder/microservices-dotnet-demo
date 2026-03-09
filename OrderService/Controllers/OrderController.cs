using Microsoft.AspNetCore.Mvc;
using OrderService.Data;
using OrderService.Models;
using System.Text;
using System.Text.Json;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly OrderDbContext _context;
        private readonly HttpClient _httpClient;

        public OrderController(OrderDbContext context, IHttpClientFactory httpClientFactory)
        {
            _context = context;
            _httpClient = httpClientFactory.CreateClient();
        }

        // Create Order
        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            order.CreatedDate = DateTime.Now;
            order.CreatedBy = "Admin";

            _context.Orders.Add(order);
            _context.SaveChanges();

            var payment = new
            {
                orderId = order.OrderId,
                amount = order.Price,
                status = "Pending"
            };

            var json = JsonSerializer.Serialize(payment);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await _httpClient.PostAsync("https://localhost:7065/api/payment", content);

            return Ok(order);
        }

        // Get All Orders
        [HttpGet]
        public IActionResult GetOrders()
        {
            var orders = _context.Orders.ToList();
            return Ok(orders);
        }

        // Get Order by Id
        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            var order = _context.Orders.Find(id);

            if (order == null)
                return NotFound();

            return Ok(order);
        }

        // Update Order
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, Order updatedOrder)
        {
            var order = _context.Orders.Find(id);

            if (order == null)
                return NotFound();

            order.ProductName = updatedOrder.ProductName;
            order.Price = updatedOrder.Price;

            order.UpdatedDate = DateTime.Now;
            order.UpdatedBy = "Admin";

            _context.SaveChanges();

            return Ok(order);
        }

        // Delete Order
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var order = _context.Orders.Find(id);

            if (order == null)
                return NotFound();

            order.IsDeleted = true;
            order.UpdatedDate = DateTime.Now;
            order.UpdatedBy = "Admin";

            _context.SaveChanges();

            return Ok("Order soft deleted");
        }
    }
}