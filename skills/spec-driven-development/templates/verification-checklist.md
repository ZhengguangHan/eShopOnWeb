# Verification Checklist Template

## Phase 6: Feature Verification

**Status**: [Not Started | In Progress | Complete]
**Verification Date**: {timestamp}
**Path Used**: [E2E Path | Simplified TDD Path]

### Business Acceptance
- [ ] All user stories satisfied
- [ ] All acceptance criteria met
- [ ] User journey works end-to-end (if E2E)
- [ ] Edge cases handled

### Technical Quality
- [ ] All tests pass (E2E/Integration/Unit)
- [ ] Build succeeds
- [ ] No linter errors
- [ ] Code follows standards

### Plan File Finalization
- [ ] All tasks marked complete [x]
- [ ] All statuses updated to "Complete"
- [ ] All modified files documented
- [ ] All timestamps recorded
- [ ] Plan status updated to "Complete"

### Code Review (Recommended)

**Prompt user**:
```
✅ Implementation Complete!

Would you like to review the code?
1. Yes, review now (recommended)
2. Skip review
3. I'll review myself later
```

**If user requests review**:

#### Code Review Results
**Date**: {timestamp}
**Result**: [Approved | Approved with Suggestions]

**Strengths**: [List positive aspects]

**Suggestions**:
- Priority: [High/Medium/Low] - [Suggestion]
  - Current: [What code does]
  - Suggested: [Improvement]
  - Rationale: [Why better]

**Security/Safety**: [Notes]

**Performance**: [Notes]

**Recommendation**: [Approve and commit | Address suggestions]

### Implementation Summary
- Total files modified: X
- Total lines changed: +XXX -XXX
- Key components: [List]
- Tests added: X E2E, X Integration, X Unit

### Files Modified
- `Path/To/File1.cs` - [Description]
- `Path/To/File2.cs` - [Description]

### Notes
[Additional context or decisions]
