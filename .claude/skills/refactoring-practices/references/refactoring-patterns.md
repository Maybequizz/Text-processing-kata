# Refactoring Patterns Catalog

## Extract Method

**Before:**
```csharp
public void ProcessUser(User user)
{
    if (user == null) throw new ArgumentNullException();
    if (string.IsNullOrEmpty(user.Email)) throw new ArgumentException();
    var existing = _repository.GetByEmail(user.Email);
    if (existing != null) return;
    _repository.Save(user);
    _email.Send(user.Email, $"Welcome {user.Name}!");
}
```

**After:**
```csharp
public void ProcessUser(User user)
{
    ValidateUser(user);
    SaveIfNew(user);
    SendWelcomeEmail(user);
}

private void ValidateUser(User user) { ... }
private void SaveIfNew(User user) { ... }
private void SendWelcomeEmail(User user) { ... }
```

## Guard Clauses (Reduce Nesting)

**Before:**
```csharp
if (user != null)
    if (user.IsActive)
        if (user.PurchaseHistory > 1000)
            return 0.20m;
return 0;
```

**After:**
```csharp
if (user == null) return 0;
if (!user.IsActive) return 0;
if (user.PurchaseHistory <= 1000) return 0;
return 0.20m;
```

## Replace Loop with LINQ

**Before:**
```csharp
var active = new List<User>();
foreach (var u in users)
    if (u.IsActive) active.Add(u);
```

**After:**
```csharp
var activeUsers = users.Where(u => u.IsActive).ToList();
```

## Introduce Parameter Object

**Before:**
```csharp
public void CreateUser(string first, string last, string email, int age) { }
```

**After:**
```csharp
public record UserRequest(string First, string Last, string Email, int Age);
public void CreateUser(UserRequest request) { }
```
