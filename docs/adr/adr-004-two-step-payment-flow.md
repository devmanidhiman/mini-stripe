# ADR-004: Two-Step Payment Flow (Create + Confirm)

**Status**: Accepted

---

## Context

A payment is not a single instantaneous event — it has distinct stages, and each stage can fail or be delayed independently. Two specific problems needed to be solved:

1. Payments are not always immediate. A merchant may request payment due at a future date, meaning the system needs to record intent before any money moves.
2. Creating a payment record and attempting to process that payment are different responsibilities. Combining them into a single operation makes the endpoint harder to reason about, test, and extend — for example, adding webhook notifications later should only require hooking into the processing step, not the creation step.

Key requirements:
- The system must be able to record a payment request before processing occurs
- Processing must be a separate, explicitly triggered step
- The design must support both immediate and deferred payment scenarios

## Decision

Implement payments as a two-step flow:

```
POST /payments              → Create a PaymentIntent (status: Pending)
POST /payments/{id}/confirm → Attempt processing (status: Succeeded or Failed)
GET  /payments/{id}         → Retrieve payment status
```

Creating a PaymentIntent only records intent — no money moves. Confirming a PaymentIntent attempts the actual transaction and transitions it to a terminal state.

## Reasoning

**Separation of concerns**
Creating an intent and processing a payment are distinct responsibilities. Keeping them as separate endpoints means each can be tested, validated, and extended independently.

**Support for deferred payments**
Not all payments resolve instantly. A two-step model naturally supports a payment being created now and confirmed later, without requiring a different code path for deferred scenarios.

**Cleaner extension points**
Future features (webhooks, retries, fraud checks) can hook into the confirm step without needing to touch the create step.

## Consequences

**Positives**
- Clear separation between recording intent and executing a transaction
- Naturally supports both instant and deferred payment scenarios without special-casing
- Confirm step is the natural place to add idempotency protection (see ADR for idempotency keys)

**Negatives**
- Requires two API calls for the simplest case (immediate payment), adding slight complexity to client integration
- Clients must handle and track the intermediate `Pending` state correctly
