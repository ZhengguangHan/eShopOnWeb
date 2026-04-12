# Complete Example: User Email Verification Feature

This example walks through implementing an email verification feature using spec-driven development with the E2E path.

## Phase 1: Research

**User request**: "Implement email verification for user registration"

**Research findings**:
- Project: ASP.NET Core web application
- E2E test project: `Senku.E2ETests` with Reqnroll
- Test framework: xUnit, FluentAssertions, NSubstitute
- Auth pattern: JWT tokens already in use
- Email service: SendGrid configured
- Database: MongoDB with BigQuery data warehouse

**Clarifying questions asked**:
- Q: Should verification be required before login?
- A: Yes, users must verify before accessing system

**Result**: Requirements clarified, ready for specification

## Phase 2: Business Specification

Business context, user stories, acceptance criteria created with Mermaid flowchart showing user journey.

**Complexity Assessment**: E2E Path recommended (user-facing, cross-layer, business-critical)

**User confirmed**: "E2E Path"

## Phase 3: Gherkin Feature File

Generated complete feature file, presented to user, received confirmation, created file at `Senku.E2ETests/Features/EmailVerification.feature`

## Phase 4: Integration-First

Created skeleton with hardcoded data:
- UI components with static success messages
- API endpoints returning mock responses
- Services with fake business logic
- In-memory repositories

Verified complete journey works end-to-end.

## Phase 5: ATDD Implementation

Replaced hardcoded implementations with real code using TDD:
- Frontend: Real API calls
- API: Request validation
- Service: Email sending logic
- Repository: Database integration

Updated plan file after each component.

## Phase 6: Verification

All tests passed, code review conducted, plan finalized with status "Complete".

**Result**: Production-ready feature with comprehensive E2E test coverage.
