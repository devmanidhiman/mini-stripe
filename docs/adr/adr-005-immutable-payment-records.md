# ADR-005: Immutable Payment Records

**Status**: Accepted

---

## Context

Payment processing involves communication between multiple independent systems — in a real system, payer banks, payment networks, and merchant banks. These systems can fail independently. A common failure mode is money leaving the payer's account without confirmation ever reaching the merchant. When the outside world gives an inconsistent or incomplete answer, the system needs a reliable record of what was requested and what was known at each point in time.

There is also a non-negotiable requirement specific to financial systems: transaction records must be auditable. Deleting or silently overwriting a payment record removes the ability to reconstruct what happened, which is unacceptable both from an engineering and a compliance standpoint.

Key requirements:
- A payment record must never be deleted once created
- State changes must be explicit and auditable, not silent overwrites
- Invalid state transitions (e.g. completing an already-failed payment) must be rejected, not allowed to occur silently

## Decision

PaymentIntent records are append-only and immutable from the outside. Properties are not publicly settable. State transitions happen only through explicit methods (`Complete()`, `Fail()`, `Cancel()`) defined on the entity itself, each of which:

- Validates that the payment is currently in a `Pending` state before transitioning
- Throws an exception if an invalid transition is attempted
- Sets both the new status and a `CompletedAt` timestamp atomically

## Reasoning

**Distributed system failures require a source of truth**
When external systems disagree or fail to respond, the payment record is the authoritative account of what was requested and what is known. It is never deleted, even if the outcome is uncertain.

**Compliance and audit trail**
Financial systems are required to maintain a complete history of transactions. Immutability is not just good engineering practice here — it is close to a regulatory expectation.

**Guarding against invalid state transitions**
A payment that has already succeeded or failed should never be allowed to transition again. Enforcing this at the entity level (rather than trusting calling code) prevents data corruption from being possible at all, rather than relying on discipline elsewhere in the system.

## Consequences

**Positives**
- The system always has a reliable, auditable record of every payment attempt, regardless of how processing concluded
- Invalid state transitions are impossible by construction, not just discouraged by convention
- Debugging production issues is easier — the full history of a payment's state is never lost

**Negatives**
- Correcting genuine data entry mistakes (e.g. a payment created with the wrong amount) requires creating a new record and marking the old one cancelled, rather than editing in place
- Slightly more verbose entity code (explicit transition methods) compared to simple property setters
