using MiniStripe.Domain.Enums;
using MiniStripe.Domain.ValueObjects;

namespace MiniStripe.Domain.Entities;

public class PaymentIntent
{
    public Guid Id {get; init;}
    public Money Amount {get; init;}
    public PaymentStatus Status {get; private set;}
    public Guid MerchantId {get; init;}
    public Guid CustomerId {get; init;}
    public DateTime CreatedAt {get; init;}
    public DateTime? CompletedAt {get; private set;}

    public PaymentIntent(Money amount, Guid merchantId, Guid customerId)
    {
        Id = Guid.NewGuid();
        Amount = amount;
        Status = PaymentStatus.Pending;
        MerchantId = merchantId;
        CustomerId = customerId;
        CreatedAt = DateTime.UtcNow;
        CompletedAt = null;

    }

    public void Complete()
    {
        if (this.Status != PaymentStatus.Pending)
        {
            throw new InvalidOperationException("Only the payments which are pending can be completed.");
        }
        this.Status = PaymentStatus.Completed;
        this.CompletedAt = DateTime.UtcNow;
    }

    public void Fail()
    {
        if (this.Status != PaymentStatus.Pending)
        {
            throw new InvalidOperationException("Completed or Cancelled payment can't be failed");
        }
        this.Status = PaymentStatus.Failed;
        this.CompletedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        if (this.Status != PaymentStatus.Pending)
        {
            throw new InvalidOperationException("Completed or failed payments can't be cancelled");
        }
        this.Status = PaymentStatus.Cancelled;
        this.CompletedAt = DateTime.UtcNow;
    }

}