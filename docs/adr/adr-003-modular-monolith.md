# ADR-003: Adopt Modular Monolith Over Microservices

**Status**: Accepted

---

## Context

At the start of this project, the bounded contexts within the payment domain (Payments, Merchants, Customers, Notifications) had not been proven through actual implementation. Microservices require clear, stable service boundaries — if those boundaries are guessed wrong upfront, fixing them later means migrating data across separate service databases and renegotiating inter-service contracts, which is far more costly than refactoring code within a single codebase.

There were also no concrete drivers for microservices: no multiple teams working independently, no proven need for independent scaling of any single module, no deployment cadence requiring separation.

Key requirements:
- Module boundaries must be enforceable in code without distributed systems overhead
- The system must be able to evolve toward microservices later if a real scaling need emerges, without a full rewrite

## Decision

Build Mini-Stripe as a modular monolith, organized around bounded contexts:

```
Payments | Merchants | Customers | Notifications
```

Each module owns its own data and logic and communicates with other modules only through defined interfaces — never by directly querying another module's tables.

## Reasoning

**Boundaries weren't proven yet**
Bounded contexts can be designed on paper, but they're only proven once real code and real data flows expose where the actual seams are. Building modules within a monolith allows boundaries to be discovered and corrected cheaply.

**Distributed complexity without distributed benefit**
Microservices introduce network calls, service discovery, distributed tracing, and partial failure handling. None of that complexity is justified without an actual scaling or deployment driver.

**Refactoring is cheaper than re-architecting**
Moving code between modules in a monolith is a refactor. Splitting a wrongly-bounded microservice means migrating live data across services — a much riskier and more expensive operation.

## Consequences

**Positives**
- Module boundaries can be corrected cheaply as the domain is better understood
- No distributed systems overhead (network latency, partial failures, service discovery) for a system that doesn't yet need it
- Extraction to microservices remains possible later — the modules already define clear boundaries that could become service boundaries

**Negatives**
- All modules currently share a single deployment unit — modules can't be deployed or scaled independently
- Without discipline, module boundaries can erode over time (modules reaching into each other's data) if not enforced through code reviews and interface contracts
