---
name: refactoring-practices
description: |
  .NET refactoring conventions and SOLID principles.
  Use when cleaning up implementation code after tests pass (REFACTOR phase).
  Trigger phrases: "refactor", "improve code", "clean up", "extract method", "apply SOLID".
  Enforces file-scoped namespaces, PascalCase/camelCase conventions, method extraction, and DRY.
  Not for: writing tests, adding new features, changing behavior.
allowed-tools: "Read, Write, Edit, Glob, Grep, Bash"
version: 2.0.0
---

# Refactoring Practices

## Core Principles

1. **All tests must stay GREEN** after every change
2. **One atomic change at a time** — run tests after each
3. **Never change behavior** — only structure and readability
4. **DRY** — eliminate duplication
5. **SOLID** — single responsibility per method/class

## Naming Conventions

| Element | Convention | Example |
|---|---|---|
| Classes | PascalCase | `TextProcessor` |
| Methods | PascalCase | `Analyze()` |
| Properties | PascalCase | `TotalWords` |
| Local vars | camelCase | `wordCount` |
| Private fields | `_camelCase` | `_repository` |
| Constants | UPPER_SNAKE | `MAX_RETRIES` |
| Async methods | Suffix `Async` | `SaveAsync()` |

## Key Refactoring Patterns

| Pattern | When | How |
|---|---|---|
| Extract Method | Method >20 lines, multiple responsibilities | Split into focused private methods |
| Extract Variable | Complex expression, magic number | Name the intent |
| Guard Clauses | Deep nesting (>3 levels) | Early return for invalid cases |
| Replace Loop with LINQ | Simple filter/map/group | `collection.Where(x => x.IsActive)` |
| Introduce Interface | Tight coupling, needs DI | Extract interface, inject abstraction |

## Progressive Disclosure

For detailed references, see:

- `references/solid-reference.md` — SOLID principles with before/after examples
- `references/refactoring-patterns.md` — complete catalog with code samples
