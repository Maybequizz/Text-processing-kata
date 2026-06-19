# Workspace Agents

This repository defines workspace-specific custom agents for Test-Driven Development pairing sessions.

## Available agents

- `tdd-pairing`
  - A pairing agent for Test-Driven Development practice.
  - Leads kata sessions, asks validation questions in-line, and guides the red-green-refactor cycle.
  - Use when solving coding katas in C# with xUnit and TDD.

- `tdd-red`
  - The red phase subagent.
  - Writes a failing test first, validates kata hypotheses, and does not add production code.

- `tdd-green`
  - The green phase subagent.
  - Implements the minimum production code required to make the failing test pass.

- `tdd-refactor`
  - The refactor phase subagent.
  - Improves production and test code after the green phase while keeping all tests green.

## Location

Agent definitions are stored in `.github/agents/`:

- `.github/agents/tdd-pairing.agent.md`
- `.github/agents/tdd-red.agent.md`
- `.github/agents/tdd-green.agent.md`
- `.github/agents/tdd-refactor.agent.md`
