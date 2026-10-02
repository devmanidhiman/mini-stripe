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
        public async Task<IActionResult> CreatePaymentAsync([FromBody] CreatePaymentCommand paymentCommand)
        {
            Guid id = await _createPayment.HandleAsync(paymentCommand);
            // nameof(GetPaymentAsync) - tells ASP.NET which action to use for building the Location header URL
            // first new { id } - route values to build the Location URL e.g. /api/payment/{id}
            // second new { id } - the JSON response body returned to the client
            //return CreatedAtAction(nameof(GetPaymentAsync), new { id = id }, new { id = id });
            return Created($"/api/payment/{id}", new { id });
        }

        [HttpPost("{id}/confirm")]
        public async Task<IActionResult> ConfirmPaymentAsync([FromRoute] Guid id)
        {
            var command = new ConfirmPaymentCommand{Id = id};
            var result = await _confirmPayment.HandleAsync(command);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentAsync([FromRoute] Guid id)
        {
            var query = new GetPaymentQuery{Id = id};
            var result = await _getPayment.HandleAsync(query);
            return Ok(result);
        } 
    }
}
