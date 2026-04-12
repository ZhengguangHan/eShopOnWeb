# AGENTS.md - Project Context & Agent Guide

> **This file is the first thing Claude reads in every session.**
> A well-written AGENTS.md dramatically improves the quality of AI-generated code.
> Update this file with project-specific information.

---

## Project Overview

<!-- TODO: Describe what this project is, who it's for, and what it does. -->

## Tech Stack

<!-- TODO: List frameworks, languages, versions, and key dependencies. -->

## Project Structure

<!-- TODO: Describe the folder layout and what lives where. -->

## Architecture

<!-- TODO: Describe the architectural patterns (e.g., Clean Architecture layers, data flow). -->

## Conventions

<!-- TODO: Naming patterns, how endpoints are structured, how tests are organized. -->

## Key Patterns

<!-- TODO: Repository pattern, specification pattern, DI setup, etc. -->

## Constraints

<!-- TODO: Things the AI should NOT do (e.g., don't use SQL Server, don't modify certain folders). -->

---

## Available Agents

Located in `agents/`:

| Agent | Purpose | When to Use |
|-------|---------|-------------|
| **planner** | Implementation planning | Complex features, multi-file changes |
| **tdd** | Test-driven development | New features, bug fixes — write tests FIRST |
| **code-reviewer** | Code review | After writing or modifying ANY code |

---

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

---

## Rules

1. **Tests before code** — tdd agent ensures Red-Green-Refactor
2. **Review after code** — code-reviewer catches issues before commit
3. **Build verification** — build must pass after every change
4. **Plan update** — Update the active file in `docs/plans/` after each implementation batch

---

**End of AGENTS.md**
