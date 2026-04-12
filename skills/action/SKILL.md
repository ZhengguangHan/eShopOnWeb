---
name: action
description: Execute a saved implementation plan from `docs/plans/` in controlled batches with review checkpoints. Use when the user asks to follow an existing plan, continue a saved plan, implement tasks from `docs/plans/`, or work through a plan step by step with TDD, verification, and feedback between batches. Do not use this skill to create a new plan.
---

# Action

Execute an existing implementation plan without inventing a new approach. Treat the saved plan file as the source of truth, move in small batches, and stop to ask when the plan or verification becomes unclear.

## Workflow

### 1. Load and review the plan

1. Read the selected plan file from `docs/plans/` completely.
2. Review it critically before changing code.
3. Raise any gaps, ambiguities, missing prerequisites, or risky assumptions with the user before starting.
4. If the plan is sound, summarize the intended execution approach and wait for user approval before changing files.
5. Turn the remaining work into a task list only after approval.
6. Verify prerequisites called out by the plan, including dependencies, baseline tests, and build status when applicable.

### 2. Execute a batch

Default to batches of 3 tasks. Use smaller batches when tasks are complex or tightly coupled.

The repo plan file in `docs/plans/` is the execution record for batch progress. Keep it updated as work advances; the in-memory todo list is only a temporary working aid.

For each task in the batch:

1. Mark the task as in progress.
2. Follow the plan step exactly as written.
3. Start with the failing test first.
4. Implement the smallest change that makes the test pass.
5. Refactor only with passing tests as safety net.
6. Run every verification the plan requires.
7. Commit after each logical change only when the user explicitly asked for commits.
8. Mark the task as complete only after verification passes.

Do not silently skip verifications or rewrite the plan on your own.

### 3. Report and pause

After each batch:

1. Before reporting batch completion, update the repo plan file with task status, actual files changed, verification results, and any deviations.
2. Summarize what was implemented.
3. Show the verification results that matter, such as tests, build status, or lint status.
4. List any deviations from the plan and justify them.
5. Say `Batch complete. Ready for feedback.`
6. Wait for user feedback before continuing.

### 4. Apply feedback and continue

1. Apply requested changes before starting the next batch.
2. If the plan itself must change, update the relevant file in `docs/plans/`.
3. If feedback changes the implementation direction materially, re-review the updated plan before resuming.
4. Repeat the execute, report, and feedback cycle until all tasks are done.

### 5. Finish cleanly

1. Run the full required verification suite.
2. Confirm tests pass and the build succeeds when applicable.
3. Check for linter errors in edited files when applicable.
4. Update the plan status to `Complete`.
5. Run code review if requested.
6. Update documentation if the delivered change is significant.
7. Present a concise summary of the completed work.

## Stop And Ask

Stop immediately and ask the user instead of guessing when:

- A plan step is unclear or ambiguous.
- A test keeps failing and the reason is not obvious.
- A dependency, environment issue, or external blocker appears.
- Database changes are required.
- The plan has a critical gap that prevents safe execution.
- Build or verification fails in a non-obvious way.
- Actual results do not match the expected verification outcome.

## Execution Checklist

Before starting:

- Plan file loaded and reviewed completely.
- Gaps or concerns raised before coding.
- Prerequisites checked.
- Remaining tasks identified.

Per batch:

- Tasks executed in order.
- `RED -> GREEN -> REFACTOR` followed.
- Required verifications run.
- Repo plan file updated with batch progress, actual files changed, verification results, and deviations.
- Do not mark a batch complete if the plan file was not updated.
- Progress reported to the user.
- Feedback received before continuing.

After completion:

- Full verification run.
- Tests and build confirmed passing when applicable.
- Plan marked `Complete`.
- Documentation updated if needed.
- Final summary delivered.
