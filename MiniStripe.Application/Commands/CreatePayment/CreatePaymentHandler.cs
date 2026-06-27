using MiniStripe.Domain.Entities;
using MiniStripe.Domain.Interfaces;

namespace MiniStripe.Application.Commands
{
    public class CreatePaymentHandler
    {
        private readonly IPaymentRepository _paymentRepository;
        public CreatePaymentHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<Guid> HandleAsync (CreatePaymentCommand paymentCommand)
        {

            var paymentIntent = new PaymentIntent(paymentCommand.Amount, 
                                    paymentCommand.MerchantId, 
                                    paymentCommand.CustomerId);

            await _paymentRepository.AddAsync(paymentIntent);
            return paymentIntent.Id;

        }
    }
}