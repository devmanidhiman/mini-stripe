namespace MiniStripe.Domain.Entities;

public class Customer
{
    public Guid Id {get; init;}
    public string Name {get; init;}= string.Empty;
    public string BankAccountNumber {get; init;}= string.Empty;
    public string Email {get; init;} = string.Empty;
    public DateTime CreatedAt {get; init;}

    public Customer(string name, string bankAccountNumber, string email)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException ("Name field cannot be null empty.");
        if (string.IsNullOrWhiteSpace(bankAccountNumber))
            throw new ArgumentException("Account number field cannot be null or empty");
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email field cannot be null or empty");
        Name = name;
        Id = Guid.NewGuid();
        BankAccountNumber = bankAccountNumber;
        Email = email;
        CreatedAt = DateTime.UtcNow;
    }
}
