# Testing the TDD Subagent System

This document guides you through a complete test of the 3-phase TDD system using the **Text Processing Kata**.

---

## Kata Overview

**Text Processing Kata**
- Analyze blog post text to find most common words and character count
- Kata file: `Text Processing/kata.txt`
- Test project: `TestProject1` (already configured with NUnit + AwesomeAssertions)

---

## Phase 1: RED - Write Failing Tests

**Goal**: Write tests that define the expected behavior BEFORE implementation exists.

### How to Invoke

In your OpenCode terminal, type:

```
@tdd-red-phase

Kata: Text Processing (blog post analyzer)

Requirements from kata.txt:
1. Create a Processor interface/class that analyzes text
2. Should find top 10 most common words (case-insensitive)
3. Should count total words in text
4. Words with same frequency don't need alphabetical order
5. Use test case: "Hello, this is an example for you to practice. You should grab this text and make it as your test case."

Please write failing tests for these requirements. Use the first test case from the kata.
```

### What RED Should Do

1. ✅ Send 6-question questionnaire about:
   - Test class naming and location
   - Test method naming pattern (MethodUnderTest_Scenario_Expected)
   - Should we create interface or class first?
   - Edge cases to test
   - File organization
   - Any assumptions

2. ✅ Wait for your answers

3. ✅ Create test file with failing tests:
   - Tests for top 10 words extraction
   - Tests for word count
   - Tests for case-insensitivity
   - All tests should FAIL (red state)

4. ✅ Show output with:
   - List of files created
   - Count of failing tests
   - Reason for failure (production code doesn't exist yet)

### Expected Output Format

```
📋 QUESTIONS BEFORE RED PHASE:

1. Where should test file be created? (e.g., TestProject1/TextProcessorTests.cs)
2. Should we create the Processor class or interface first?
3. [4 more questions]

Please answer above before I proceed with code changes.
```

### Approve RED Phase

Once RED phase is complete and you've reviewed the failing tests, respond:

```
Tests look good. RED phase approved. Ready for GREEN phase.
```

---

## Phase 2: GREEN - Implement Minimum Logic

**Goal**: Write ONLY the minimum code needed to make tests pass. NO refactoring.

### How to Invoke

After RED is approved:

```
@tdd-green-phase

The RED phase created failing tests. Now implement the minimum logic to make all tests pass.

Do NOT:
- Refactor code
- Improve naming
- Reorganize files
- Modify any tests

Just make them pass with the simplest possible implementation.
```

### What GREEN Should Do

1. ✅ Send 6-question questionnaire about:
   - Implementation approach
   - Should we use LINQ or loops?
   - Dictionary or List for word frequency?
   - How to handle punctuation?
   - Any shortcuts to simplify?
   - Edge case handling

2. ✅ Wait for your answers

3. ✅ Create/implement Processor class:
   - Analyze method to process text
   - Returns word frequency data
   - Minimum viable implementation (might look ugly, that's OK!)

4. ✅ Show output with:
   - List of files modified
   - Count of passing tests
   - Confirmation all tests pass
   - Note: Code may look rough (REFACTOR phase fixes this)

### Approve GREEN Phase

Once tests pass:

```
All tests passing. GREEN phase approved. Ready for REFACTOR phase.
```

---

## Phase 3: REFACTOR - Improve Code Quality

**Goal**: Clean up code while keeping all tests GREEN. NO behavior changes.

### How to Invoke

After GREEN is approved:

```
@tdd-refactor-phase

All tests are passing. Now refactor the code to improve quality:

- Apply SOLID principles
- Improve naming
- Reduce duplication
- Better separation of concerns
- Cleaner code structure

Keep all tests GREEN. No behavior changes.
```

### What REFACTOR Should Do

1. ✅ Send 6-question questionnaire about:
   - Naming improvements needed?
   - SOLID principle violations?
   - Code duplication to eliminate?
   - Performance improvements?
   - Test organization improvements?
   - Any method extraction needed?

2. ✅ Wait for your answers

3. ✅ Refactor code:
   - Improve naming (PascalCase, camelCase)
   - Extract methods
   - Apply SOLID principles
   - Reorganize for clarity
   - Update tests formatting (not logic)

4. ✅ Show output with:
   - List of files modified
   - Improvements made
   - Confirmation all tests still pass
   - Summary of quality improvements

### Approve REFACTOR Phase

Once refactoring is complete:

```
Code looks much better. REFACTOR phase approved. Cycle complete!
```

---

## Complete Cycle Summary

After all three phases:

```
✅ RED PHASE:    Tests written, failing
✅ GREEN PHASE:  Logic implemented, tests passing
✅ REFACTOR:     Code improved, tests still passing

🎉 Text Processing kata complete!
```

---

## File Structure After Completion

```
TestProject1/
├── TextProcessorTests.cs          (Test file created in RED phase)
├── TestProject1.csproj            (Already configured)

Text Processing/
├── Processor.cs                   (or similar - created in GREEN phase)
├── Text Processing.csproj
├── kata.txt
```

---

## Key Rules to Remember

### RED Phase
- ❌ NO production logic
- ✅ Only stub code to compile
- ✅ Tests must FAIL
- ✅ Send questionnaire first

### GREEN Phase
- ❌ NO refactoring
- ❌ NO test modification
- ✅ Minimum viable implementation
- ✅ Tests must PASS

### REFACTOR Phase
- ❌ NO new tests
- ❌ NO behavior changes
- ✅ Code quality improvements
- ✅ Tests must stay GREEN

### ALL Phases
- ✅ Use AwesomeAssertions exclusively
- ✅ Send questionnaire before changes
- ✅ Wait for user approval
- ✅ Clear phase completion status

---

## Troubleshooting

### "Agent didn't ask questions"
- Copy/paste the full kata requirements in your prompt
- Ask explicitly: "Send questionnaire BEFORE making changes"
- Check agent loads AGENTS.md context correctly

### "Tests don't use AwesomeAssertions"
- This is critical - AwesomeAssertions are required
- Check if agent loaded `testing-practices.md` skill
- Look at that skill file for proper syntax

### "Agent made changes across phases"
- This violates phase separation
- Read the agent's system prompt in `.claude/agents/`
- Ensure questionnaire was sent first

### "Tests won't compile"
- Check project references in .csproj
- Verify NUnit and AwesomeAssertions packages installed
- Run: `dotnet restore` and `dotnet build`

---

## Next Steps

1. Open your OpenCode terminal
2. Navigate to this project
3. Start with RED phase invitation above
4. Follow the cycle through all three phases
5. Verify the kata is complete with clean code and passing tests

Good luck!
