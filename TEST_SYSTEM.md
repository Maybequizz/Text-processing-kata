# Testing the Redesigned TDD System

This document guides you through the OPTIMIZED 3-phase TDD system with inline questionnaires and automatic agent handoff.

---

## Architecture Overview (Redesigned)

### What Changed

Previously: User → RED → (wait for approval) → User → GREEN → (wait for approval) → User → REFACTOR  
**Now**: User → RED (inline Q&A) → GREEN (auto-invoke) (inline Q&A) → REFACTOR (auto-invoke) (inline Q&A) → Done

**Benefits:**
- ✅ **Single message per phase**: You respond once with 6 answers
- ✅ **Automatic handoff**: Agents call each other automatically
- ✅ **Massive token savings**: No context re-loading between phases
- ✅ **No manual invocation**: Forget about @mentions after RED
- ✅ **Same conversation context**: All phases in single thread

---

## Kata Overview

**Text Processing Kata**
- Analyze blog post text for most common words and character count
- Kata file: `Text Processing/kata.txt`
- Test project: `TestProject1` (NUnit + AwesomeAssertions)

---

## Phase 1: RED - Write Failing Tests

### How to Invoke (Only User Action)

In your OpenCode terminal, send this ONCE:

```
@tdd-red-phase

Kata: Text Processing (blog post analyzer)

Requirements:
1. Create TextProcessor class that analyzes text
2. Should find top 10 most common words (case-insensitive)
3. Should count total words in text
4. Words with same frequency don't need alphabetical order
5. Use test case: "Hello, this is an example for you to practice. You should grab this text and make it as your test case."

Expected output: 
- Top words: you, this, your, to, text, test, should, practice, make, it
- Total words: 21

Please help me write failing tests for these requirements.
```

### What RED Will Do

1. **Respond with inline questionnaire**:
   ```
   📋 QUESTIONS BEFORE RED PHASE:
   
   1. Test Location: Where should test class be created?
   2. Test Class Name: Name following pattern?
   3. Test Methods: What scenarios to test?
   4. Input/Output Examples: Concrete test data?
   5. Stub Location: Where should TextProcessor.cs live?
   6. Edge Cases: Null, empty, special chars?
   
   Please answer 1-6 in this SAME response.
   ```

2. **You respond with answers** (in the same message thread):
   ```
   1. TestProject1/TextProcessor_Analyze_Tests.cs
   2. Analyze_WithTextAnalysis_Tests
   3. Test top 10 words extraction, word count accuracy, case-insensitive matching
   4. Input: "Hello, this is..." → 21 words, top word "you" appears 2x
   5. Text Processing/TextProcessor.cs
   6. Edge cases: empty string, null input, single word
   ```

3. **RED immediately creates tests and stubs**:
   - Creates test file with 3-4 failing tests
   - All tests use AwesomeAssertions
   - Creates TextProcessor.cs with empty Analyze method
   - Runs tests, verifies they FAIL
   - Shows status:
   ```
   ✅ RED PHASE COMPLETE
   
   📊 Tests: 3 created and FAILING (expected)
   📝 Stubs: TextProcessor.cs with empty Analyze method
   ⏭️ Invoking GREEN phase automatically...
   ```

4. **GREEN phase auto-invokes** (no user action needed)

---

## Phase 2: GREEN - Implement Minimum Logic

### How It's Invoked (Automatic)

RED phase automatically calls GREEN with context.

### What GREEN Will Do

1. **Respond with inline questionnaire**:
   ```
   📋 QUESTIONS BEFORE GREEN PHASE:
   
   1. Data Structure: Dictionary or List for word frequencies?
   2. Parsing: LINQ Split or manual parsing?
   3. Case Handling: Convert to lowercase?
   4. Punctuation: Use Regex to remove?
   5. Top 10: OrderByDescending + Take(10)?
   6. Edge Cases: Return empty result for null/empty?
   
   Please answer 1-6 in this SAME response.
   ```

2. **You respond with answers**:
   ```
   1. Dictionary<string, int> for frequencies
   2. Split() with StringSplitOptions.RemoveEmptyEntries
   3. Yes, convert to lowercase
   4. Yes, simple regex for punctuation
   5. Yes, OrderByDescending + Take(10)
   6. Return empty AnalysisResult for null/empty
   ```

3. **GREEN immediately implements**:
   - Writes TextProcessor.Analyze method
   - Implementation is SIMPLE (no refactoring, optimization OK is fine)
   - Runs tests to verify ALL PASS
   - Shows status:
   ```
   ✅ GREEN PHASE COMPLETE
   
   📊 Tests: 3 PASSING (all green)
   📝 Implementation: TextProcessor.Analyze method
   ⏭️ Invoking REFACTOR phase automatically...
   ```

4. **REFACTOR phase auto-invokes** (no user action needed)

---

## Phase 3: REFACTOR - Improve Code Quality

### How It's Invoked (Automatic)

GREEN phase automatically calls REFACTOR with context.

### What REFACTOR Will Do

1. **Respond with inline questionnaire**:
   ```
   📋 QUESTIONS BEFORE REFACTOR PHASE:
   
   1. Method Extraction: Split into separate methods?
   2. Naming: Improve variable names (x → wordEntry)?
   3. SOLID: Create separate classes for parsing/sorting?
   4. Duplication: Any repeated patterns to eliminate?
   5. Validation: Add null checks or validation methods?
   6. Documentation: Add XML comments?
   
   Please answer 1-6 in this SAME response.
   ```

2. **You respond with answers**:
   ```
   1. Yes, extract ExtractWords, CalculateFrequencies, GetTop10
   2. Yes, rename variables for clarity
   3. No, keep in single class for now
   4. Remove duplicate punctuation removal
   5. Yes, add validation method
   6. Yes, add XML documentation
   ```

3. **REFACTOR immediately improves code**:
   - Extracts methods for clarity
   - Improves naming throughout
   - Applies SOLID principles if requested
   - Runs tests to verify ALL still PASS
   - Shows completion status:
   ```
   ✅ REFACTOR PHASE COMPLETE
   
   📊 Improvements:
      ✓ Extracted 3 methods
      ✓ Improved variable naming
      ✓ Added XML documentation
      ✓ Tests: 3 PASSING
   
   🎉 TDD CYCLE COMPLETE!
      RED      ✅ Failing tests written
      GREEN    ✅ Logic implemented
      REFACTOR ✅ Code improved
   ```

---

## Complete Flow (One Example Session)

```
USER:
@tdd-red-phase [send kata requirements]

RED:
📋 QUESTIONS BEFORE RED PHASE:
1. Test location?
2. Test class name?
3. Test scenarios?
4. Input/output examples?
5. Stub location?
6. Edge cases?

USER (same response):
1. TestProject1/TextProcessor_Analyze_Tests.cs
2. Analyze_WithTextAnalysis_Tests
3. [answers 3-6]

RED: [creates tests, stubs, runs, verifies failure]
✅ RED COMPLETE
⏭️ Invoking GREEN...

GREEN:
📋 QUESTIONS BEFORE GREEN PHASE:
1. Data structure?
2. Parsing approach?
3. Case handling?
4. Punctuation removal?
5. Top 10 selection?
6. Edge cases?

USER (same response):
1. Dictionary<string, int>
2. Split() method
3. [answers 4-6]

GREEN: [implements logic, runs tests, verifies all pass]
✅ GREEN COMPLETE
⏭️ Invoking REFACTOR...

REFACTOR:
📋 QUESTIONS BEFORE REFACTOR PHASE:
1. Method extraction?
2. Naming improvements?
3. SOLID principles?
4. Duplication removal?
5. Validation methods?
6. Documentation?

USER (same response):
1. Yes, extract 3 methods
2. Yes, rename variables
3. [answers 5-6]

REFACTOR: [improves code, runs tests, verifies still green]
✅ REFACTOR COMPLETE
🎉 TDD CYCLE DONE!
```

---

## Key Improvements Over Previous Version

### ✅ Token Efficiency
- **Before**: 3 messages per phase (question, answer, implementation) = 9 messages
- **Now**: 2 messages per phase (inline question + implementation) = 6 messages
- **Savings**: 33% fewer messages, massive context reduction

### ✅ Automatic Handoff
- **Before**: Manual `@tdd-green-phase` invocation needed
- **Now**: RED automatically invokes GREEN, GREEN invokes REFACTOR
- **Result**: True end-to-end flow without user intervention

### ✅ Single Conversation Context
- **Before**: Multiple separate conversations, context loss between phases
- **Now**: All phases in single thread, full context maintained
- **Result**: Agents have complete history, better decisions

### ✅ Same-Message Q&A
- **Before**: Questionnaire → user provides answer in new message
- **Now**: Questionnaire inline → user responds in same turn
- **Result**: More natural flow, no artificial delays

---

## File Structure After Completion

```
TestProject1/
├── TextProcessor_Analyze_Tests.cs          (created in RED)
├── TestProject1.csproj

Text Processing/
├── TextProcessor.cs                        (created in RED, implemented in GREEN, refactored in REFACTOR)
├── Text Processing.csproj
├── kata.txt
└── Program.cs
```

---

## Troubleshooting

### "Agent didn't send questionnaire inline"
- Check agent's system prompt loads correctly
- Verify agent prompt contains "inline questionnaire" section
- If still broken: send questionnaire as separate message manually

### "Tests don't pass after GREEN"
- GREEN implementation might be incomplete
- Ask GREEN to debug with specific error message
- Don't move to REFACTOR until all tests pass

### "REFACTOR changed behavior"
- Tests should catch this immediately
- If tests fail in REFACTOR, it means behavior changed (shouldn't happen)
- Ask REFACTOR to revert and use different approach

### "Agents aren't calling each other"
- Verify agents have Task tool enabled in frontmatter
- Check AGENTS.md is loaded by each agent
- If still broken: manually invoke next phase with `@agent-name`

---

## Rules to Remember

### RED Phase
- ✅ Inline questionnaire before creating tests
- ✅ Tests must FAIL
- ✅ Uses AwesomeAssertions ONLY
- ✅ Stubs created, no logic
- ✅ Auto-invokes GREEN

### GREEN Phase
- ✅ Inline questionnaire before implementing
- ✅ Tests must PASS
- ✅ Implementation is simple, no refactoring
- ✅ No behavior changes
- ✅ Auto-invokes REFACTOR

### REFACTOR Phase
- ✅ Inline questionnaire before refactoring
- ✅ Tests must stay PASSING
- ✅ Code improved, no behavior changes
- ✅ Extraction, naming, SOLID applied
- ✅ Reports completion

### ALL Phases
- ✅ Questionnaire is INLINE (no new message needed)
- ✅ User responds with numbered answers (1-6)
- ✅ Agents proceed immediately after answers
- ✅ Auto-handoff to next phase
- ✅ No manual approval needed

---

## Next Steps

1. Copy kata requirements into OpenCode
2. Send `@tdd-red-phase [requirements]` message
3. RED sends inline questionnaire
4. You answer questions 1-6 in same message
5. RED creates tests, invokes GREEN
6. GREEN sends inline questionnaire
7. You answer questions 1-6 in same message
8. GREEN implements, invokes REFACTOR
9. REFACTOR sends inline questionnaire
10. You answer questions 1-6 in same message
11. REFACTOR improves, reports completion
12. ✅ Done! Full cycle complete in ~4 conversation turns total

Good luck!
