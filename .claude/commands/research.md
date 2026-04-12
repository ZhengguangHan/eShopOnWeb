# Research — Feature Investigation

## Overview

Conduct comprehensive, read-only research on a feature or change request before
any implementation begins. Invokes the research skill to explore the codebase,
map data flows, identify unknowns, and present findings with explicit questions
for clarification. **In Cursor, use the `AskUserQuestion` tool during the research
phase** (not only prose in chat) so clarifications are structured and captured
before the final report.

## Steps

1. **Invoke Research Skill**
  - Read the skill file at `skills/research/SKILL.md`
    - Follow the skill workflow exactly:
      - **Step 1**: Context Analysis — audit codebase, read `AGENTS.md`, review git history
      - **Step 2**: Logic Mapping — trace data flow across layers, map dependencies
      - **Step 3**: Identify Unknowns — list every gap (files, config, requirements, DB, permissions); **use `AskUserQuestion`** as gaps appear when user input is required
      - **Step 4**: Clarify and Report — present structured research report; **use `AskUserQuestion`** for any remaining unknowns, then **STOP** and wait if anything is still unanswered
    - **STOP** after presenting the report (and after any final `AskUserQuestion` round) and wait for user answers
2. **Follow-Up** (after user responds to unknowns)
  - Incorporate user answers into the research findings
    - Update the report if new information changes the analysis
    - Confirm all unknowns are resolved

## When to Stop and Ask

- Requirements are ambiguous or incomplete
- Feature scope is too broad to research effectively
- Cannot locate key files, dependencies, or modules
- Database schema or external API details are unclear
- Not 100% sure about any aspect of the codebase context

**Ask for clarification rather than guessing. Never modify code.**

**Cursor**: Prefer **`AskUserQuestion`** for those clarifications during research instead of relying only on open-ended chat messages.

## Checklist

- [ ] Research skill loaded and followed
- [ ] `AGENTS.md` project context reviewed
- [ ] Relevant files and modules identified with paths
- [ ] Data flow traced across layers
- [ ] Unknowns explicitly listed
- [ ] **`AskUserQuestion` used in Cursor** where user decisions or missing facts were needed during the research phase
- [ ] Structured research report presented
- [ ] User answered all unknowns before proceeding
