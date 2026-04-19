# Project Context

## Purpose
**Microsoft eShopOnWeb** — ASP.NET Core reference application (maintained by NimblePros) demonstrating a single-process, monolithic eCommerce architecture. Companion sample to the free eBook *Architecting Modern Web Applications with ASP.NET Core and Azure*. The `main` branch targets ASP.NET Core 10.0.

## Tech Stack
- **Languages**: C# (nullable reference types, file-scoped namespaces enforced)
- **Framework**: .NET 10 / ASP.NET Core 10.0, .NET Aspire 13.x
- **Frontend**: Razor Pages + MVC (server-rendered), Blazor WebAssembly (admin area), Bootstrap
- **Database**: SQL Server (EF Core 10) with in-memory EF provider option (`UseOnlyInMemoryDatabase: true`)
- **Auth**: ASP.NET Core Identity, JWT bearer, OAuth GitHub SSO
- **Testing**: xUnit v3, NSubstitute, Reqnroll + MSTest (BDD/Gherkin for PublicApi), `WebApplicationFactory`, Coverlet
- **Key libraries**: Ardalis.ApiEndpoints, Ardalis.GuardClauses, Ardalis.Result, Ardalis.Specification, AutoMapper, MediatR, FastEndpoints, FluentValidation, NimblePros.SharedKernel/Metronome
- **Observability**: OpenTelemetry, Seq, Prometheus exporter
- **Build/CI**: `dotnet` CLI, GitHub Actions (`.github/workflows/dotnetcore.yml`), `azd` for Azure deploy
- **Infra**: Docker, Dev Containers, Azure Developer CLI (bicep in `infra/`)

## Project Structure (monorepo, single solution: `eShopOnWeb.sln`)
```
src/
  ApplicationCore/                Domain entities, interfaces, services, specifications (aggregates: Basket, Buyer, Order)
  Infrastructure/                 EF Core DbContexts, Identity, external services
  Web/                            Razor Pages + MVC storefront, Blazor host
  PublicApi/                      REST API (FastEndpoints / ApiEndpoints) for admin
  BlazorAdmin/                    Blazor WASM admin UI
  BlazorShared/                   DTOs shared between Blazor and PublicApi
  eShopWeb.AppHost/               Aspire orchestration host
  eShopWeb.AspireServiceDefaults/
tests/
  UnitTests/          xUnit v3 + NSubstitute, per-scenario folder layout (ApplicationCore, Web, Builders, MediatorHandlers)
  IntegrationTests/   Repository-level tests (EF Core)
  WebTests/           Reqnroll + MSTest BDD for Razor/MVC storefront (e.g., Basket.feature) via WebApplicationFactory
  PublicApiTests/     Reqnroll + MSTest BDD for PublicApi (Authentication, CatalogItems, RoleManagement, RoleMembership, UserManagement)
```

## Project Conventions

### Code Style (from `.editorconfig`)
- **Naming**: PascalCase for public members; `_camelCase` for private instance fields; PascalCase for constants
- **Formatting**: 4-space indent for C#, 2-space for XML; UTF-8 BOM; final newline required
- **Namespaces**: file-scoped (warning if violated)
- **Nullable**: enabled; `CS8618` suppressed for DTO/Request/Response files
- `var` preferred throughout; expression-bodied properties/accessors encouraged
- Braces always (`csharp_prefer_braces = true`)

### Architecture Patterns
- **Clean / Layered**: `Web` & `PublicApi` depend on `Infrastructure` (runtime DI) and `ApplicationCore`; `Infrastructure` depends on `ApplicationCore`; `ApplicationCore` has no project dependencies
- **DDD building blocks**: aggregate roots (`Basket`, `Buyer`, `Order`), `BaseEntity`, `IRepository<T>` + `Ardalis.Specification`
- **Guard clauses** (`Ardalis.GuardClauses`) for argument validation
- **Result pattern** (`Ardalis.Result<T>`) for service return types
- **API style**: REST; storefront via MVC controllers + Razor Pages, admin via PublicApi consumed by Blazor WASM
- **DI**: constructor injection; extension methods (`AddCoreServices`, `AddWebServices`, `AddDatabaseContexts`) wire services

### Testing Strategy
- Unit tests use NSubstitute (`Substitute.For<IRepository<Basket>>()`) with Arrange-Act-Assert
- One test class per scenario, folder-per-SUT layout (e.g., `BasketServiceTests/AddItemToBasket.cs`)
- BDD Reqnroll + MSTest features for both storefront (`WebTests`) and PublicApi (`PublicApiTests`); Gherkin `.feature` files with step definitions, hooks, and support contexts
- Coverage via `coverlet.collector` + `CodeCoverage.runsettings`
- CI runs `dotnet test` with XPlat Code Coverage on every push/PR

### Git Workflow
- **Branching**: feature branches merged via PR
- **Commit style**: Conventional Commits (`feat:`, `chore:`, `fix:`, `refactor:`)
- **CI gate**: build + test + coverage report must pass on `ubuntu-latest` with `.NET 10.0.x`

## Agent Working Rules

1. Before writing any code, first describe the solution and wait for user approval. If requirements are unclear, ask clarifying questions before writing any code.
2. If a task requires modifying 3 or more files, stop first and break it down into smaller tasks.
3. After writing code, list out potential issues and suggest corresponding test cases to improve coverage.
4. When finding a bug, first write a test that reproduces the bug, then iterate continuously until the test passes.
5. Every time the user corrects a mistake, add a new rule to this `CLAUDE.md` file so the situation does not happen again.

## Available Agents (project-defined, `agents/`)
| Agent | Purpose | When to Use |
|-------|---------|-------------|
| **planner** | Implementation planning | Complex features, multi-file changes |
| **tdd** | Test-driven development | New features, bug fixes — write tests FIRST |
| **code-reviewer** | Code review | After writing or modifying ANY code |

Workflow: `planner → tdd → code-reviewer` for features; `tdd → code-reviewer` for bug fixes.

## Domain Context
Reference eCommerce domain:
- **Catalog** — items, brands, types
- **Basket** — anonymous or user-owned; anonymous basket is **transferred to the user** on login (`TransferBasketAsync`)
- **Order** — created from basket at checkout
- **Admin** — role/user/catalog management exposed via PublicApi, consumed by BlazorAdmin

## Important Constraints
- `main` targets .NET 10 / ASP.NET Core 10.0 — do not downgrade framework version
- Database changes go through EF Core migrations; two contexts: `catalogcontext` (products/orders/baskets) and `appidentitydbcontext` (users/roles)
- In-memory DB is opt-in via `appsettings.json` (`"UseOnlyInMemoryDatabase": true`) — used for workshop scenarios
- `ApplicationCore` must have **no infrastructure dependencies** — dependency direction is part of the architecture
- File-scoped namespaces are a warning-level rule — treat as required
- Nullable reference types are on project-wide; DTOs/Requests/Responses have `CS8618` suppressed

## External Dependencies
- **GitHub OAuth** — SSO provider (client ID/secret stored in user secrets)
- **Azure** — deployment via `azd` (resources defined in `infra/`, secrets via Key Vault)
- **Seq** — log aggregation, wired via Aspire integration