using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniStripe.Domain.Entities;

namespace MiniStripe.Infrastructure.Persistence;

public class CustomerConfiguration :IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).HasMaxLength(100).IsRequired();
        builder.Property(c => c.Email).HasMaxLength(255).IsRequired();
        builder.Property(c => c.BankAccountNumber).HasMaxLength(50).IsRequired();
        builder.Property(c => c.CreatedAt).IsRequired();
    }
}
