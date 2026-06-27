using MiniStripe.Domain.Entities;

namespace MiniStripe.Domain.Interfaces
{
    public interface  IPaymentRepository
    {
        Task AddAsync(PaymentIntent paymentIntent);
        Task<PaymentIntent?> GetAsync(Guid id);
        Task UpdateAsync(PaymentIntent paymentIntent);

    }
}
