namespace MiniStripe.Domain.ValueObjects;

public class Money
{
    public decimal Amount {get; init;}
    public string Currency {get; init;} = string.Empty;


    public Money(decimal amount, string currency)
    {
        if (amount < 0) 
            throw new ArgumentException("Amount can never be a negative number");
        if (string.IsNullOrWhiteSpace(currency))
            throw new ArgumentException("Please specify the currency");
        Amount = amount;
        Currency = currency.ToUpper();
    }

    public bool Equals (Money other)
    {
        if (other is null) return false;
        return Amount == other.Amount && Currency == other.Currency;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;

        if (GetType() != obj.GetType()) return false;

        return Equals((Money)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Amount, Currency);
    }

    public static bool operator ==(Money a, Money b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
        if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;

        return a.Equals(b);
    }

    public static Money operator +(Money a, Money b)
    {
        if (a.Currency != b.Currency)
            throw new ArgumentException("Cannot Add money with different currencies");
        return new Money(a.Amount + b.Amount, a.Currency);
    }

    public static bool operator > (Money a, Money b)
    {
        if (a.Currency != b.Currency) 
            throw new ArgumentException("Cannot compare money with different currencies");
        return a.Amount > b.Amount;
    }

    public static bool operator < (Money a, Money b)
    {
        if (a.Currency != b.Currency) 
            throw new ArgumentException("Cannot compare money with different currencies");
        return a.Amount < b.Amount;
    }

    public static bool operator !=(Money a, Money b) => !(a == b);
    public static bool operator >=(Money a, Money b) => a > b || a == b;
    public static bool operator <=(Money a, Money b) => a < b || a == b;

    public override string ToString()
    {
        return $"{Currency} {Amount.ToString("F2")}";
    }

}