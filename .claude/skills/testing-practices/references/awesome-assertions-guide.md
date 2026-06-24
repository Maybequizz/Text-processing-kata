# AwesomeAssertions Complete Guide

## Import
```csharp
using AwesomeAssertions;
```

## Assertion Catalog

| Scenario | Syntax |
|---|---|
| Equality | `result.Should().Be(expected)` |
| Inequality | `result.Should().NotBe(0)` |
| Null | `result.Should().BeNull()` |
| Not null | `result.Should().NotBeNull()` |
| Boolean true | `flag.Should().BeTrue()` |
| Boolean false | `flag.Should().BeFalse()` |
| String contains | `str.Should().Contain("substring")` |
| String starts | `str.Should().StartWith("prefix")` |
| String ends | `str.Should().EndWith("suffix")` |
| Collection count | `items.Should().HaveCount(5)` |
| Collection contains | `items.Should().Contain(item)` |
| Collection empty | `items.Should().BeEmpty()` |
| Exception type | `action.Should().Throw<ArgumentException>()` |
| Exception with message | `action.Should().Throw<Exception>().WithMessage("*partial*")` |
| Range check | `value.Should().BeInRange(1, 100)` |
| Type check | `result.Should().BeOfType<string>()` |

## Chaining

```csharp
result.Should()
    .Be(10)
    .And.BeGreaterThan(0)
    .And.BeLessThan(20);

str.Should().StartWith("Hello").And.EndWith("World");
```

## Mock Assertions (Moq)

```csharp
mock.Invocations.Should().HaveCount(1);
```
