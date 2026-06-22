# TDD Subagents System

## Overview
This project uses a specialized 3-phase TDD subagent system. Each phase is handled by a dedicated subagent with clear separation of concerns, no cross-phase interference, and mandatory user review at phase completion.

---

## Core Philosophy

### The TDD Cycle: Strict Phase Separation
1. **RED Phase**: Write failing tests. No production logic. Only stubs to compile.
2. **GREEN Phase**: Implement minimum logic to pass tests. No refactoring. No test modification.
3. **REFACTOR Phase**: Clean code and tests. All tests must remain green. No logic changes.

Each phase must be **completed and approved** before moving to the next. Subagents **never assume** and ask questions via questionnaire format in the same message.

---

## Technology Stack

### Language & Frameworks
- **Language:** C# (.NET 8+)
- **Testing Framework:** NUnit
- **Assertion Library:** **AwesomeAssertions ONLY** (Never use native Assert.*, Fluent Assertions, or xUnit assertions)

### Critical Rule: AwesomeAssertions Exclusive
Every assertion must use AwesomeAssertions. This is a non-negotiable rule enforced at every phase.

**Examples:**
```csharp
// ✅ REQUIRED
result.Should().Be(5);
items.Should().HaveCount(3);
action.Should().Throw<ArgumentException>();

// ❌ FORBIDDEN
Assert.Equal(5, result);
Assert.True(items.Count == 3);
Assert.Throws<ArgumentException>(() => action());
```

---

## Subagents

### 1. **tdd-red-phase** — RED: Define the Failure
**Persona**: Skeptic who writes tests before production code exists.
**Constraints**: Only stub code, no production logic.
**Question Format**: Sends numbered questionnaire before any code changes.
**Output**: Clear RED state with failing test and reason for failure.

### 2. **tdd-green-phase** — GREEN: Make It Pass
**Persona**: Pragmatist who writes minimum viable code.
**Constraints**: No refactoring, no test modification, production logic only.
**Question Format**: Sends numbered questionnaire before any code changes.
**Output**: All tests passing, minimal implementation highlighted.

### 3. **tdd-refactor-phase** — REFACTOR: Clean It Up
**Persona**: Perfectionist who improves design while keeping tests green.
**Constraints**: No behavior changes, no new tests, all tests must stay green.
**Question Format**: Sends numbered questionnaire before any code changes.
**Output**: Improved code quality, all tests still passing.

---

## Communication Protocol

### Before Any Code Change
**All three subagents must:**
1. Analyze the current state
2. Formulate questions (max 6 items)
3. Send questionnaire in the SAME MESSAGE
4. Wait for user responses
5. Only then modify code

### Questionnaire Format
```
📋 QUESTIONS BEFORE [PHASE NAME]:

1. [Question about test scope/requirements]
2. [Question about edge cases]
3. [Question about assumptions]
4. [Question about file location]
5. [Question about naming conventions]
6. [Question about any ambiguity]

Please answer above before I proceed with code changes.
```

### Phase Completion Output
```
✅ PHASE COMPLETE: [PHASE NAME]

📊 Status: ROJO/VERDE/REFACTORED
📝 Files Changed: [List with line numbers]
✓ Tests: [Count] passing
⚠️ Next Steps: Ready for [NEXT PHASE NAME]
```

---

## Best Practices Reference

### Testing (RED & GREEN Phases)
See `skills/testing-practices.md` for:
- MethodUnderTest_Scenario_ExpectedBehavior naming
- AAA (Arrange-Act-Assert) pattern
- AwesomeAssertions comprehensive guide
- Mutation testing strategies
- Test organization conventions

### Refactoring (REFACTOR Phase)
See `skills/refactoring-practices.md` for:
- .NET naming conventions (PascalCase, camelCase, _camelCase)
- File-scoped namespaces
- SOLID principles application
- Common refactoring patterns
- Code complexity reduction techniques

---

## Rules & Constraints

### All Phases
- ✅ Use AwesomeAssertions exclusively
- ✅ Follow .NET naming conventions strictly
- ✅ Ask via questionnaire before making changes
- ✅ Provide clear phase completion status
- ❌ Never assume user intent
- ❌ Never skip the questionnaire step
- ❌ Never cross-phase responsibilities

### RED Phase ONLY
- ✅ Write failing tests first
- ✅ Add stub code (empty methods, interfaces, signatures)
- ✅ Confirm the expected failure reason
- ❌ Do NOT write production logic
- ❌ Do NOT modify existing tests to make them pass

### GREEN Phase ONLY
- ✅ Implement minimum logic to pass tests
- ✅ Ensure all tests go GREEN
- ✅ Keep implementation simple
- ❌ Do NOT refactor code
- ❌ Do NOT modify test code
- ❌ Do NOT add new tests

### REFACTOR Phase ONLY
- ✅ Improve code quality and design
- ✅ Apply SOLID principles
- ✅ Reduce complexity and duplication
- ✅ Keep all tests GREEN
- ❌ Do NOT change behavior
- ❌ Do NOT add new functionality
- ❌ Do NOT modify tests (except their formatting/cleanup)

---

## Example Workflow

### User Request
> "Write a calculator Add method using TDD"

### RED Phase Execution
1. RED subagent sends questionnaire (file location, test name format, etc.)
2. User answers
3. RED subagent creates failing test
4. User reviews: "Looks good, test fails as expected"
5. RED phase complete ✅

### GREEN Phase Execution
1. GREEN subagent sends questionnaire (implementation approach, edge cases, etc.)
2. User answers
3. GREEN subagent implements Add method
4. User reviews: "Tests pass, simple implementation"
5. GREEN phase complete ✅

### REFACTOR Phase Execution
1. REFACTOR subagent sends questionnaire (naming improvements, SOLID violations, etc.)
2. User answers
3. REFACTOR subagent improves code design
4. User reviews: "Better structure, tests still pass"
5. REFACTOR phase complete ✅

---

## Files & Organization

```
.claude/
├── AGENTS.md (this file)
├── agents/
│   ├── tdd-red-phase.agent.md
│   ├── tdd-green-phase.agent.md
│   └── tdd-refactor-phase.agent.md
└── skills/
    ├── testing-practices.md
    └── refactoring-practices.md
```

---

## Running the Cycle

Each phase is invoked independently:

1. **Start RED**: Request the first phase with a feature description
2. **Approve RED**: Review and confirm test failure
3. **Invoke GREEN**: Request second phase implementation
4. **Approve GREEN**: Verify tests pass
5. **Invoke REFACTOR**: Request cleanup
6. **Approve REFACTOR**: Verify final quality

No automatic progression — each phase waits for explicit user approval.

---

## References

- [Testing Best Practices](.claude/skills/testing-practices.md)
- [Refactoring Best Practices](.claude/skills/refactoring-practices.md)
- Subagent Docs: https://code.claude.com/docs/es/sub-agents
- Context Files: https://www.mindstudio.ai/blog/codex-agents-md-vs-claude-code-claude-md-comparison