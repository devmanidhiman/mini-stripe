using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniStripe.Application.Commands;
using MiniStripe.Application.Queries;

namespace MiniStripe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ConfirmPaymentHandler _confirmPayment;
        private readonly CreatePaymentHandler _createPayment;
        private readonly GetPaymentHandler _getPayment;        
        
        public PaymentController(CreatePaymentHandler createPayment, 
            ConfirmPaymentHandler confirmPayment, GetPaymentHandler getPayment)
        {
            _confirmPayment = confirmPayment;
            _createPayment = createPayment;
            _getPayment = getPayment;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePaymentAsync([FromBody] CreatePaymentCommand createPaymentCommand)
        {
            
        }

        [HttpPost("{id}/confirm")]
        public async Task<IActionResult> ConfirmPaymentAsync([FromRoute] Guid id)
        {
            
        } 

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentAsync([FromRoute] Guid id)
        {
            
        } 
    }
}
