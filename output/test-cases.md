# Functional, Negative, and Boundary Test Cases

## Functional Tests

### FT-01: Create ticket with valid data
- Scenario: Customer submits a valid title and description.
- Input: Title = "Login issue", Description = "Cannot log in after password reset."
- Expected result: Ticket is created successfully.
- Expected status: HTTP 201 Created (or equivalent success response).
- Expected data: Ticket ID is generated, title is saved, description is saved, status is Open.

### FT-02: Title is accepted when present
- Scenario: Customer provides a valid title.
- Input: Title = "Payment failure", Description = "Payment page errors on submit."
- Expected result: Ticket creation succeeds.

### FT-03: Description is accepted when present
- Scenario: Customer provides a valid description.
- Input: Title = "API timeout", Description = "Requests hang for 30+ seconds."
- Expected result: Ticket creation succeeds.

### FT-04: Status defaults to Open
- Scenario: Customer creates a ticket without specifying a status.
- Input: Title = "Dashboard not loading", Description = "Page is blank after login."
- Expected result: New ticket status is set to "Open".

### FT-05: Ticket ID is auto-generated
- Scenario: Customer creates a valid ticket.
- Input: Valid ticket payload.
- Expected result: A unique ticket ID is generated and returned with the created record.

## Negative Tests

### NT-01: Title is missing
- Scenario: Customer submits a request with no title.
- Input: Title = empty string or null, Description = "Application crashes on startup."
- Expected result: Ticket creation fails with validation error for title.

### NT-02: Title is whitespace only
- Scenario: Customer submits a title containing only spaces.
- Input: Title = "   ", Description = "System freezes after upload."
- Expected result: Ticket creation fails because title is invalid.

### NT-03: Description is missing
- Scenario: Customer submits a request with no description.
- Input: Title = "Printer not working", Description = empty string or null.
- Expected result: Ticket creation fails with validation error for description.

### NT-04: Description is whitespace only
- Scenario: Customer submits description containing only spaces.
- Input: Title = "Printer not working", Description = "   ".
- Expected result: Ticket creation fails because description is invalid.

### NT-05: Both title and description missing
- Scenario: Customer submits an empty payload.
- Input: Title = null, Description = null.
- Expected result: Ticket creation fails and validation errors are returned for both fields.

### NT-06: Extra fields are ignored or rejected appropriately
- Scenario: Client sends more fields than expected.
- Input: Title = "Cannot export report", Description = "CSV export fails.", Additional unknown property.
- Expected result: System either ignores unsupported fields or rejects the payload without creating a ticket.

## Boundary Tests

### BT-01: Minimum valid title length
- Scenario: Customer submits the shortest valid title.
- Input: Title = "A", Description = "Issue description."
- Expected result: Ticket is accepted.

### BT-02: Maximum valid title length
- Scenario: Customer submits a title at the maximum allowed length.
- Input: Title = 200-character string, Description = "Issue description."
- Expected result: Ticket is accepted.

### BT-03: Minimum valid description length
- Scenario: Customer submits the shortest valid description.
- Input: Title = "Issue", Description = "A"
- Expected result: Ticket is accepted.

### BT-04: Maximum valid description length
- Scenario: Customer submits a description at the maximum allowed length.
- Input: Title = "Issue", Description = 2000-character string.
- Expected result: Ticket is accepted.

### BT-05: Title length exceeds limit
- Scenario: Customer submits title longer than 200 characters.
- Input: Title = 201-character string, Description = "Issue description."
- Expected result: Ticket creation fails with a validation message.

### BT-06: Description length exceeds limit
- Scenario: Customer submits description longer than 2000 characters.
- Input: Title = "Issue", Description = 2001-character string.
- Expected result: Ticket creation fails with a validation message.

### BT-07: Duplicate ticket IDs are not generated
- Scenario: Two valid tickets are created rapidly.
- Input: Two separate ticket submissions.
- Expected result: Each ticket gets a unique ID; no duplicate IDs are created.

### BT-08: Default status remains Open on valid creation
- Scenario: Two separate valid tickets are created without explicit status.
- Input: Valid ticket payload in two requests.
- Expected result: Both records persist with status = "Open".

## Acceptance Coverage Map

- AC-01 => FT-01, FT-02, FT-03
- AC-02 => NT-01, NT-02, BT-05
- AC-03 => NT-03, NT-04, BT-06
- AC-04 => FT-04, BT-08
- AC-05 => FT-05, BT-07
- AC-06 => FT-01, FT-05

## Summary
These tests cover successful creation, required-field validation, whitespace handling, maximum and minimum boundary values, and unique ID generation for the ticket creation requirement.