# {Feature Name} Implementation Plan

## Metadata
- **File**: `docs/plans/YYYY-MM-DD-{summary}.md`
- **Created**: {date and time}
- **Status**: Draft
- **Last Updated**: {timestamp}

## Phase 1: Research

**Status**: Complete
**Completed**: {timestamp}

### Research Findings

**Project Structure**:
- E2E Tests: [path]
- Unit Tests: [path]
- Integration Tests: [path if exists]

**Existing Patterns**: [Document similar features and conventions]

**Dependencies**: [List identified dependencies]

**Clarifications**: [Document Q&A with user]

## Phase 2: Business Specification

**Status**: Complete
**Completed**: {timestamp}

### Business Context
[1-2 paragraphs describing business need]

### User Stories
- **Story 1**: As a [role], I want [goal], so that [benefit]

### Acceptance Criteria
- **AC1**: Given [context] When [action] Then [outcome]

### User Journey
[Mermaid flowchart here]

### Scope
**In Scope**: [List features]
**Out of Scope**: [List exclusions]

### Complexity Assessment
**Recommended Path**: [E2E | Simplified TDD]
**User Decision**: [User choice]
**Confirmation Date**: {timestamp}
**Rationale**: [Why this path]

## Progress Log

Use this section as the lightweight execution record during implementation. Add one entry after every implementation batch before reporting progress or asking for feedback.

### Entry Template

- **Timestamp**: {timestamp}
- **Batch**: [Batch name or task numbers]
- **Task Status**: [Completed tasks / in-progress tasks]
- **Files Changed**: [Actual file paths changed in this batch]
- **Verification Results**: [Tests/build/lint results that were run]
- **Deviations**: [None, or describe what changed from plan and why]

## Path A: E2E Path (if chosen)

### Phase 3: Gherkin Feature File
**Status**: [Complete | Skipped]
**File Location**: `{Project}.E2ETests/Features/{Feature}.feature`
**User Confirmed**: {timestamp}
**File Created**: {timestamp}

### Phase 4: Integration Specification
**Status**: [Complete | Skipped]
**Completed**: {timestamp}
[Integration architecture details]

### Phase 5: ATDD Implementation
**Status**: [Complete | Skipped]
**Started**: {timestamp}
**Completed**: {timestamp}

#### Component 1
- [ ] Task description
  - Status: [Pending | In Progress | Complete]
  - Files Modified: [List]
  - Completed: {timestamp}

## Path B: Simplified TDD (if chosen)

### Simplified TDD Implementation
**Status**: [Complete | Skipped]
**Started**: {timestamp}
**Completed**: {timestamp}

## Phase 6: Verification

**Status**: Complete
**Verified**: {timestamp}
**Path Used**: [E2E Path | Simplified TDD Path]

### Business Acceptance
- [x] All user stories satisfied
- [x] All acceptance criteria met

### Technical Quality
- [x] All tests pass
- [x] Code follows standards

### Code Review
**Conducted**: {timestamp}
**Result**: [Approved | Approved with Suggestions]
**Key Findings**: [Summary]

### Implementation Summary
- Total files modified: X
- Key components: [List]
- Tests added: X E2E, X Integration, X Unit

## Files Modified
- Path/To/File1.cs - [Description]
- Path/To/File2.cs - [Description]
