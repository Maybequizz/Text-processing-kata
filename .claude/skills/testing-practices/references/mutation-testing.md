# Mutation Testing with Stryker.NET

## Install & Run
```bash
dotnet tool install -g dotnet-stryker
dotnet stryker --solution-path "Text Processing.sln"
```

## Target Kill Rate: >= 80%

## Common Mutations Tests Should Catch
- Boundary: `==` → `!=`, `>` → `>=`
- Constants: `0` → `1`, string changes
- Logical: `&&` → `||`
- Return values

## Writing Mutation-Killing Tests

```csharp
// Weak: no assertion
[Test]
public void Add_TwoNumbers_NoException() => calculator.Add(2, 3);

// Strong: specific assertion kills mutants
[Test]
public void Add_WithPositiveNumbers_ReturnsSum()
{
    calculator.Add(2, 3).Should().Be(5);
}
```
