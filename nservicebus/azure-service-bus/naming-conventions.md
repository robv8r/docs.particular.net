---
title: Azure Service Bus Transport Naming Conventions
summary: Naming Conventions for Azure Service Bus, Versions 6 and below.
tags:
 - Azure
 - Cloud
---


## Versions 6 and below


### Naming Conventions

To have a fine-grained control over entity names generated by the ASB transport, `NamingConventions` class exposes several conventions that can be overwritten to provide customization.

Entity sanitization:

snippet: ASB-NamingConventions-entity-sanitization

This sanitization allows forward slash `/` in queue and topic names unlike default sanitization convention used by Azure Service Bus transport.

Entities creation:

snippet: ASB-NamingConventions-entity-creation-conventions

WARNING: This is an advanced topic and requires full understanding of the topology.
