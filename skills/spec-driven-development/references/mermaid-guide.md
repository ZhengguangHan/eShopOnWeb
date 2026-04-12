# Mermaid Diagram Guide for Spec-Driven Development

## Mandatory Diagrams by Phase

### Phase 2: Business Specification

**Required**:
- User journey flowchart showing complete user experience

**Optional**:
- System context diagram (if feature involves multiple systems)
- State diagram (if feature has distinct states)

### Phase 4: Integration-First (E2E Path)

**Required**:
- Sequence diagram showing component interactions
- Component diagram showing integration points

**Optional**:
- Data flow diagram (if data-heavy feature)

### Phase 5: ATDD Implementation

**Required**:
- Work breakdown structure (WBS) showing task hierarchy
- Dependency graph showing task relationships

## Critical Syntax Rules

### 1. No Spaces in Node IDs

Node IDs must be single words. Use camelCase, PascalCase, or underscores.

**Good**:
```mermaid
flowchart TD
    userAuth[User Authentication]
    apiGateway[API Gateway]
    dataLayer[Data Layer]
```

**Bad**:
```mermaid
flowchart TD
    user auth[User Authentication]  # Space in ID breaks parser
    API Gateway[API Gateway]        # Space in ID breaks parser
```

### 2. No HTML Tags in Labels

HTML tags like `<br/>` render as literal text or cause errors.

**Good**:
```mermaid
sequenceDiagram
    participant UI as User Interface
    participant API as API Service
```

**Bad**:
```mermaid
sequenceDiagram
    participant UI as User<br/>Interface  # Renders as literal text
```

### 3. Quote Special Characters in Edge Labels

Wrap labels containing parentheses, brackets, colons in quotes.

**Good**:
```mermaid
flowchart TD
    A -->|"O(1) lookup"| B
    C -->|"Check: valid?"| D
```

**Bad**:
```mermaid
flowchart TD
    A -->|O(1) lookup| B  # Parentheses parsed as node syntax
```

### 4. No Explicit Styling

Theme automatically applies colors. Manual styling breaks in dark mode.

**Good**:
```mermaid
flowchart TD
    A[Process]
    B[Complete]
```

**Bad**:
```mermaid
flowchart TD
    A[Process]
    B[Complete]
    style A fill:#fff  # Breaks in dark mode
```

### 5. Explicit Subgraph IDs

Use format: `subgraph id [Label]`

**Good**:
```mermaid
flowchart TD
    subgraph auth [Authentication Flow]
        login[Login]
        verify[Verify]
    end
```

**Bad**:
```mermaid
flowchart TD
    subgraph Authentication Flow  # Space causes parsing error
        login[Login]
    end
```

## Diagram Types and Use Cases

### Flowchart - User Journeys and Workflows

**Use for**: User experience flows, decision trees, process steps

**Example**:
```mermaid
flowchart TD
    Start([User Visits Site]) --> Login{Logged In?}
    Login -->|No| ShowLogin[Show Login Form]
    Login -->|Yes| Dashboard[Load Dashboard]
    ShowLogin --> EnterCreds[Enter Credentials]
    EnterCreds --> Validate{Valid?}
    Validate -->|No| Error[Show Error]
    Validate -->|Yes| Dashboard
```

### Sequence Diagram - Component Interactions

**Use for**: API calls, service communication, integration flows

**Example**:
```mermaid
sequenceDiagram
    participant UI as User Interface
    participant API as API Controller
    participant Service as User Service
    participant DB as Database
    
    UI->>API: POST /api/register
    API->>Service: RegisterUser(request)
    Service->>DB: CreateUser(user)
    DB-->>Service: User created
    Service-->>API: UserResult
    API-->>UI: 200 OK
```

### State Diagram - State Transitions

**Use for**: Features with distinct states and transitions

**Example**:
```mermaid
stateDiagram-v2
    [*] --> Draft
    Draft --> Submitted: User submits
    Submitted --> UnderReview: System processes
    UnderReview --> Approved: Manager approves
    UnderReview --> Rejected: Manager rejects
    Approved --> [*]
    Rejected --> Draft: User can revise
```

### Graph (Tree) - Work Breakdown Structure

**Use for**: Task hierarchy, component relationships

**Example**:
```mermaid
graph TD
    Feature[User Registration]
    Feature --> Frontend[Frontend Tasks]
    Feature --> Backend[Backend Tasks]
    
    Frontend --> UI1[Registration Form]
    Frontend --> UI2[Validation UI]
    
    Backend --> API1[Registration Endpoint]
    Backend --> SVC1[User Service]
```

## Best Practices

### Keep Diagrams Focused

Each diagram should tell one story - don't try to show everything in one diagram.

### Use Consistent Node IDs

Pick a naming convention and stick to it: camelCase, PascalCase, or snake_case.

### Meaningful Labels

Node labels should clearly describe what the node represents.

### Align with Phase Purpose

- Phase 2: Focus on USER journey
- Phase 4: Focus on COMPONENT integration
- Phase 5: Focus on TASK breakdown
