# Testing Best Practices for .NET TDD

## Test Naming Convention

Use the **MethodUnderTest_Scenario_ExpectedBehavior** pattern exclusively.

### Good Examples:
- `Withdraw_WithSufficientFunds_UpdatesBalance()`
- `Add_WithPositiveNumbers_ReturnsCorrectSum()`
- `Parse_WithInvalidJson_ThrowsFormatException()`
- `Filter_WithEmptyCollection_ReturnsEmptyCollection()`

### Bad Examples:
- `TestWithdraw()` — Too generic, unclear what is being tested
- `ShouldWork()` — Lacks specificity
- `Test1()` — Meaningless identifier

---

## Test Structure: AAA Pattern (Arrange-Act-Assert)

Every test MUST clearly separate three stages with explicit code comments:

```csharp
[Test]
public void Calculate_WithValidInputs_ReturnsExpectedResult()
{
    // Arrange: Set up test data and test doubles
    var calculator = new Calculator();
    int a = 5;
    int b = 3;
    
    // Act: Execute the method under test
    int result = calculator.Add(a, b);
    
    // Assert: Verify the outcome
    result.Should().Be(8);
}
```

### Arrange Phase:
- Create test data
- Initialize System Under Test (SUT)
- Set up test doubles (mocks, stubs, fakes)
- Configure fixtures
- **Keep it focused**: Only include data relevant to the test

### Act Phase:
- Call exactly ONE public method on the SUT
- Pass only the inputs you arranged
- Capture the return value or exception
- **Single responsibility**: One logical operation

### Assert Phase:
- Verify one or more outcomes related to your test name
- Use the most specific assertion available
- Never modify data after Act and before Assert

---

## Assertion Strategy: AwesomeAssertions Required

**CRITICAL RULE: Only AwesomeAssertions are allowed. Never use NUnit/xUnit native Assert.**

### Why AwesomeAssertions?
- Fluent interface reads like natural language
- Chaining assertions together improves readability
- Better error messages on failure
- Type-safe at compile time

### Assertion Patterns by Scenario

| Scenario | Good (AwesomeAssertions) | Bad (Native Assert) |
| :--- | :--- | :--- |
| **Equality** | `result.Should().Be(expected);` | `Assert.Equal(expected, result);` |
| **Inequality** | `result.Should().NotBe(0);` | `Assert.NotEqual(0, result);` |
| **Null checks** | `result.Should().BeNull();` | `Assert.Null(result);` |
| **Not null** | `result.Should().NotBeNull();` | `Assert.NotNull(result);` |
| **Boolean** | `flag.Should().BeTrue();` | `Assert.True(flag);` |
| **String contains** | `message.Should().Contain("error");` | `Assert.Contains("error", message);` |
| **Collection count** | `items.Should().HaveCount(5);` | `Assert.Equal(5, items.Count);` |
| **Collection contains** | `items.Should().Contain(item);` | `Assert.Contains(item, items);` |
| **Exceptions** | `action.Should().Throw<ArgumentNullException>();` | `Assert.Throws<ArgumentNullException>(action);` |
| **Exception message** | `action.Should().Throw<InvalidOperationException>().WithMessage("*expected*");` | `Assert.Throws<InvalidOperationException>(action);` |
| **Range check** | `value.Should().BeInRange(1, 100);` | Multiple Assert.True calls |
| **Type check** | `result.Should().BeOfType<string>();` | `Assert.IsType<string>(result);` |

### Chaining Assertions
AwesomeAssertions allow logical chaining with `.And`:

```csharp
// Good: Chained assertions
result.Should()
    .Be(10)
    .And.BeGreaterThan(0)
    .And.BeLessThan(20);

// Also good: Multiple independent assertions
str.Should().StartWith("Hello").And.EndWith("World");
str.Should().Contain("test").And.NotContain("debug");
```

---

## Test Setup & Fixtures

### Shared Fixtures (Use Sparingly)
Only use `SetUp` methods for setup that is **identical across most tests** in the class:

```csharp
[TestFixture]
public class CalculatorTests
{
    private Calculator _calculator;
    
    [SetUp]
    public void Setup()
    {
        // Shared setup: only if ALL or almost ALL tests need this
        _calculator = new Calculator();
    }
    
    [Test]
    public void Add_WithValidInputs_ReturnsCorrectSum()
    {
        // OK: _calculator is used
    }
}
```

### Test-Specific Setup (Preferred)
Keep test-specific setup **inside each test method**:

```csharp
[Test]
public void Divide_WithZeroDivisor_ThrowsArgumentException()
{
    // Arrange
    var calculator = new Calculator();
    
    // Act & Assert
    calculator.Invoking(c => c.Divide(10, 0))
        .Should().Throw<ArgumentException>();
}
```

### When to Use Fixtures:
- Common resource initialization (database, file system mocks)
- Shared test data that multiple tests truly depend on
- Expensive resource creation

### When NOT to Use Fixtures:
- Different tests need different variations
- Some tests don't use certain fixtures
- It makes the test harder to understand

---

## Test Doubles: Mocks, Stubs, and Fakes

### Stub (Fake Return Values)
Use when you need canned responses:

```csharp
var stubRepository = new Mock<IUserRepository>();
stubRepository
    .Setup(r => r.GetUser(1))
    .Returns(new User { Id = 1, Name = "John" });

var service = new UserService(stubRepository.Object);
```

### Mock (Verify Behavior)
Use when you need to verify method calls:

```csharp
var mockLogger = new Mock<ILogger>();

var service = new UserService(mockLogger.Object);
service.CreateUser(new User { Name = "Jane" });

mockLogger.Verify(
    l => l.Log(It.Is<string>(msg => msg.Contains("User created"))),
    Times.Once
);
```

### Assertion on Mocks
Use AwesomeAssertions even with mocks:

```csharp
var mock = new Mock<IEmailService>();
// ... execute code ...
mock.Invocations.Should().HaveCount(1);
```

---

## Mutation Testing: Ensuring Test Quality

Tests should catch real bugs. Use mutation testing to verify your tests are effective.

### What is Mutation Testing?
Mutation testing introduces small changes (mutations) to production code to verify your tests catch them. High mutation kill rate (>80%) means your tests are effective.

### Running Mutation Tests (Stryker.Net)
```bash
dotnet tool install -g dotnet-stryker
dotnet stryker --solution-path Text\ Processing.sln
```

### Common Mutations Your Tests Should Catch
1. **Boundary mutations**: `==` becomes `!=`, `>` becomes `>=`
2. **Constant mutations**: `0` becomes `1`, strings change
3. **Logical operator mutations**: `&&` becomes `||`
4. **Return value mutations**: return value changes

### Writing Tests That Kill Mutations
```csharp
// BAD: Won't catch if method returns wrong value
[Test]
public void Add_TwoNumbers_NoExceptionThrown()
{
    var result = calculator.Add(2, 3);
    // No assertion! This will pass even if Add returns 0
}

// GOOD: Specific assertions that catch mutations
[Test]
public void Add_WithPositiveNumbers_ReturnsSum()
{
    var result = calculator.Add(2, 3);
    result.Should().Be(5);              // Catches if result is 4, 6, etc.
    result.Should().BeGreaterThan(0);   // Catches if result becomes negative
}
```

---

## Test Organization by Feature

Organize test classes by feature/functionality:

```
TestProject1/
├── Features/
│   ├── UserManagement/
│   │   ├── CreateUserTests.cs
│   │   ├── UpdateUserTests.cs
│   │   └── DeleteUserTests.cs
│   ├── Payments/
│   │   ├── ProcessPaymentTests.cs
│   │   └── RefundPaymentTests.cs
│   └── Notifications/
│       └── EmailNotificationTests.cs
```

### One Test Class Per Public Behavior

```csharp
// Good: One class, tests for one logical behavior
[TestFixture]
public class UserRepository_GetUserById_Tests
{
    [Test]
    public void GetUserById_WithValidId_ReturnsUser() { }
    
    [Test]
    public void GetUserById_WithInvalidId_ThrowsException() { }
    
    [Test]
    public void GetUserById_WithDeletedUser_ReturnsNull() { }
}
```

---

## Common Anti-Patterns to Avoid

### 1. Tests That Share State
```csharp
// BAD: Tests depend on execution order
private int sharedCounter = 0;

[Test]
public void Test1_IncrementCounter()
{
    sharedCounter++;
}

[Test]
public void Test2_CheckCounter()
{
    sharedCounter.Should().Be(1); // Fails if Test1 didn't run
}

// GOOD: Each test is independent
[Test]
public void Test1_IncrementCounter()
{
    int counter = 0;
    counter++;
    counter.Should().Be(1);
}
```

### 2. Testing Multiple Concerns
```csharp
// BAD: Testing both validation AND calculation
[Test]
public void Calculate_WithInvalidInputAndProperCalculation()
{
    var sut = new Calculator();
    sut.Invoking(c => c.Add(-1, 5))  // Testing validation
        .Should().Throw<ArgumentException>();
    // But also trying to test calculation...
}

// GOOD: One concern per test
[Test]
public void Add_WithNegativeNumber_ThrowsArgumentException()
{
    sut.Invoking(c => c.Add(-1, 5))
        .Should().Throw<ArgumentException>();
}

[Test]
public void Add_WithValidNumbers_ReturnsCorrectSum()
{
    sut.Add(5, 3).Should().Be(8);
}
```

### 3. Missing Assertions
```csharp
// BAD: Act without Assert
[Test]
public void Process_WithData_DoesNotThrow()
{
    processor.Process(data); // No assertion!
}

// GOOD: Explicit assertions about the result
[Test]
public void Process_WithData_ReturnsProcessedResult()
{
    var result = processor.Process(data);
    result.Should().NotBeNull();
    result.IsValid.Should().BeTrue();
}
```

### 4. Generic Exception Assertions
```csharp
// BAD: Too generic
[Test]
public void Parse_WithInvalidJson_ThrowsException()
{
    json.Invoking(j => JsonConvert.DeserializeObject(j))
        .Should().Throw<Exception>();
}

// GOOD: Specific exception type and message
[Test]
public void Parse_WithInvalidJson_ThrowsJsonReaderException()
{
    invalidJson.Invoking(j => JsonConvert.DeserializeObject(j))
        .Should().Throw<JsonReaderException>()
        .WithMessage("*Unexpected end of file*");
}
```

---

## Property-Based Testing (Advanced)

When you want to verify a property holds for many inputs, use property-based testing:

```csharp
[Theory]
[PropertyData(nameof(GenerateValidNumbers))]
public void Add_WithAnyValidNumbers_IsCommutative(int a, int b)
{
    var result1 = calculator.Add(a, b);
    var result2 = calculator.Add(b, a);
    
    result1.Should().Be(result2);
}
```

This ensures your addition is commutative for multiple input pairs.

---

## Running Tests Effectively

### Run All Tests
```bash
dotnet test
```

### Run Specific Test Class
```bash
dotnet test --filter "FullyQualifiedName~CalculatorTests"
```

### Run Tests with Code Coverage
```bash
dotnet test /p:CollectCoverage=true
```

### Run Mutation Tests
```bash
dotnet stryker
```

---

## Summary

✅ **DO:**
- Use AwesomeAssertions exclusively
- Name tests: MethodUnderTest_Scenario_ExpectedBehavior
- Use AAA pattern with explicit comments
- Write one assertion per logical behavior
- Keep tests independent
- Verify mutation kill rate >= 80%

❌ **DON'T:**
- Use native Assert.* methods
- Test multiple concerns in one test
- Skip assertions
- Share state between tests
- Use overly generic exception assertions
