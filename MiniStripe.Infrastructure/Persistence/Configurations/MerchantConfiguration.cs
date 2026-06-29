using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniStripe.Domain.Entities;

namespace MiniStripe.Infrastructure.Persistence;

public class MerchantConfiguration : IEntityTypeConfiguration<Merchant>
{
    public void Configure(EntityTypeBuilder<Merchant> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Name).HasMaxLength(100).IsRequired();
        builder.Property(m => m.BankAccountNumber).HasMaxLength(50).IsRequired();
        builder.Property(m => m.CreatedAt).IsRequired();
    }
}
