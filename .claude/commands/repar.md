# Repar — Spec-Driven Development

## Overview

Invoke the full spec-driven development workflow in any mode. It follows the
spec-driven-development skill end-to-end: research, plan & specification,
mandatory plan file creation at `docs/plans/YYYY-MM-DD-{summary}.md`, where
`{summary}` is a kebab-case summary, complexity assessment, path selection
(E2E or Simplified TDD), and implementation through to verification. The
workflow must still write the plan file before moving on to implementation.

**Context management**: After Phase 2 (plan file saved), run `/compact` to
compress the conversation before starting implementation. The plan file on
disk is the source of truth — the implementation phase reads it fresh.

## Steps

1. **Environment Check**
  - Detect the current environment (Cursor or Claude Code)
    - Commands may run in any mode
    - Proceed as long as Phase 2 will create and save the plan file to
      `docs/plans/YYYY-MM-DD-{summary}.md` using a kebab-case `{summary}`
2. **Invoke Spec-Driven Development Skill (Phases 1–2)**
  - Read the skill file at `skills/spec-driven-development/SKILL.md`
    - **Phase 1**: Research and Discovery (planner agent)
    - **Phase 2**: Plan & Specification with mandatory plan file creation at
      `docs/plans/YYYY-MM-DD-{summary}.md` (planner agent)
    - **Complexity Assessment**: Present E2E vs Simplified TDD recommendation
    - Wait for user to choose a path
    - Do not continue to implementation until the plan file has been created and saved
3. **Compact Context**
  - Run `/compact` to compress the conversation
  - The plan file at `docs/plans/` is the source of truth — implementation
    reads it from disk, not from conversation history
4. **Continue Spec-Driven Development Skill (Phases 3–4)**
  - Re-read the skill file at `skills/spec-driven-development/SKILL.md`
  - Re-read the plan file at `docs/plans/YYYY-MM-DD-{summary}.md`
    - **E2E Path**: Phase 3 (Gherkin + TDD Implementation using tdd agent)
    - **Simple TDD Path**: Path B (TDD implementation using tdd agent)
    - **Phase 4**: Verify & Review (code-reviewer agent)
  - Follow all phase transitions, checkpoints, and stop conditions defined in the skill

## When to Stop and Ask

Stop immediately and ask the user when:

- Requirements are ambiguous or incomplete
- Database schema changes are identified (user must create schema)
- The feature scope is unclear
- Conflicting requirements are found during research
- User has not confirmed the complexity path
- Phase 2 has not yet created the required plan file

**Ask for clarification rather than guessing.**

## Checklist

### Before Starting

- Environment detected (Cursor or Claude Code)
- Requirement acknowledged: save the plan to `docs/plans/YYYY-MM-DD-{summary}.md`

### After Phase 1

- Codebase explored and patterns documented
- Ambiguities resolved via clarifying questions
- Project structure understood

### After Phase 2 (before compact)

- User stories written (As a / I want / So that)
- Acceptance criteria defined (Given / When / Then)
- Plan file created at `docs/plans/YYYY-MM-DD-{summary}.md`
- Workflow does not proceed on planning discussion alone; the file is saved
- Complexity assessment presented
- User confirmed path (E2E or Simplified TDD)

### After Compact

- `/compact` has been run
- Plan file path is known and file exists on disk
- Skill file will be re-read for implementation phases

### After Phase 3 (E2E Path Only)

- Gherkin feature file presented and confirmed by user
- Feature file saved to test project
- TDD implementation complete (RED → GREEN → REFACTOR for each component)

### After Phase 4

- All tests pass (unit, integration, BDD as applicable)
- Build succeeds with no linter errors
- Plan file status updated to "Complete"
- Code review completed (if requested)
