using MiniStripe.Domain.Enums;
using MiniStripe.Domain.Interfaces;

namespace MiniStripe.Application.Commands
{
    public class ConfirmPaymentHandler
    {
        private readonly IPaymentRepository _paymentRepository;
        private static readonly Random _random = new Random();
        
        public ConfirmPaymentHandler(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<PaymentStatus> HandleAsync(ConfirmPaymentCommand paymentCommand)
        {
            var paymentIntent = await _paymentRepository.GetAsync(paymentCommand.Id);
            if (paymentIntent is null)
                throw new KeyNotFoundException($"Payment with ID {paymentCommand.Id} was not found.");
            var succeeded = _random.Next(0, 2) == 1;

            if (succeeded) 
                paymentIntent.Complete();
            else 
                paymentIntent.Fail();

            await _paymentRepository.UpdateAsync(paymentIntent);
            
            return paymentIntent.Status;
        }
    }
}