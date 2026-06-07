# ADR-002: Adopt Clean Architecture

**Status**: Accepted

---

## Context

Mini-Stripe is a payment processing system where correctness of business rules is critical. We needed an architecture that keeps core payment logic isolated from infrastructure concerns such as databases, message queues, external APIs, so that business rules can be reasoned about, tested, and changed independently of the surrounding technology.

Key requirements:
- Source code dependencies must strictly point inward means outer layers depend on inner layers, never the reverse
- Each layer must be testable in isolation without spinning up external dependencies
- Changes to infrastructure (e.g. swapping databases) must not require changes to business logic

## Decision

Adopt Clean Architecture with four distinct layers:

```
API → Application → Domain
Infrastructure → Domain
```

- **Domain** —> core business logic, entities, value objects, repository interfaces
- **Application** —> use case orchestration, commands, queries
- **Infrastructure** —> database, caching, messaging implementations
- **API** —> HTTP endpoints, request/response DTOs, middleware

Domain never points outward. It has zero knowledge of PostgreSQL, Redis, or any infrastructure detail.

## Reasoning

**Framework and database independence**
Core business logic does not depend on external tools. Technologies can be swapped without touching business rules for example, replacing PostgreSQL with another database only requires changes in the Infrastructure layer.

**Testability**
Because inner layers have no dependencies on outer layers, payment rules can be unit tested in complete isolation without a database, message queue, or HTTP server.

**Business logic first**
The structure forces domain rules to be modelled explicitly before implementation details. Code reflects the actual payment domain rather than framework conventions.

## Consequences

**Positives**
- Core payment rules (idempotency, state transitions) are testable in complete isolation
- Infrastructure is a plugin — payment gateways and databases can be swapped without touching business logic
- Clear boundaries make the codebase easier to reason about and extend

**Negatives**
- More projects and files than a simple layered architecture. Higher initial setup cost
- Strict layering adds indirection that can feel verbose for simple operations
