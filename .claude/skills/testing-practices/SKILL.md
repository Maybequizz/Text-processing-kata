---
name: testing-practices
description: |
  .NET TDD testing conventions for NUnit + AwesomeAssertions projects.
  Use when writing, reviewing, or fixing tests in this project.
  Trigger phrases: "write test", "add test", "create test", "fix test", "TDD".
  Enforces AAA pattern with Arrange/Act/Assert comments, MethodUnderTest_Scenario_ExpectedBehavior naming, and AwesomeAssertions exclusively.
  Not for: production code, refactoring logic.
---

# Testing Practices

## Hard Rules (must-follow)

### 1. Naming Convention: `MethodUnderTest_Scenario_ExpectedBehavior`

```
Analyze_WithSingleWord_ReturnsWordAsMostUsed()
Add_WithPositiveNumbers_ReturnsSum()
Parse_WithInvalidInput_ThrowsFormatException()
```

### 2. AAA Pattern with Explicit Comments

Every test MUST have three sections with comment headers:

```csharp
[Test]
public void MethodUnderTest_Scenario_ExpectedBehavior()
{
    // Arrange
    var sut = new TextProcessor();
    var input = "Hello";

    // Act
    var result = sut.Analyze(input);

    // Assert
    result.Should().Be(expected);
}
```

### 3. AwesomeAssertions Only (NO native NUnit Assert)

| Allowed | Forbidden |
|---|---|
| `result.Should().Be(value)` | `Assert.Equal(value, result)` |
| `result.Should().NotBeNull()` | `Assert.NotNull(result)` |
| `collection.Should().HaveCount(5)` | `Assert.Equal(5, collection.Count)` |
| `action.Should().Throw<T>()` | `Assert.Throws<T>(action)` |
| `str.Should().Contain("x")` | `Assert.Contains("x", str)` |

## Test Structure Rules

- One test class per public behavior: `[MethodUnderTest]_[Behavior]_Tests`
- Tests must be independent (no shared state)
- One logical concern per test
- `[SetUp]` only for setup identical across ALL tests

## Reference Files

For detailed guides, see:

- `references/awesome-assertions-guide.md` — complete assertion catalog with chaining patterns
- `references/mutation-testing.md` — mutation testing with Stryker.NET
