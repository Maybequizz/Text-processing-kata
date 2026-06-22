# TDD Subagents System

## Overview
This project uses a specialized 3-phase TDD subagent system. Each phase is handled by a dedicated subagent with clear separation of concerns, no cross-phase interference, and mandatory user review at phase completion.

---

## Core Philosophy

### The TDD Cycle: Strict Phase Separation
1. **RED Phase**: Write failing tests. No production logic. Only stubs to compile.
2. **GREEN Phase**: Implement minimum logic to pass tests. No refactoring. No test modification.
3. **REFACTOR Phase**: Clean code and tests. All tests must remain green. No logic changes.

Each phase **auto-invokes the next** (no manual intervention). Subagents **never assume** and ask questions via questionnaire format **inline in the same message**.

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
**Communication**: Sends inline questionnaire, waits for answers, creates tests immediately.
**Handoff**: Auto-invokes GREEN phase via Task tool (no user approval).
**Output**: Clear RED state with failing tests and stub code.

### 2. **tdd-green-phase** — GREEN: Make It Pass
**Persona**: Pragmatist who writes minimum viable code.
**Constraints**: No refactoring, no test modification, production logic only.
**Communication**: Sends inline questionnaire, waits for answers, implements immediately.
**Handoff**: Auto-invokes REFACTOR phase via Task tool (no user approval).
**Output**: All tests passing, minimal implementation highlighted.

### 3. **tdd-refactor-phase** — REFACTOR: Clean It Up
**Persona**: Perfectionist who improves design while keeping tests green.
**Constraints**: No behavior changes, no new tests, all tests must stay green.
**Communication**: Sends inline questionnaire, waits for answers, refactors immediately.
**Handoff**: Reports completion (final phase - no further invocation).
**Output**: Improved code quality, all tests still passing, cycle complete.

---

## Communication Protocol

### Inline Questionnaire Pattern (CRITICAL)

**All three subagents must:**
1. Send questionnaire INLINE in their initial response (not separate message)
2. Ask numbered questions (max 6 items)
3. **Wait for user answers in the SAME message turn** (no new message needed)
4. Parse answers and proceed with code changes
5. After completion, auto-invoke next phase (or report if final)

### Questionnaire Format

```
📋 QUESTIONS BEFORE [PHASE NAME]:

1. [Question about requirements/approach]
2. [Question about specific details]
3. [Question about edge cases]
4. [Question about file location/naming]
5. [Question about priorities]
6. [Question about any ambiguity]

Please answer 1-6 in this same response, then I'll proceed.
```

### Phase Completion & Auto-Handoff

**RED → GREEN:**
```
✅ RED PHASE COMPLETE

📊 Status: FAILING (as expected)
📝 Files: TestProject1/TextProcessor_Analyze_Tests.cs
✓ Tests: 3 created and failing
⏭️ Invoking GREEN phase automatically...
```

Then uses Task tool:
```
@tdd-green-phase

Context: RED phase created 3 failing tests...
```

**GREEN → REFACTOR:** (Same pattern, GREEN auto-invokes REFACTOR)

**REFACTOR → DONE:** (Reports completion, no further invocation)

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

### User Invocation (Only One)
```
@tdd-red-phase

Write a calculator Add method using TDD.
Requirements: Add(5, 3) should return 8.
```

### RED Phase Execution
1. RED subagent responds with inline questionnaire
2. User answers questions 1-6 in **same message**
3. RED creates failing tests + stubs immediately
4. RED auto-invokes GREEN

### GREEN Phase Execution
1. GREEN receives context from RED via Task tool
2. GREEN responds with inline questionnaire
3. User answers questions 1-6 in **same message**
4. GREEN implements logic + auto-invokes REFACTOR

### REFACTOR Phase Execution
1. REFACTOR receives context from GREEN via Task tool
2. REFACTOR responds with inline questionnaire
3. User answers questions 1-6 in **same message**
4. REFACTOR improves code + reports completion

### Total Conversation Turns: 4
- Turn 1: User invokes RED + answers RED questions
- Turn 2: User answers GREEN questions
- Turn 3: User answers REFACTOR questions
- Done: Entire TDD cycle complete

---

## Running the TDD Cycle

**Automatic flow (no manual invocation needed):**

1. **User invokes RED**: `@tdd-red-phase [requirements]`
2. **RED sends inline questionnaire** → User answers (1-6)
3. **RED creates tests** → **RED auto-invokes GREEN**
4. **GREEN sends inline questionnaire** → User answers (1-6)
5. **GREEN implements** → **GREEN auto-invokes REFACTOR**
6. **REFACTOR sends inline questionnaire** → User answers (1-6)
7. **REFACTOR improves code** → **Reports completion**

**Total user interactions: 4 messages**
- Message 1: Invoke RED + answer RED questions
- Message 2: Answer GREEN questions
- Message 3: Answer REFACTOR questions
- Done!

No need to manually invoke phases, they call each other automatically.

---

## Files & Organization

```
.claude/
├── README.md (Quick start guide for humans)
├── AGENTS.md (Technical reference - source of truth for agents)
├── agents/
│   ├── tdd-red-phase.agent.md (RED phase agent)
│   ├── tdd-green-phase.agent.md (GREEN phase agent)
│   └── tdd-refactor-phase.agent.md (REFACTOR phase agent)
└── skills/
    ├── testing-practices.md (Domain knowledge for testing)
    └── refactoring-practices.md (Domain knowledge for refactoring)
```

**Information Flow:**
- README.md → Entry point for humans
- AGENTS.md → Loaded by all agents as context
- Individual agent .md files → Specific execution instructions
- Skills → Domain-specific knowledge and examples

---

## Running the TDD Cycle

Each phase is invoked independently and must be approved before moving to the next:

1. **Start RED**: Request the first phase with a feature description
2. **Approve RED**: Review and confirm test failure
3. **Invoke GREEN**: Request second phase implementation
4. **Approve GREEN**: Verify tests pass
5. **Invoke REFACTOR**: Request cleanup
6. **Approve REFACTOR**: Verify final quality

No automatic progression — each phase waits for **explicit user approval**.

---

## How Agents Load This Context

### Automatic Context Loading (No Configuration Needed)
When each subagent starts, Claude Code **automatically loads**:
1. ✅ This `AGENTS.md` file as contextual reference
2. ✅ Each agent's individual skill files (preloaded via frontmatter)
3. ✅ Git repository state
4. ✅ Project configuration

### What Each Agent Receives at Startup
```
tdd-red-phase        →  System Prompt (from agent markdown body)
                     +  AGENTS.md (this file - shared context)
                     +  skills/testing-practices.md (preloaded)
                     
tdd-green-phase      →  System Prompt (from agent markdown body)
                     +  AGENTS.md (this file - shared context)
                     +  skills/testing-practices.md (preloaded)
                     
tdd-refactor-phase   →  System Prompt (from agent markdown body)
                     +  AGENTS.md (this file - shared context)
                     +  skills/refactoring-practices.md (preloaded)
                     +  skills/testing-practices.md (preloaded)
```

### How This Works in Practice
1. **System Loads Agent**: User invokes `@tdd-red-phase` [task]
2. **Context Assembly**: Claude Code loads AGENTS.md + skills + agent prompt
3. **Agent Executes**: Agent has complete context to understand system
4. **No Redundancy**: Agent's individual .md file has execution logic, not system philosophy

---

## Information Architecture

### For Humans Getting Started
→ **Read**: `.claude/README.md` (quick start, examples, navigation)

### For Humans Deep Dive
→ **Read**: `AGENTS.md` (this file - complete technical specification)

### For Domain Knowledge (Humans & Agents)
→ **Read**: `skills/testing-practices.md` (testing tactics, examples)
→ **Read**: `skills/refactoring-practices.md` (refactoring tactics, examples)

### For Agents (Automatic)
- ✅ Agents automatically load `AGENTS.md` for system philosophy
- ✅ Agents load their specific skills (testing or refactoring)
- ✅ Agents execute their own .md file system prompt
- ❌ Agents do NOT read README.md (human-only documentation)

---

## Single Source of Truth Policy

This document (`AGENTS.md`) is the authoritative specification:
- ✅ **README.md**: References AGENTS.md, provides entry point
- ✅ **Skills files**: Contain domain-specific tactics, not system rules
- ✅ **Agent .md files**: Contain execution logic, reference AGENTS.md
- ❌ **NO duplication**: System rules exist only in one place
- ❌ **NO conflicting information**: All agents follow same spec

---

## References

- [Quick Start Guide](README.md) — For humans new to the system
- [Testing Best Practices](skills/testing-practices.md) — Domain knowledge for RED & GREEN
- [Refactoring Best Practices](skills/refactoring-practices.md) — Domain knowledge for REFACTOR
- Claude Code Subagents: https://code.claude.com/docs/es/sub-agents
- Context Files Comparison: https://www.mindstudio.ai/blog/codex-agents-md-vs-claude-code-claude-md-comparison