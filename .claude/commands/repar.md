# Repar — Spec-Driven Development

## Overview

Invoke the full spec-driven development workflow in any mode. It follows the
spec-driven-development skill end-to-end: research, business specification,
mandatory plan file creation at `docs/plans/YYYY-MM-DD-{summary}.md`, where
`{summary}` is a kebab-case summary, complexity assessment, path selection
(E2E or Simplified TDD), and implementation through to verification. The
workflow must still write the plan file before moving on to implementation.

## Steps

1. **Environment Check**
  - Detect the current environment (Cursor or Claude Code)
    - Commands may run in any mode
    - Proceed as long as Phase 2 will create and save the plan file to
      `docs/plans/YYYY-MM-DD-{summary}.md` using a kebab-case `{summary}`
2. **Invoke Spec-Driven Development Skill**
  - Read the skill file at `skills/spec-driven-development/SKILL.md`
    - Follow the entire skill workflow from start to finish:
      - **Phase 1**: Research and Discovery
      - **Phase 2**: Business Specification and mandatory plan file creation at
        `docs/plans/YYYY-MM-DD-{summary}.md`
      - **Complexity Assessment**: Present E2E vs Simplified TDD recommendation
      - Wait for user to choose a path
      - **E2E Path**: Phase 3 (Gherkin) → Phase 4 (Integration) → Phase 5 (ATDD) → Phase 6 (Verify)
      - **Simple TDD Path**: Path B (TDD implementation) → Phase 6 (Verify)
    - Follow all phase transitions, checkpoints, and stop conditions defined in the skill
    - Do not continue to implementation until the plan file has been created and saved

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

### After Phase 2

- User stories written (As a / I want / So that)
- Acceptance criteria defined (Given / When / Then)
- Plan file created at `docs/plans/YYYY-MM-DD-{summary}.md`
- Workflow does not proceed on planning discussion alone; the file is saved
- Complexity assessment presented
- User confirmed path (E2E or Simplified TDD)

### After Phase 3 (E2E Path Only)

- Gherkin feature file presented and confirmed by user
- Feature file saved to E2E test project

### After Implementation

- All tests pass (unit, integration, E2E as applicable)
- Build succeeds with no linter errors
- Plan file status updated to "Complete"
- Code review completed (if requested)
- Documentation updated (if needed)
