# Project Context — Discover & Save

## Overview

Explore the current project and automatically discover its tech stack, coding
conventions, architecture patterns, testing strategy, and domain context.
Present findings for user review, then save the completed project context to
`CLAUDE.md`.

## Steps

1. **Project Structure Discovery**
  - List the top-level directory structure
    - Identify solution files (`.sln`), project files (`.csproj`, `package.json`,
      `tsconfig.json`, `go.mod`, etc.)
    - Detect monorepo vs single-project layout
    - Identify source, test, and documentation directories
    - Note any existing config files (`.editorconfig`, `.eslintrc`, `Dockerfile`,
      `docker-compose.yml`, CI/CD pipelines, etc.)

2. **Tech Stack Detection**
  - Read project/dependency files to identify:
    - **Languages**: C#, TypeScript, JavaScript, Python, Go, etc.
    - **Frameworks**: ASP.NET Core, Vue, React, Express, etc.
    - **Databases**: SQL Server, MongoDB, BigQuery, PostgreSQL, Redis, etc.
    - **Testing frameworks**: xUnit, NUnit, Jest, Playwright, Reqnroll, etc.
    - **Build tools**: dotnet, npm, webpack, vite, etc.
    - **Infrastructure**: Docker, Kubernetes, cloud provider, CI/CD platform
    - **Key libraries**: ORM (EF Core, Dapper), logging (Serilog), DI container, etc.

3. **Code Style and Conventions**
  - Check for style configuration files (`.editorconfig`, `.eslintrc`,
    `.prettierrc`, `stylecop.json`, `Directory.Build.props`)
    - Sample 3-5 source files to detect naming conventions (PascalCase,
      camelCase, snake_case)
    - Detect indentation style (tabs vs spaces, indent size)
    - Check for nullable reference types, implicit usings, file-scoped namespaces
    - Note async/await patterns, dependency injection usage, error handling patterns

4. **Architecture Pattern Analysis**
  - Identify architectural patterns from folder structure and code:
    - Layered architecture (Controller → Service → Repository)
    - Clean architecture, CQRS, MediatR, vertical slices
    - API style (REST, GraphQL, gRPC)
    - Frontend patterns (component-based, state management)
    - Check for shared projects, common libraries, or cross-cutting concerns

5. **Testing Strategy Detection**
  - Locate test projects and their structure
    - Detect testing patterns (Arrange-Act-Assert, Given-When-Then)
    - Identify test categories (unit, integration, E2E)
    - Check for test fixtures, factories, builders, or shared test utilities
    - Note mocking frameworks (Moq, NSubstitute, FakeItEasy)
    - Check for coverage tools (Coverlet, dotCover)

6. **Git Workflow Analysis**
  - Read recent git log (last 20 commits) to detect:
    - Commit message conventions (conventional commits, prefixes, ticket references)
    - Branching patterns (feature branches, trunk-based)
    - Check for branch protection or PR templates (`.github/`, `.azuredevops/`)

7. **Domain Context and Constraints**
  - Look for domain-specific terminology in code and docs
    - Check for regulatory or compliance patterns (audit logging, data encryption)
    - Identify external service integrations (APIs, message queues, third-party SDKs)
    - Note any README, CONTRIBUTING, or architecture decision records (ADRs)

8. **Present Findings for Review**
  - Fill out the project context template (see below) with all discovered information
    - Present the completed template to the user for review
    - Ask: "Does this accurately capture your project? Any corrections or additions?"
    - Wait for user confirmation or corrections

9. **Save to CLAUDE.md**
  - After user confirms, append or update the "Project Context" section in `CLAUDE.md` at the project root
    - If `CLAUDE.md` already has a "Project Context" section, replace it
    - If `CLAUDE.md` does not exist, create it with the project context as the first section
    - Preserve any existing content in `CLAUDE.md` (agent definitions, workflows, etc.)

## Project Context Template

```markdown
# Project Context

## Purpose
[Discovered project purpose from README, solution name, or domain analysis]

## Tech Stack
- **Languages**: [e.g., C# 12, TypeScript 5.x]
- **Frameworks**: [e.g., ASP.NET Core 8, Vue 3]
- **Databases**: [e.g., SQL Server, MongoDB, Redis]
- **Testing**: [e.g., xUnit, Reqnroll, Playwright]
- **Build/CI**: [e.g., dotnet CLI, npm, GitHub Actions]
- **Infrastructure**: [e.g., Docker, Kubernetes, Azure]
- **Key Libraries**: [e.g., Dapper, Serilog, MediatR, AutoMapper]

## Project Conventions

### Code Style
- **Naming**: [e.g., PascalCase for public members, _camelCase for private fields]
- **Formatting**: [e.g., 4-space indent, file-scoped namespaces, nullable enabled]
- **Patterns**: [e.g., async/await throughout, constructor DI, IOptions<T> for config]

### Architecture Patterns
- **Backend**: [e.g., Layered — Controller → Service → Repository]
- **Frontend**: [e.g., Vue 3 Composition API, component-based with Tailwind CSS]
- **API Style**: [e.g., RESTful with versioned endpoints]
- **Cross-cutting**: [e.g., middleware for auth, global exception handler]

### Testing Strategy
- **Unit tests**: [e.g., xUnit + Moq, Arrange-Act-Assert pattern]
- **Integration tests**: [e.g., WebApplicationFactory, test database]
- **E2E tests**: [e.g., Reqnroll + Playwright, Gherkin feature files]
- **Coverage target**: [e.g., 80%+ with Coverlet]

### Git Workflow
- **Branching**: [e.g., feature branches off main, PR-based merging]
- **Commit style**: [e.g., conventional commits — feat:, fix:, refactor:]
- **PR process**: [e.g., requires review, CI must pass]

## Agent Working Rules

1. Before writing any code, first describe the solution and wait for user
   approval. If requirements are unclear, ask clarifying questions before
   writing any code.
2. If a task requires modifying 3 or more files, stop first and break it
   down into smaller tasks.
3. After writing code, list out potential issues and suggest corresponding
   test cases to improve coverage.
4. When finding a bug, first write a test that reproduces the bug, then
   iterate continuously until the test passes.
5. Every time the user corrects a mistake, add a new rule to this
   `CLAUDE.md` file so the situation does not happen again.

## Domain Context
[Domain-specific terminology, business rules, or concepts the AI needs to understand]

## Important Constraints
- [e.g., Database changes require manual schema creation — STOP and ask]
- [e.g., No Element Plus components in frontend]
- [e.g., All API endpoints must be authenticated]

## External Dependencies
- [e.g., Payment gateway API — docs at URL]
- [e.g., Message queue — RabbitMQ / Azure Service Bus]
- [e.g., Third-party auth — OAuth2 / OIDC provider]

---

## Available Agents

Located in `agents/`:

| Agent | Purpose | When to Use |
|-------|---------|-------------|
| **planner** | Implementation planning | Complex features, multi-file changes |
| **tdd** | Test-driven development | New features, bug fixes — write tests FIRST |
| **code-reviewer** | Code review | After writing or modifying ANY code |

## Core Workflow

### New Feature Development

```
planner → tdd → code-reviewer
```

1. **planner**: Break down feature into implementation steps
2. **tdd**: Write failing tests → implement → refactor (Red-Green-Refactor)
3. **code-reviewer**: Review all code changes for quality and security

### Bug Fix

```
tdd → code-reviewer
```

1. **tdd**: Write a test that reproduces the bug (MUST fail), then fix
2. **code-reviewer**: Review the fix for regressions

## Rules

1. **Tests before code** — tdd agent ensures Red-Green-Refactor
2. **Review after code** — code-reviewer catches issues before commit
3. **Build verification** — build must pass after every change
4. **Plan update** — Update the active file in `docs/plans/` after each implementation batch
```

## When to Stop and Ask

Stop and ask the user when:

- Project purpose is unclear from available files
- Multiple conflicting conventions are detected
- Domain context cannot be inferred from code alone
- External dependencies are detected but their role is unclear

**Ask for clarification rather than guessing.**

## Checklist

### Discovery

- [ ] Top-level project structure mapped
- [ ] All project/dependency files identified
- [ ] Tech stack fully detected
- [ ] Code style conventions identified
- [ ] Architecture patterns documented
- [ ] Test projects and patterns found
- [ ] Git workflow analyzed

### Review

- [ ] Template filled with discovered information
- [ ] Findings presented to user
- [ ] User confirmed or corrected findings

### Save

- [ ] Project context saved to `CLAUDE.md`
- [ ] Existing `CLAUDE.md` content preserved
