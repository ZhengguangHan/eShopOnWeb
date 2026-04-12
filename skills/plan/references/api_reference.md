# Plan Reference

Use this reference when authoring the saved plan file.

The plan must be TDD-first. Reuse the wording and execution order from `skills/test-driven-development/SKILL.md` as closely as possible.

## Required Output Path

Save every plan to:

`docs/plans/YYYY-MM-DD-{summary}.md`

Use a short kebab-case summary for `{summary}`.

## Core Principle

Write the test first. Watch it fail. Write minimal code to pass.

**Core principle:** If you didn't watch the test fail, you don't know if it tests the right thing.

**Violating the letter of the rules is violating the spirit of the rules.**

## The Iron Law

```text
NO PRODUCTION CODE WITHOUT A FAILING TEST FIRST
```

Every non-exempt implementation task in the plan must enforce this order.

Write code before the test? Delete it. Start over.

### No Exceptions

- Don't keep it as "reference"
- Don't "adapt" it while writing tests
- Don't look at it
- Delete means delete

Implement fresh from tests. Period.

### Exceptions (Ask Your Human Partner)

- Throwaway prototypes
- Generated code
- Configuration files

Thinking "skip TDD just this once"? Stop. That's rationalization.

## Required Plan Document Structure

```markdown
# [Feature Name] Implementation Plan

> **For Agent:** Execute this plan task-by-task. Follow each step exactly, verify test results before proceeding, and commit after each task.
> **TDD Rule:** No production code without a failing test first.

**Goal:** [One sentence describing what this builds]
**Architecture:** [2-3 sentences about approach]
**Tech Stack:** [Key technologies/libraries]
**Complexity Path:** `E2E path` | `Simplified TDD path`
**Status:** Draft | In Progress | Complete

---

## Requirements
### User Stories
- As a [role], I want [capability], so that [outcome].

### Acceptance Criteria
- Given [context], when [action], then [outcome].

### Assumptions, Constraints, and Scope Boundaries
- [Explicit assumptions and limits]

## Architecture Review
- Reusable components and similar implementations
- Affected layers and key data flow
- Mermaid user journey
- Exact file paths that will likely change

## Implementation Steps

### Phase 1: [Phase Name]

#### Task 1: [Behavior or Component Name]
**Goal:** [One sentence describing the behavior this task proves]

**Files:**
- Create: `exact/path/to/file.ext`
- Modify: `exact/path/to/existing.ext`
- Test: `tests/or/specs/exact/path/to/test.ext`

**RED - Write Failing Test**
[Complete test code]

**Requirements:**
- One behavior
- Clear name
- Real code (no mocks unless unavoidable)

**Verify RED - Watch It Fail**
Run: `[exact test command]`

Confirm:
- Test fails (not errors)
- Failure message is expected
- Fails because feature missing (not typos)

**Test passes?** You're testing existing behavior. Fix test.

**Test errors?** Fix error, re-run until it fails correctly.

**GREEN - Minimal Code**
[Complete minimal implementation code]

Don't add features, refactor other code, or "improve" beyond the test.

**Verify GREEN - Watch It Pass**
Run: `[exact test command]`

Confirm:
- Test passes
- Other tests still pass
- Output pristine (no errors, warnings)

**Test fails?** Fix code, not test.

**Other tests fail?** Fix now.

**REFACTOR - Clean Up**
- Remove duplication
- Improve names
- Extract helpers

Keep tests green. Don't add behavior.

**Verify GREEN - Stay Green After Refactor**
Run: `[exact test command]`

Confirm:
- Refactor keeps tests green
- Other tests still pass
- Output pristine (no errors, warnings)

**COMMIT**
Run:
`git commit -m "<type>[optional scope]: <emoji> <description>"`

### Phase 2: [Phase Name]
...

## Testing Strategy
- Unit tests: [function-level coverage]
- Integration tests: [repository/database-level coverage]
- E2E tests: [user story journeys]

## Risks & Mitigations
- **Risk**: [Description] -> Mitigation: [How to address]

## Success Criteria
- [ ] Criterion 1
- [ ] Criterion 2
```

## Required Task Contract

Every non-exempt implementation task must use this exact order:

`RED -> Verify RED -> GREEN -> Verify GREEN -> REFACTOR -> Verify GREEN -> COMMIT`

Do not collapse or rename these sections. Do not skip the verification steps. Do not move commit before refactor verification.

### Approved Exception Contract

If the user explicitly approves an exception for throwaway prototype work, generated code, or configuration-only work, do not fake TDD steps. Instead, replace the implementation task with this exact exception block:

```markdown
#### Task [N]: [Task Name]
**Exception Type:** Throwaway prototype | Generated code | Configuration-only
**User Approval:** [Quote or summarize the approval]
**Files:**
- Modify: `exact/path/to/file.ext`

**Implementation**
[Exact change to make]

**Verification**
Run: `[exact verification command]`

Confirm:
- Expected result is observed
- Relevant build, lint, or test checks still pass
- Output pristine (no errors, warnings)

**COMMIT**
Run:
`git commit -m "<type>[optional scope]: <emoji> <description>"`
```

Use this exception contract only when the user has explicitly approved it.

## Task Authoring Rules

For every implementation task in the plan, include:

- Exact file paths
- Complete code, not summaries
- Exact commands
- Expected failure or pass conditions
- Minimal implementation guidance
- Commit message text

Steps must be bite-sized and take roughly 2-5 minutes each.

## Why Order Matters

### "I'll Write Tests After To Verify It Works"

Tests written after code pass immediately. Passing immediately proves nothing:

- Might test wrong thing
- Might test implementation, not behavior
- Might miss edge cases you forgot
- You never saw it catch the bug

Test-first forces you to see the test fail, proving it actually tests something.

### "Tests After Achieve The Same Goals - It's Spirit Not Ritual"

No. Tests-after answer "What does this do?" Tests-first answer "What should this do?"

Tests-after are biased by your implementation. You test what you built, not what's required. You verify remembered edge cases, not discovered ones.

Tests-first force edge case discovery before implementing. Tests-after verify you remembered everything (you didn't).

30 minutes of tests after != TDD. You get coverage, lose proof tests work.

## Common Rationalizations

- `"Too simple to test"` -> Simple code breaks. Test takes 30 seconds.
- `"I'll test after"` -> Tests passing immediately prove nothing.
- `"Tests after achieve same goals"` -> Tests-after = "what does this do?" Tests-first = "what should this do?"
- `"Already manually tested"` -> Ad-hoc != systematic. No record, can't re-run.
- `"Deleting X hours is wasteful"` -> Sunk cost fallacy. Keeping unverified code is technical debt.
- `"Keep as reference, write tests first"` -> You'll adapt it. That's testing after. Delete means delete.
- `"Need to explore first"` -> Fine. Throw away exploration, start with TDD.
- `"Test hard = design unclear"` -> Listen to test. Hard to test = hard to use.
- `"TDD will slow me down"` -> TDD faster than debugging. Pragmatic = test-first.
- `"Manual test faster"` -> Manual doesn't prove edge cases. You'll re-test every change.

## Red Flags - STOP and Start Over

- Code before test
- Test after implementation
- Test passes immediately
- Can't explain why test failed
- Tests added "later"
- Rationalizing "just this once"
- "I already manually tested it"
- "Tests after achieve the same purpose"
- "It's about spirit not ritual"
- "Keep as reference" or "adapt existing code"
- "Already spent X hours, deleting is wasteful"
- "TDD is dogmatic, I'm being pragmatic"
- "This is different because..."

**All of these mean: Delete code. Start over with TDD.**

## Research Checklist

- [ ] Existing feature patterns identified
- [ ] Project structure understood
- [ ] Test framework configuration verified
- [ ] All ambiguities resolved with clarifying questions
- [ ] Scope clearly defined

## Plan Quality Checklist

- [ ] Every implementation task uses `RED -> Verify RED -> GREEN -> Verify GREEN -> REFACTOR -> Verify GREEN -> COMMIT`
- [ ] Every step has exact file paths
- [ ] Every step has complete code
- [ ] Every step has exact commands with expected output
- [ ] Every step has expected failure or pass conditions
- [ ] Steps are bite-sized (2-5 minutes each)
- [ ] DRY and YAGNI principles applied
- [ ] Frequent commits after each logical change

## Completeness Checklist

- [ ] All user stories covered
- [ ] All acceptance criteria addressed
- [ ] Testing strategy defined
- [ ] Risks and mitigations documented
- [ ] Success criteria listed as checklist
- [ ] Plan saved to `docs/plans/YYYY-MM-DD-{summary}.md`

## Verification Checklist

Before marking the plan complete:

- [ ] Every implementation task has a test-first step
- [ ] Every implementation task requires watching the test fail before implementing
- [ ] Every implementation task requires the expected failure reason
- [ ] Every implementation task requires minimal code to pass
- [ ] Every implementation task requires all relevant tests to pass
- [ ] Every implementation task requires pristine output (no errors, warnings)
- [ ] Edge cases and error paths are covered where needed

Can't check all boxes? The plan does not enforce TDD strongly enough. Fix the plan.

## Critical Stops

- [ ] Database changes flagged and handed to the user
- [ ] Ambiguous requirements resolved before proceeding
- [ ] Complexity path confirmed by the user
- [ ] Build or test failures addressed immediately
- [ ] The command does not finish without saving the plan first
