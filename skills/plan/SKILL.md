---
name: plan
description: Create actionable implementation plans for features, refactors, and complex bug fixes. Use when the user explicitly asks for a saved implementation plan, phased execution plan, roadmap, or step-by-step plan file in `docs/plans/`. Produce a requirements-driven plan with research, architecture review, a Mermaid user journey, complexity-path recommendation, TDD-first steps, exact file paths, test commands, risks, and success criteria.
---

# Plan

Create a saved implementation plan before writing code. Turn ambiguous requests into a concrete execution path with explicit assumptions, acceptance criteria, and bite-sized TDD tasks.

## Workflow

### 1. Verify planning mode first

1. When this skill is run directly for planning work, start it in planning mode before doing research or authoring the plan.
2. In Cursor, switch to `Plan` mode first when this skill is being run directly.
3. In Claude Code, MUST use `EnterPlanMode` when this skill is being run directly.
4. If a higher-level workflow intentionally runs this skill from another mode, do not block on mode switching, but still keep this skill planning-only.
5. While using this skill, do not implement code. Limit work to discovery, clarification, and authoring the saved plan.

### 2. Research the codebase

1. Explore relevant files, patterns, tests, and recent changes.
2. Identify affected layers, dependencies, and existing conventions.
3. Ask up to two clarifying questions if the request is ambiguous.
4. Record project-specific constraints that must shape the plan.

### 3. Define the requirement

1. Restate the user goal in one sentence.
2. Write user stories using `As a / I want / So that`.
3. Define acceptance criteria using `Given / When / Then`.
4. List assumptions, constraints, and scope boundaries.

### 4. Review architecture

1. Identify reusable components and similar implementations.
2. Map the affected layers and key data flow.
3. Include a Mermaid user journey as part of the architecture review.
4. If database changes are required, stop and ask the user for help before continuing.

### 5. Choose the delivery path

Recommend one path, explain why, and get user confirmation before implementation begins:

- `E2E path`: user-facing UI, multi-layer workflows, critical business behavior, external integrations, or complex state.
- `Simplified TDD path`: backend-only changes, single-layer work, utility functions, straightforward CRUD, or narrow bug fixes.

### 6. Author the saved plan

1. Create `docs/plans/YYYY-MM-DD-{summary}.md`.
2. Use a short kebab-case summary.
3. Make the saved file the single source of truth.
4. Break work into phases and tasks that each take roughly 2-5 minutes.
5. Read `references/api_reference.md` before writing the final plan file and treat it as the required contract, not optional guidance.
6. Use the exact document structure from `references/api_reference.md`.
7. For every implementation task, use the exact task contract from `references/api_reference.md`:
   `RED -> Verify RED -> GREEN -> Verify GREEN -> REFACTOR -> Verify GREEN -> COMMIT`
8. For every implementation task, include exact file paths, complete code, commands, expected failure or pass conditions, and commit message text.
9. Reuse the TDD wording from `references/api_reference.md` as closely as possible, including the mandatory verification language and anti-rationalization guardrails.
10. Preserve the TDD exception handling from `references/api_reference.md`; if a task is throwaway prototype work, generated code, or configuration-only work, tell the user and ask before forcing TDD steps into the plan.
11. If the user explicitly approves that exception, use the approved exception contract from `references/api_reference.md` instead of inventing fake `RED` or `GREEN` sections.

## Quality Bar

- Save the plan file before handing off options.
- Prefer exact code and commands over summaries like "add validation".
- Keep steps DRY and YAGNI-aware.
- Include unit, integration, and E2E coverage only where needed.
- Every implementation task must enforce test-first behavior strongly enough that an executor cannot reasonably interpret it as "tests after are acceptable".
- Call out risks, mitigations, and measurable success criteria.

## Output Rules

When presenting the result:

1. Confirm the saved plan path.
2. Summarize the recommended complexity path.
3. List any open questions or blocked items.
4. Offer two execution options:
   - Continue in the current session.
   - Hand off to a new session using the saved plan.
