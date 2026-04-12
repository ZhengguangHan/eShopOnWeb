---
name: research
description: Collaborative senior code partner for comprehensive feature research and discovery. Conducts codebase audit, logic mapping, and unknown identification before any implementation. Use when starting work on a new feature, investigating how to implement a change, needing to understand existing code flows, or when the user asks to research, analyze, or explore a feature area. Strictly read-only — never modifies code.
---

# Research — Collaborative Code Partner

Read-only codebase research for a feature or change request. Explore, map, identify unknowns, and report — never modify code.

## Critical Rules

- **NEVER modify code.** This skill is strictly read-only.
- **NEVER make speculative suggestions.** Only report what is confirmed by the codebase.
- **STOP and ask** if you are not 100% sure about context, dependencies, or requirements.
- **Use the `AskUserQuestion` tool (Cursor)** during the research phase whenever requirements are ambiguous, unknowns need decisions, or you would otherwise ask the user in free-form chat. Prefer structured `AskUserQuestion` prompts over long prose-only questions so answers are explicit and easy to track.

## Workflow

### Step 1: Context Analysis (Codebase Audit)

1. Read `AGENTS.md` for project context (tech stack, conventions, architecture)
2. Explore the workspace structure relevant to the feature:
   - Identify related source files, modules, and namespaces
   - Search for existing implementations of similar features
   - Review recent changes (`git log --oneline -20`, `git diff` on relevant paths)
3. Retain all contextual information in your findings:
   - Always include file paths, function/class names, and module locations
   - Quote relevant code snippets using markdown code references

**Output**: List of relevant files, modules, and existing patterns with paths.

### Step 2: Logic Mapping

1. Trace the data flow for the feature area:
   - Entry points (API controllers, UI components, event handlers)
   - Service layer (business logic, orchestration)
   - Data layer (repositories, database calls, external APIs)
2. Document existing functions and classes that will interact with the new feature
3. Map dependencies between components (what calls what)
4. Note any cross-cutting concerns (auth, logging, validation, caching)

**Output**: Data flow description with function signatures, call chains, and layer boundaries.

### Step 3: Identify Unknowns (Mandatory)

Explicitly list every gap found during research. **As you surface gaps, use `AskUserQuestion`** (Cursor) to collect decisions or missing facts—especially when a question has a small set of valid answers (multiple choice) or when you need one focused answer before continuing deeper exploration. You may batch related unknowns into one `AskUserQuestion` turn or split them if answers unblock different branches of the audit.

- **Files**: Source files that could not be located or were ambiguous
- **Variables/Config**: Environment variables, feature flags, or settings not found
- **Dependencies**: External services, packages, or APIs with unclear usage
- **Requirements**: Business rules or acceptance criteria that are ambiguous
- **Database**: Schema, stored procedures, or queries that need clarification
- **Permissions**: Auth/role requirements that are not documented

**This step is not optional.** If there are zero unknowns, state that explicitly with justification.

### Step 4: Clarify and Report

**Use `AskUserQuestion` again for any remaining unknowns** before you treat the research phase as complete; the written report should reflect answers you already received via the tool. Present findings using this structure:

```markdown
## Research Report: [Feature Name]

### 1. Project Context
- Tech stack: [from AGENTS.md or detected]
- Relevant area: [module/namespace/layer]

### 2. Related Files
| File | Purpose | Relevance |
|------|---------|-----------|
| `path/to/file.cs` | [What it does] | [Why it matters] |

### 3. Data Flow
[Entry point] → [Service] → [Repository] → [Database/External]

Key functions:
- `ClassName.MethodName()` (`path/to/file.cs`) — [What it does]

### 4. Existing Patterns
[Code reference showing how similar features are implemented]

### 5. Unknowns & Questions
1. [Question about unclear requirement]
2. [Question about missing dependency]
3. [Question about ambiguous data flow]

**Please answer these questions before I proceed.**
```

**After presenting the report, STOP and wait for user responses** (if any questions were only in the report and not yet asked via `AskUserQuestion`, use `AskUserQuestion` now rather than assuming the user will reply unprompted).

## Environment Adaptation

**Cursor**: Use Task tool with `explore` subagent for parallel codebase exploration. **Prefer the `AskUserQuestion` tool throughout the research phase** (Steps 1–4) for structured clarification—scope, requirements, architecture choices, and unknowns—not only at the end of Step 4.

**Claude Code**: Use available search and read tools directly. Present questions inline and wait for user response.

## Constraints

- Do not propose implementation approaches, architecture changes, or code modifications
- Do not create, edit, or delete any files
- Do not run build, test, or deploy commands
- All code shown is strictly for reference (quoted from existing codebase)
- If the feature area is too broad, ask the user to narrow scope before continuing
