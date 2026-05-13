namespace MiniStripe.Domain.Entities;

public class Merchant
{
    public string Name {get; init;} = string.Empty;
    public Guid Id {get; init;} 
    public string BankAccountNumber {get; init;} = string.Empty;
    public DateTime CreatedAt {get; init;}

    public Merchant (string name, string bankAccountNumber)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException ("Name field cannot be null empty.");
        if (string.IsNullOrWhiteSpace(bankAccountNumber))
            throw new ArgumentException("Account number field cannot be null or empty");
        Name = name;
        Id = Guid.NewGuid();
        BankAccountNumber = bankAccountNumber;
        CreatedAt = DateTime.UtcNow;
    }

}


