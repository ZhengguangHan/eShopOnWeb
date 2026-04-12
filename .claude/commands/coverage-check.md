# Coverage Check — Spec Gap Analysis & Test Generation

## Overview

Audit an existing service or area of the codebase for missing Reqnroll
specification test coverage. Map all endpoints and behaviors, compare against
existing specs, report the gaps, then write and implement the missing specs.

**This is a TDD safety-net exercise:** you are about to change this service,
so you need guard rails in place first.

## Steps

1. **Identify the Target Service**
   - Ask the user which service or area to audit (e.g., "basket service",
     "catalog endpoints", "order service")
   - If the user already specified it, proceed directly

2. **Research the Service**
   - Read the skill file at `skills/research/SKILL.md`
   - Scope the research to the target service only:
     - Map every endpoint, controller action, or service method
     - Document the HTTP method, route, request/response shape
     - Note auth requirements and edge cases (empty input, not found, unauthorized)
     - Trace the data flow: controller/endpoint → service → repository → entity

3. **Find Existing Specs**
   - Search for all Reqnroll `.feature` files related to this service
   - Search for all existing xUnit/integration tests related to this service
   - List what is already covered and by which test file

4. **Gap Analysis**
   - Compare the full list of endpoints/behaviors against existing test coverage
   - Present a clear table:

     | Endpoint / Behavior | Covered? | Test File | Gap Description |
     |---------------------|----------|-----------|-----------------|
     | ...                 | Yes/No   | ...       | ...             |

   - **STOP** and present the gap analysis to the user
   - Ask: "These are the gaps I found. Which ones should I write specs for?"
   - Wait for user confirmation before proceeding

5. **Write Gherkin Specifications**
   - For each confirmed gap, write a `.feature` file with Given/When/Then scenarios
   - Follow the same patterns and conventions as existing spec files in the project
   - Each scenario should be independent and self-contained
   - Cover both happy path and key edge cases (not found, invalid input, unauthorized)
   - Present the feature files to the user for review before implementing

6. **Implement Step Definitions**
   - Write step definition classes matching the new feature files
   - Follow the patterns in existing step definitions
   - Use WebApplicationFactory and in-memory database
   - Each scenario must start from a clean state

7. **Verify**
   - Run all tests: both existing and new
   - Every new test must **fail first** (RED) before implementation makes it pass
   - No existing tests should break
   - Report results:
     - Total tests: X passed, Y failed
     - New specs: list each with pass/fail status

## When to Stop and Ask

- The target service is unclear
- A gap is found but it's unclear whether it should be covered
- Existing test patterns are inconsistent (ask which pattern to follow)
- An endpoint requires auth or setup that doesn't exist in the test infrastructure

**Ask for clarification rather than guessing.**

## Checklist

- [ ] Target service identified
- [ ] All endpoints/behaviors mapped
- [ ] Existing specs and tests found
- [ ] Gap analysis presented to user
- [ ] User confirmed which gaps to fill
- [ ] Gherkin feature files written and reviewed
- [ ] Step definitions implemented
- [ ] All new tests fail first (RED verified)
- [ ] All tests pass (existing + new)
- [ ] No existing tests broken
