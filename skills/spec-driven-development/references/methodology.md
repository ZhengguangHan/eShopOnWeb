# Spec-Driven Development Methodology

## Overview

Spec-driven development is an outside-in development approach that prioritizes system integration and user experience before implementation details.

## Core Philosophy

### Integration Confidence First

Build and prove the complete user journey works before perfecting individual components. This catches interface and flow problems early when they're cheapest to fix.

### User Perspective Drives Design

Always start from how the user experiences the feature. Technical implementation follows from user needs, not the reverse.

### Fail Fast on Integration Issues

Surface integration problems immediately rather than discovering them late in development when changes are expensive.

### TDD for Reliability

Use test-driven development to build robust, maintainable code with confidence that changes don't break existing functionality.

### Incremental Refinement

Continuously improve while maintaining working software. Never break the integration.

## Development Phases

### Phase 1: Research

**Goal**: Understand the landscape before planning

**Why it matters**: Premature planning without context leads to rework. Understanding existing patterns, constraints, and architecture informs better designs.

**Activities**:
- Explore codebase for similar features
- Identify existing test frameworks
- Understand current architecture
- Document conventions and patterns
- Clarify ambiguous requirements

**Output**: Research findings, clarified requirements, identified patterns

### Phase 2: Business Specification

**Goal**: Define WHAT needs to be built from user perspective

**Why it matters**: Implementation details change, but business requirements are stable. Starting with business specs ensures you build the right thing.

**Activities**:
- Write user stories (As a/I want/So that)
- Define acceptance criteria (Given/When/Then)
- Create user journey diagrams
- Document scope boundaries
- List dependencies

**Output**: Business specification document with user-focused requirements

**Key principle**: No implementation details. Focus purely on observable behavior and business value.

### Phase 2 Decision: Complexity Assessment

**Goal**: Choose appropriate testing strategy for feature complexity

**Why it matters**: E2E tests provide comprehensive coverage but take longer to write and run. Simple features don't justify the overhead.

**Decision criteria**:
- **E2E Path**: User-facing, cross-layer, business-critical, complex state
- **Simple TDD**: Internal only, single layer, utilities, bug fixes, simple CRUD

**User confirmation required**: Present assessment and recommendation, wait for user to choose path.

### Phase 3: Gherkin Feature File (E2E Path Only)

**Goal**: Create executable specification using Gherkin

**Why it matters**: Gherkin translates business requirements into executable tests that verify the complete user journey. It serves as living documentation.

**Activities**:
- Translate acceptance criteria to scenarios
- Use Given/When/Then format
- Tag scenarios with AC references
- Include data-driven tests (Scenario Outline)
- Present to user for approval

**Output**: `.feature` file in E2E test project

**Critical**: File only created after explicit user confirmation. This ensures requirements are correctly captured before implementation begins.

### Phase 4: Integration-First (E2E Path Only)

**Goal**: Prove the complete flow works before adding real logic

**Why it matters**: Building integration last often reveals interface mismatches and design issues late. Building it first with fake data proves the architecture before investing in real implementation.

**Activities**:
- Create UI components with hardcoded data
- Build API endpoints with mock responses
- Implement services returning fake objects
- Use in-memory repositories (Dictionary)
- Wire all components together
- Verify complete user journey works

**Output**: Working skeleton that demonstrates integration

**Key principle**: "Fake it before you make it" - prove the interfaces work before adding complexity.

### Phase 5: ATDD Implementation (E2E Path Only)

**Goal**: Replace fake implementations with real code using TDD

**Why it matters**: With integration proven, you can focus on correctness of individual components. TDD ensures each component works correctly before integration.

**Activities**:
- Follow RED → GREEN → REFACTOR cycle
- Replace hardcoded UI with real API calls
- Replace mock API responses with real logic
- Replace fake services with business logic
- Replace in-memory storage with database
- Maintain working system at every step

**Output**: Production-ready implementation with E2E test coverage

**Key principle**: Always keep the system working and deployable. Never break the integration.

### Simplified TDD Path (Simple Features)

**Goal**: Implement simple features efficiently without E2E overhead

**When to use**: Internal changes, single-layer updates, utilities, bug fixes

**Activities**:
- Write unit tests for business logic
- Write integration tests for data access
- Follow TDD methodology
- Skip Gherkin and integration skeleton

**Output**: Implementation with unit and integration test coverage

### Phase 6: Verification

**Goal**: Ensure complete implementation meets all requirements

**Why it matters**: Final quality gate before considering work complete. Catches issues before they reach production.

**Activities**:
- Verify all acceptance criteria met
- Confirm all tests pass
- Check build succeeds
- Review linter output
- Validate against coding standards
- Conduct code review
- Document results in plan

**Output**: Verified, production-ready implementation

## Progress Tracking

### During Development

**Plan file is single source of truth**:
- Update after each component completion
- Mark tasks complete with timestamps
- Document modified files
- Track status at multiple levels

### After Completion

**Update plan file with final status**:
- Mark all tasks as complete
- Document implementation date
- Set plan status to "Complete"

## Key Success Factors

### Never Skip the Hardcode Step (E2E Path)

In integration phase, always use hardcoded values first:
- ✅ `return "Hello World";`
- ✅ `return new User { Id = 1, Name = "Test" };`

This proves the test can pass and integration works before adding complexity.

### Run Tests After Every Step

After RED, GREEN, or REFACTOR:
1. Build the solution
2. Run tests
3. Verify expected outcome
4. Only then proceed

### Maintain Working System

The system should always:
- Compile successfully
- Run without crashing
- Pass all implemented tests
- Be deployable

### User Confirmations

Never proceed past these checkpoints without user approval:
- Complexity assessment (Phase 2)
- Gherkin feature file (Phase 3)
- Database schema changes (any phase)

## Anti-Patterns to Avoid

### Skipping Complexity Assessment

Don't automatically choose E2E for everything. Simple features waste time with unnecessary ceremony.

### Creating Gherkin Without Approval

Always present and get confirmation. Users need to verify requirements are correctly captured.

### Implementation Details in Business Spec

Phase 2 describes WHAT, not HOW. No technology choices, no implementation patterns.

### Skipping Integration Skeleton (E2E Path)

Don't jump to real implementation. Prove integration works with fake data first.

### Breaking the Build

Never proceed with compilation errors. Fix immediately before continuing.

### Updating plan file During Work

Plan file tracks details during implementation. Only update the plan file's final status section when all work is complete.

## Summary

Spec-driven development ensures you:
1. Build the right thing (business specs first)
2. Build it correctly (TDD methodology)
3. Build with confidence (integration proven early)
4. Build efficiently (appropriate testing strategy)
5. Build with quality (code standards + review)

The methodology adapts to feature complexity while maintaining quality standards throughout.
