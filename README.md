# Mini-Stripe

A payment processing system built to demonstrate production-minded backend engineering, distributed systems thinking, and full-stack ability.

---

## Tech Stack

| Layer | Technology |
|---|---|
| Backend | ASP.NET Core (C#) |
| Database | PostgreSQL |
| Cache | Redis |
| Message Queue | RabbitMQ (Phase 1–2) → Kafka (Phase 3+) |
| Frontend | React + TypeScript |
| Auth | JWT |
| Infra | Docker + Docker Compose |
| Docs | Swagger / OpenAPI |

---

## Architecture

This project follows **Clean Architecture** organized as a **modular monolith**.

### Why Clean Architecture

Clean Architecture enforces a strict dependency rule, outer layers depend on inner layers, never the reverse. For a payment system, this means the core business logic (domain) has zero knowledge of PostgreSQL, Redis, or any infrastructure detail. Payment rules can be tested in isolation, databases can be swapped without touching business logic, and the system can be reasoned about without holding the entire stack in my head.

The dependency direction is always:

```
API → Application → Domain
Infrastructure → Application → Domain
```

Domain never points outward.

### Why Modular Monolith

I went with a modular monolith because the bounded contexts weren't fully clear at the start. A modular monolith lets me enforce module boundaries without the operational overhead of distributed systems. Once the boundaries are proven and a specific module demonstrably needs independent scaling, it can be extracted — not before.

### Why We Stay Modular

The monolith is organized around proven bounded contexts. Payments, Merchants, Customers, Notifications. Each module owns its data and logic and communicates through defined interfaces. If a future team needed to extract a module as a service, the boundaries are already drawn, but we don't do that extraction speculatively. Independent scaling or deployment needs to be a demonstrated requirement, not an assumption.

---

## Payment Flow

Every payment moves through the following stages:

1. **Request Created** — A PaymentIntent is created when a customer initiates a payment or a merchant requests one. This is a record of intent only, no money has moved.
2. **Validated** — The intent is validated: does the merchant exist? Is the amount valid? Is the currency supported?
3. **Processing** — In a real system this is where bank servers are contacted. In this project it is simulated. The outcome is one of three states: `Succeeded`, `Failed`, or `Cancelled`.

### Phase 1 Endpoints

```
POST /payments              → Create a PaymentIntent (status: Pending)
POST /payments/{id}/confirm → Attempt processing   (status: Succeeded or Failed)
GET  /payments/{id}         → Retrieve payment status
```

### Why Two-Step (Create + Confirm)

Two reasons:

1. **Separation of concerns** — creating an intent and processing a payment are different responsibilities. One endpoint doing both jobs is harder to reason about, test, and extend.
2. **Deferred payments** — not all payments happen instantly. A merchant may issue a payment request due in two weeks. The two-step model handles this naturally.

### Why Immutable Records

Two reasons:

1. **Distributed system failures** — money can leave the payer's account without reaching the merchant. The payment record is the source of truth when the outside world gives an inconsistent answer.
2. **Legal and compliance** — financial systems are required to maintain full audit trails. Transaction records cannot be deleted even if you wanted to.

### Idempotency

The confirm endpoint is idempotent. Every request carries a client-generated idempotency key. Before processing, the server checks if this key has been seen before, if yes, it returns the stored result without reprocessing. This prevents double charges caused by network retries.

---

## Phases

### Phase 1 — MVP
Core payment flow, merchant and customer entities, basic transaction processing, idempotency keys, JWT auth, Swagger docs, Docker setup.

### Phase 2 — Core Features
Redis caching, webhook delivery system with retries, payment status tracking.

### Phase 3 — Reliability
RabbitMQ → Kafka migration, retry queues, dead letter queues, distributed transaction handling.

### Phase 4 — Advanced
Rate limiting, fraud detection hooks, reporting and analytics endpoints, multi-currency support.

---

## Running Locally

_Coming soon — Docker setup in progress._