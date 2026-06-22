# TDD Subagents System

Welcome to the Test-Driven Development subagent system for this project.

## 🚀 Quick Start

This project uses **3 specialized subagents** to manage the TDD cycle. Each agent handles ONE phase with clear separation of concerns.

### The TDD Cycle

```
1. 🔴 RED:     Write tests that fail
2. 🟢 GREEN:   Implement minimum code to pass tests
3. 🔵 REFACTOR: Improve code while keeping tests green
```

### How to Use

Invoke each phase with `@mention`:

```
@tdd-red-phase Create a test for Calculator.Add(a, b) that verifies Add(5, 3) = 8

[Agent responds with questionnaire → You answer → Agent creates failing test]

@tdd-green-phase Implement Add method to pass the test

[Agent responds with questionnaire → You answer → Agent implements]

@tdd-refactor-phase Improve the Calculator code structure

[Agent responds with questionnaire → You answer → Agent refactors]
```

## 📚 Documentation

- **`AGENTS.md`** — Complete technical reference for the system
  - Phases and responsibilities
  - Communication protocol
  - Rules and constraints
  - Best practices

- **`skills/testing-practices.md`** — Testing guide
  - Test naming: `MethodUnderTest_Scenario_ExpectedBehavior`
  - AAA pattern (Arrange-Act-Assert)
  - AwesomeAssertions (ONLY assertion library allowed)
  - Test organization and mutation testing

- **`skills/refactoring-practices.md`** — Refactoring guide
  - .NET naming conventions
  - SOLID principles
  - Common refactoring patterns
  - Code quality standards

## ⚡ Key Rules

### 1. AwesomeAssertions Only
```csharp
// ✅ REQUIRED
result.Should().Be(5);
items.Should().HaveCount(3);
action.Should().Throw<ArgumentException>();

// ❌ FORBIDDEN
Assert.Equal(5, result);
Assert.True(items.Count == 3);
```

### 2. Questionnaire Protocol
**Before ANY code changes**, each agent sends 6 questions and waits for your answers.

### 3. Phase Separation
- **RED**: Only tests + stubs (no production logic)
- **GREEN**: Only implementation (no refactoring)
- **REFACTOR**: Only code improvement (no new features)

### 4. User Approval
Each phase must be **explicitly approved** before moving to the next.

## 📋 Example Workflow

### Red Phase
```
You: @tdd-red-phase Write a test for Calculate.Add method

Agent: 📋 RED PHASE QUESTIONS:
1. Where should the test class go?
2. What's the test class name?
3. What specific test scenario?
4. What inputs/outputs?
5. Where should stubs go?
6. Any edge cases?

You: [Answer questions]

Agent: ✅ PHASE COMPLETE: RED
Test created: Add_WithPositiveNumbers_ReturnsSum
Status: ❌ FAILING (as expected)
Ready for GREEN phase
```

### Green Phase
```
You: @tdd-green-phase Implement Add to pass the test

Agent: 📋 GREEN PHASE QUESTIONS:
[6 questions about implementation approach]

You: [Answer questions]

Agent: ✅ PHASE COMPLETE: GREEN
Implementation: public int Add(int a, int b) => a + b;
Status: ✓ ALL TESTS PASSING
Ready for REFACTOR phase
```

### Refactor Phase
```
You: @tdd-refactor-phase Improve code quality

Agent: 📋 REFACTOR PHASE QUESTIONS:
[6 questions about refactoring priorities]

You: [Answer questions]

Agent: ✅ PHASE COMPLETE: REFACTOR
Improvements applied: [list of changes]
Status: ✓ ALL TESTS STILL PASSING
Feature complete
```

## 🤖 Subagents

| Agent | Phase | Responsibility |
|:---|:---|:---|
| `tdd-red-phase` | 🔴 RED | Write failing tests + stubs |
| `tdd-green-phase` | 🟢 GREEN | Implement minimum logic |
| `tdd-refactor-phase` | 🔵 REFACTOR | Improve code quality |

## 📁 System Structure

```
.claude/
├── README.md (you are here)
├── AGENTS.md (technical reference)
├── agents/
│   ├── tdd-red-phase.agent.md
│   ├── tdd-green-phase.agent.md
│   └── tdd-refactor-phase.agent.md
└── skills/
    ├── testing-practices.md
    └── refactoring-practices.md
```

## ✅ What Each Agent Does

### RED Phase (tdd-red-phase)
- ✅ Writes test classes following `MethodUnderTest_Scenario_ExpectedBehavior` naming
- ✅ Uses AwesomeAssertions exclusively
- ✅ Creates AAA pattern tests (Arrange-Act-Assert)
- ✅ Adds minimal stub code for compilation
- ✅ Confirms tests fail for the right reason
- ❌ Does NOT write production logic

### GREEN Phase (tdd-green-phase)
- ✅ Implements minimum viable code
- ✅ Makes ALL tests pass (GREEN state)
- ✅ Keeps implementation simple and direct
- ✅ Verifies test assertions are satisfied
- ❌ Does NOT refactor code
- ❌ Does NOT modify tests

### REFACTOR Phase (tdd-refactor-phase)
- ✅ Applies SOLID principles
- ✅ Extracts methods to reduce complexity
- ✅ Improves naming and formatting
- ✅ Replaces loops with LINQ
- ✅ Introduces interfaces for DI
- ✅ Keeps ALL tests passing
- ❌ Does NOT change behavior
- ❌ Does NOT add new functionality

## 🎓 Best Practices Included

### Testing
- Naming convention: `MethodUnderTest_Scenario_ExpectedBehavior`
- AAA Pattern with explicit comments
- AwesomeAssertions comprehensive guide
- Test organization by feature
- Mutation testing strategies

### Refactoring
- .NET naming conventions (PascalCase, camelCase, _camelCase)
- File-scoped namespaces (modern .NET)
- SOLID principles (SRP, OCP, LSP, ISP, DIP)
- Common refactoring patterns
- Code complexity reduction

## 📖 Next Steps

1. Read `AGENTS.md` for complete technical details
2. Use `@tdd-red-phase` to start a feature
3. Answer questionnaires before code changes
4. Review each phase's output
5. Approve before moving to next phase

---

**Questions?** See `AGENTS.md` for comprehensive reference or individual skill files for specific topics.
