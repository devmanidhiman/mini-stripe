ADR – 001: Choose PostgreSQL as the Primary Database for the Payment System
Status: Accepted
Context: The Mini stripe project is a data intensive application that stores merchants, Customers, and Payment Intent. The data model is highly relational, Merchants receive payments, Customers initiate them, PaymentIntents record the transactions, and we need to support queries that can join across multiple entities (Merchants, customers, payments, etc.)
Key requirements:
1.	Strong consistency for balances and transactions
2.	Clear Schema and constraints for data integrity
3.	Support for complex queries and reporting
Decision: We will use PostgreSQL relational database as operational database.
Reasoning:
1.	Relational database fits the domain. The core entities (Customer, merchants and accounts) have clear relationships and many-to-one / many-to-many links. A relational database model with foreign keys and joins matches this structure.
2.	Strong consistency and ACID transactions. PostgreSQL provides robust ACID guarantee, which is critical for financial and transactional data where lost or inconsistent writes are unacceptable.
3.	Schema and data integrity. A strict schema, constraints, and types help prevent invalid data from entering the system and make refactoring safer over time
Why PostgreSQL:
1.	Open source and Free.
2.	Docker Friendly, the official docker image is reliable and well documented.
3.	Feature set with full ACID compliance.
Consequences:
1.	Positives
a.	Easier to model and query complex relationships.
b.	Strong guarantees around data integrity and transactions.
c.	Leverage SQL skills and mature tooling.
2.	Negatives:
a.	Schema migrations are required when PaymentIntent fields change — this is acceptable in Phase 1 but will need tooling (EF Core migrations) as the schema evolves.
b.	Horizontal sharding is more complex than some in some NoSQL systems.
