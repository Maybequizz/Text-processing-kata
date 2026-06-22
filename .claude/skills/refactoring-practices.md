# Refactoring Best Practices for .NET

## Core Principles

1. **Never break tests** — All tests must remain GREEN after refactoring
2. **Small steps** — Make atomic changes, one at a time
3. **Verify after each change** — Run tests after every modification
4. **Improve readability** — Code should communicate intent
5. **DRY (Don't Repeat Yourself)** — Eliminate duplication
6. **SOLID principles** — Follow single responsibility, open-closed, etc.

---

## .NET Code Style & Formatting Standards

### File-Scoped Namespaces (Modern .NET)
Use file-scoped namespaces (not the old block style):

```csharp
// GOOD: File-scoped namespace (C# 10+)
namespace TextProcessing.Services;

public class UserService { }

// OLD: Block-scoped namespace (avoid)
namespace TextProcessing.Services {
    public class UserService { }
}
```

### Class and Member Ordering
Follow this order within a class:

1. Fields & Constants
2. Properties (public, then private)
3. Constructors
4. Public Methods
5. Private Methods
6. Nested Classes/Types

```csharp
public class User
{
    // 1. Constants & Static Fields
    private const int MinAge = 18;
    private static int _nextId = 1;
    
    // 2. Fields
    private string _email;
    
    // 3. Properties
    public int Id { get; }
    public string Name { get; set; }
    
    // 4. Constructors
    public User(string name)
    {
        Name = name;
        Id = _nextId++;
    }
    
    // 5. Public Methods
    public bool IsAdult() => GetAge() >= MinAge;
    
    // 6. Private Methods
    private int GetAge() => DateTime.Now.Year - BirthYear;
}
```

### Naming Conventions

| Element | Convention | Example |
| :--- | :--- | :--- |
| Classes | PascalCase | `UserService`, `PaymentProcessor` |
| Methods | PascalCase | `GetUser()`, `ProcessPayment()` |
| Properties | PascalCase | `FirstName`, `IsActive` |
| Local Variables | camelCase | `userName`, `isValid` |
| Private Fields | _camelCase | `_userName`, `_emailService` |
| Constants | UPPER_SNAKE_CASE | `MAX_RETRIES`, `DEFAULT_TIMEOUT` |
| Parameters | camelCase | `userId`, `emailAddress` |
| Boolean Properties | Prefix with "Is" or "Has" | `IsActive`, `HasPermission` |

### Async/Await Naming
Methods that return Task should end with "Async":

```csharp
// GOOD
public async Task<User> GetUserAsync(int id) { }
public async Task SaveUserAsync(User user) { }

// BAD
public async Task<User> GetUser(int id) { }
public async Task SaveUserAsync(User user) { } // At least this is OK
```

---

## Common Refactoring Patterns

### 1. Extract Method (Reduce Complexity)
When a method is too long or has multiple responsibilities, extract pieces:

**Before:**
```csharp
public void ProcessUser(User user)
{
    // Validation logic (5 lines)
    if (user == null) throw new ArgumentNullException();
    if (string.IsNullOrEmpty(user.Email)) throw new ArgumentException();
    
    // Database logic (3 lines)
    var existingUser = _userRepository.GetByEmail(user.Email);
    if (existingUser != null) return;
    _userRepository.Save(user);
    
    // Email logic (3 lines)
    var emailBody = $"Welcome {user.Name}!";
    _emailService.Send(user.Email, emailBody);
}
```

**After:**
```csharp
public void ProcessUser(User user)
{
    ValidateUser(user);
    SaveNewUser(user);
    SendWelcomeEmail(user);
}

private void ValidateUser(User user)
{
    if (user == null) throw new ArgumentNullException();
    if (string.IsNullOrEmpty(user.Email)) throw new ArgumentException();
}

private void SaveNewUser(User user)
{
    var existingUser = _userRepository.GetByEmail(user.Email);
    if (existingUser != null) return;
    _userRepository.Save(user);
}

private void SendWelcomeEmail(User user)
{
    var emailBody = $"Welcome {user.Name}!";
    _emailService.Send(user.Email, emailBody);
}
```

### 2. Extract Variable (Improve Readability)
Replace magic numbers and complex expressions with named variables:

**Before:**
```csharp
if (user.Age > 18 && user.CreatedAt < DateTime.Now.AddYears(-1) && user.IsActive)
{
    // Grant premium access
}
```

**After:**
```csharp
bool isAdult = user.Age > 18;
bool isLongTimeUser = user.CreatedAt < DateTime.Now.AddYears(-1);
bool shouldGrantPremium = isAdult && isLongTimeUser && user.IsActive;

if (shouldGrantPremium)
{
    // Grant premium access
}
```

### 3. Replace Magic Numbers with Constants
```csharp
// BAD
if (count > 100) { }
var timeout = 5000;

// GOOD
private const int MaxItemsPerPage = 100;
private const int RequestTimeoutMs = 5000;

if (count > MaxItemsPerPage) { }
var timeout = RequestTimeoutMs;
```

### 4. Introduce Parameter Object (When Many Parameters)
**Before:**
```csharp
public void CreateUser(string firstName, string lastName, string email, 
                       int age, string city, string country)
{
    // Implementation
}
```

**After:**
```csharp
public record UserCreationRequest(
    string FirstName,
    string LastName,
    string Email,
    int Age,
    string City,
    string Country);

public void CreateUser(UserCreationRequest request)
{
    // Implementation
}
```

### 5. Inline Method (Remove Unnecessary Wrapper)
**Before:**
```csharp
public bool IsUserEligible(User user)
{
    return IsAdult(user);
}

private bool IsAdult(User user)
{
    return user.Age >= 18;
}
```

**After:**
```csharp
public bool IsUserEligible(User user)
{
    return user.Age >= 18;
}
```

### 6. Replace Loop with LINQ
**Before:**
```csharp
var activeUsers = new List<User>();
foreach (var user in users)
{
    if (user.IsActive)
    {
        activeUsers.Add(user);
    }
}
```

**After:**
```csharp
var activeUsers = users.Where(u => u.IsActive).ToList();
```

### 7. Extract Interface (For Dependency Injection)
**Before:**
```csharp
public class EmailService
{
    public void SendEmail(string to, string body) { }
}

public class UserService
{
    private EmailService _emailService; // Tight coupling
}
```

**After:**
```csharp
public interface IEmailService
{
    void SendEmail(string to, string body);
}

public class EmailService : IEmailService
{
    public void SendEmail(string to, string body) { }
}

public class UserService
{
    private readonly IEmailService _emailService; // Loose coupling
}
```

---

## Method Complexity Reduction (Cyclomatic Complexity)

### Red Flags (Too Complex)
- More than 3 levels of nesting
- More than 10 lines of code
- More than 3 conditional branches

### Guard Clauses (Early Return)
**Before:**
```csharp
public decimal CalculateDiscount(User user)
{
    if (user != null)
    {
        if (user.IsActive)
        {
            if (user.PurchaseHistory > 1000)
            {
                return 0.20m; // 20% discount
            }
        }
    }
    return 0;
}
```

**After:**
```csharp
public decimal CalculateDiscount(User user)
{
    if (user == null) return 0;
    if (!user.IsActive) return 0;
    if (user.PurchaseHistory <= 1000) return 0;
    
    return 0.20m; // 20% discount
}
```

### Extract Complex Conditions
**Before:**
```csharp
if ((user.Age > 18 && user.IsActive) || 
    (user.IsParentApproved && user.Age > 13))
{
    AllowAccess();
}
```

**After:**
```csharp
bool isAdultUser = user.Age > 18 && user.IsActive;
bool isApprovedMinor = user.IsParentApproved && user.Age > 13;

if (isAdultUser || isApprovedMinor)
{
    AllowAccess();
}
```

---

## SOLID Principles Quick Reference

### S — Single Responsibility
Each class should have only ONE reason to change:

```csharp
// BAD: Does parsing, validation, AND storage
public class UserProcessor
{
    public void ProcessUserCsv(string csv) { }
}

// GOOD: Each class has one job
public class UserCsvParser { }
public class UserValidator { }
public class UserRepository { }
```

### O — Open/Closed
Classes should be open for extension, closed for modification:

```csharp
// BAD: Must modify to add discount types
public class DiscountCalculator
{
    public decimal Calculate(User user)
    {
        if (user.Type == "Gold") return 0.20m;
        if (user.Type == "Silver") return 0.10m;
        return 0;
    }
}

// GOOD: Extend without modifying
public interface IDiscountStrategy
{
    decimal Calculate(User user);
}

public class GoldDiscountStrategy : IDiscountStrategy { }
public class SilverDiscountStrategy : IDiscountStrategy { }
```

### L — Liskov Substitution
Derived classes should substitute base classes without breaking code:

```csharp
// BAD: Bird.Fly() but Penguin can't fly
public abstract class Bird { public abstract void Fly(); }
public class Penguin : Bird { public override void Fly() => throw new NotImplementedException(); }

// GOOD: Separate flying ability
public abstract class Bird { }
public interface IFlyable { void Fly(); }
public class Sparrow : Bird, IFlyable { public void Fly() { } }
public class Penguin : Bird { } // No Fly method
```

### I — Interface Segregation
Clients should not depend on interfaces they don't use:

```csharp
// BAD: EmailService implements methods it doesn't need
public interface INotificationService
{
    void SendEmail();
    void SendSms();
    void SendPushNotification();
}

public class EmailService : INotificationService { }

// GOOD: Segregated interfaces
public interface IEmailService { void SendEmail(); }
public interface ISmsService { void SendSms(); }

public class EmailService : IEmailService { }
```

### D — Dependency Inversion
Depend on abstractions, not concretions:

```csharp
// BAD: Depends on concrete EmailService
public class UserService
{
    private EmailService _email = new EmailService();
}

// GOOD: Depends on abstraction
public class UserService
{
    private readonly IEmailService _email;
    public UserService(IEmailService email) => _email = email;
}
```

---

## Eliminating Code Duplication

### Example: DRY Violation
**Before (Duplication):**
```csharp
public class UserValidator
{
    public bool ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;
        return email.Contains("@");
    }
    
    public bool ValidatePhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone)) return false;
        return phone.Length >= 10;
    }
}
```

**After (DRY):**
```csharp
public class Validator
{
    private static bool IsPopulated(string value) 
        => !string.IsNullOrWhiteSpace(value);
    
    public bool ValidateEmail(string email) 
        => IsPopulated(email) && email.Contains("@");
    
    public bool ValidatePhone(string phone) 
        => IsPopulated(phone) && phone.Length >= 10;
}
```

---

## Refactoring Workflow Checklist

Before refactoring:
- [ ] All tests passing (GREEN)
- [ ] Understand the current code intent
- [ ] Have a specific improvement in mind

During refactoring:
- [ ] Make ONE atomic change
- [ ] Run tests immediately after
- [ ] If tests fail, revert and retry
- [ ] Commit small, meaningful changes

After refactoring:
- [ ] All tests still passing
- [ ] Code is more readable
- [ ] No behavioral change
- [ ] Performance not degraded (verify if important)

---

## Red Flags: Code That Needs Refactoring

✅ **Extract method** if: More than 20 lines, multiple responsibilities
✅ **Extract variable** if: Complex expression, unclear magic number
✅ **Introduce parameter object** if: More than 3-4 parameters
✅ **Extract interface** if: To reduce coupling or enable testing
✅ **Reduce nesting** if: More than 3 levels deep
✅ **Remove duplication** if: Same logic appears 2+ times
✅ **Simplify conditionals** if: Hard to read boolean logic
✅ **Use LINQ** if: Simple collection filtering/mapping

---

## Tools for .NET Refactoring

### ReSharper / Rider
- Automated refactoring suggestions
- Code inspections
- Quick fixes

### StyleCop Analyzers
Enforce naming conventions and style:
```bash
dotnet add package StyleCop.Analyzers
```

### SonarAnalyzer
Detect code smells:
```bash
dotnet add package SonarAnalyzer.CSharp
```

---

## Summary

✅ **DO:**
- Use file-scoped namespaces
- Follow PascalCase/camelCase/_camelCase standards
- Extract methods to reduce complexity
- Use LINQ instead of loops
- Apply SOLID principles
- Make atomic changes
- Run tests after each refactoring

❌ **DON'T:**
- Refactor while tests are failing
- Make multiple changes at once
- Change behavior during refactoring
- Ignore code style standards
- Skip test verification
- Over-engineer solutions
