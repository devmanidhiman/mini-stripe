using MiniStripe.Domain.Entities;
using MiniStripe.Domain.Interfaces;

namespace MiniStripe.Infrastructure.Persistence;

public class PaymentRepository : IPaymentRepository
{
    private readonly MiniStripeDbContext _dbContext;
    public PaymentRepository (MiniStripeDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(PaymentIntent paymentIntent)
    {
        await _dbContext.PaymentIntents.AddAsync(paymentIntent);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<PaymentIntent?> GetAsync(Guid id)
    {
        return await _dbContext.FindAsync<PaymentIntent>(id);
    }

    public async Task UpdateAsync(PaymentIntent paymentIntent)
    {
        _dbContext.PaymentIntents.Update(paymentIntent);
        await _dbContext.SaveChangesAsync();
    }
}
