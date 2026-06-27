using System.ComponentModel;
using MiniStripe.Domain.ValueObjects;

namespace MiniStripe.Application.Commands
{
    public class CreatePaymentCommand
    {
        public required Money Amount { get; init;}
        public Guid MerchantId {get; init;}
        public Guid CustomerId {get; init;}
    }
}