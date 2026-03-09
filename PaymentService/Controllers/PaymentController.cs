using Microsoft.AspNetCore.Mvc;
using PaymentService.Data;
using PaymentService.Models;

namespace PaymentService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentDbContext _context;

        public PaymentController(PaymentDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult ProcessPayment(Payment payment)
        {
            _context.Payments.Add(payment);
            _context.SaveChanges();
            return Ok(payment);
        }

        [HttpGet]
        public IActionResult GetPayments()
        {
            var payments = _context.Payments.ToList();
            return Ok(payments);
        }
    }
}