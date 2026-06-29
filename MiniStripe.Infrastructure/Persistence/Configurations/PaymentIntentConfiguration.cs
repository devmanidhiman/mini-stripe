using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniStripe.Domain.Entities;

namespace MiniStripe.Infrastructure.Persistence;

public class PaymentIntentConfiguration : IEntityTypeConfiguration<PaymentIntent>
{
    public void Configure(EntityTypeBuilder<PaymentIntent> builder)
    {
        builder.HasKey(p => p.Id);
        builder.OwnsOne(p => p.Amount, money =>
        {
           money.Property(m => m.Amount).HasColumnName("Amount").IsRequired();
           money.Property(m => m.Currency).HasColumnName("Currency").HasMaxLength(3).IsRequired();
        });
        builder.Property(p => p.Status).HasConversion<string>().IsRequired();
        builder.Property(p => p.MerchantId).IsRequired();
        builder.Property(p => p.CustomerId).IsRequired();
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.CompletedAt).IsRequired(false);

    }

}
