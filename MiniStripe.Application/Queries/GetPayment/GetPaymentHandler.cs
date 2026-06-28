using MiniStripe.Domain.Entities;
using MiniStripe.Domain.Interfaces;

namespace MiniStripe.Application.Queries
{
    public class GetPaymentHandler
    {
        private readonly IPaymentRepository _paymentRepository;
        public GetPaymentHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<PaymentIntent> HandleAsync(GetPaymentQuery paymentQuery)
        {
            var paymentIntent = await _paymentRepository.GetAsync(paymentQuery.Id);
            if (paymentIntent is null)
                throw new KeyNotFoundException($"Payment with ID {paymentQuery.Id} was not found.");
            
            return paymentIntent;
        }
    }
}