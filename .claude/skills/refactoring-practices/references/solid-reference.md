# SOLID Principles Quick Reference

## S — Single Responsibility
One reason to change per class/method.

```csharp
// BAD: Parses, validates, AND stores
public class UserProcessor { }

// GOOD: Each class has one job
public class UserCsvParser { }
public class UserValidator { }
public class UserRepository { }
```

## O — Open/Closed
Open for extension, closed for modification.

```csharp
// BAD: Modify to add discount types
public class DiscountCalculator
{
    public decimal Calculate(User user) => user.Type switch
    {
        "Gold" => 0.20m,
        "Silver" => 0.10m,
        _ => 0
    };
}

// GOOD: Extend via strategy pattern
public interface IDiscountStrategy
{
    decimal Calculate(User user);
}
```

## L — Liskov Substitution
Derived classes must be substitutable for base.

```csharp
// BAD: Penguin can't fly but inherits Fly()
public abstract class Bird { public abstract void Fly(); }

// GOOD: Separate flying into interface
public interface IFlyable { void Fly(); }
public class Sparrow : Bird, IFlyable { }
public class Penguin : Bird { }
```

## I — Interface Segregation
Don't force clients to depend on methods they don't use.

```csharp
// BAD: EmailService forced to implement SendSms
public interface INotification { void SendEmail(); void SendSms(); }

// GOOD: Segregated interfaces
public interface IEmailService { void SendEmail(); }
public interface ISmsService { void SendSms(); }
```

## D — Dependency Inversion
Depend on abstractions, not concretions.

```csharp
// BAD
public class UserService
{
    private EmailService _email = new();
}

// GOOD
public class UserService(IEmailService email)
{
    private readonly IEmailService _email = email;
}
```
