# Anti-Patterns to Avoid

## Workflow Anti-Patterns

### Skipping Saved Plan Artifact Creation
**Problem**: Starting or continuing without creating `docs/plans/YYYY-MM-DD-{summary}.md`
**Solution**: Create and save the plan file during Phase 2 before implementation continues

### Proceeding Without User Confirmation
**Problem**: Creating files or choosing paths without user approval
**Solution**: Present, ask, STOP, wait for explicit confirmation

### Automatic Path Selection
**Problem**: Choosing E2E or Simple TDD without asking user
**Solution**: Present assessment and recommendation, then ask user to choose

### Implementing Before Specification
**Problem**: Jumping to code before completing business spec
**Solution**: Complete Phase 2 fully before any code

## Specification Anti-Patterns

### Implementation Details in Business Spec
**Bad**: "As a user, I want JWT tokens stored in Redux"
**Good**: "As a user, I want to stay logged in across sessions"

### Technical Language in Acceptance Criteria
**Bad**: "When POST hits /api/register endpoint"
**Good**: "When I submit the registration form"

### Vague Success Criteria
**Bad**: "Then the system should work correctly"
**Good**: "Then I should see confirmation message and receive email"

## Gherkin Anti-Patterns

### Implementation Details in Scenarios
**Bad**: Given the UserRepository is mocked
**Good**: Given I am on the registration page

### Missing Tags
Always use feature tags and AC tags

### Overly Complex Scenarios
One scenario, one concern - break down complex flows

## Implementation Anti-Patterns

### Skipping Hardcode Phase (E2E Path)
Always use fake data first to prove integration

### Ignoring Coding Standards
Reference dotnet-coding-standards during implementation

### Not Updating Plan File
Update plan immediately after each component

### Skipping TDD Phases
Strictly follow RED → GREEN → REFACTOR

## Verification Anti-Patterns

### Skipping Code Review
Always offer code review before final commit

### Incomplete Plan Updates
Document all changes in plan file

### Not Updating Progress.md
Always update as final action with link to plan

Remember: Following the methodology prevents these issues.
