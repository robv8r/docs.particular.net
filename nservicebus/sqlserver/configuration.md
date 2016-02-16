---
title: SQL Server Transport configuration
summary: SQL Server Transport configuration
tags:
- SQL Server
redirects:
- nservicebus/sqlserver/concurrency
---


## Connection strings

Connection string can be configured in several ways:


### Via the configuration API

By using the `ConnectionString` extension method.

snippet:sqlserver-config-connectionstring


### Via the App.Config

By adding a connection named `NServiceBus/Transport` in the `connectionStrings` node.
  
snippet:sqlserver-connection-string-xml


### Via a named connection string

By using the `ConnectionStringName` extension method.

snippet:sqlserver-named-connection-string

Combined with a named connection in the `connectionStrings` node of the `app.config` file.

snippet:sqlserver-named-connection-string-xml


## Sql Server Transport, the Outbox and user data: disabling the DTC

In an environment where DTC is disabled and [Outbox](/nservicebus/outbox/) is enabled, it is important to prevent a local transaction from escalating to a distributed one.

The following conditions need to be met:

* the business specific data and the `Outbox` storage must be in the same database;
* the user code accessing business related data must use the same `connection string` as the `Outbox` storage.


### [Entity Framework](https://msdn.microsoft.com/en-us/data/ef.aspx) caveats

In order to avoid escalating transaction to DTC when using Entity Framework, the database connection has to be shared. However, sharing the connection string can be problematic when dealing with entities based on the [Entity Framework ADO.Net Data Model (EDMX)](https://msdn.microsoft.com/en-us/library/vstudio/cc716685.aspx). 

The `DbContext` generated by Entity Framework does not expose a way to inject a simple database connection string. The underlying problem is that Entity Framework requires an `Entity Connection String` that contains more information than a simple connection string.

It is possible to generate a custom a custom `EntityConnection` and inject it into the Entity Framework `DbContext` instance:

snippet:EntityConnectionCreationAndUsage

In the snippet above the `EntityConnectionStringBuilder` class is used to create a valid `Entity Connection String`. Having that a new `EntityConnection` instance can be created.

The `DbContext` generated by default by Entity Framework does not have a constructor that accepts an `EntityConnection` as a parameter. Since it is a partial class we can add that parameter using the following snippet:

snippet:DbContextPartialWithEntityConnection

NOTE: The snippet above assumes that the created entity data model is named `MySample`. The references should match the actual names used in the project.


## Persistence

When the SQL Server transport is used in combination [NHibernate persistence](/nservicebus/nhibernate/) it allows for sharing database connections and optimizing transactions handling to avoid escalating to DTC. However, SQL Server Transport can be used with any other available persistence implementation.


## Transactions

SQL Server transport supports all [transaction handling modes](/nservicebus/messaging/transactions.md), i.e. Transaction scope, Receive only, Sends atomic with Receive and No transactions.

Refer to [Transport Transactions](/nservicebus/messaging/transactions.md) for detailed explanation of the supported transaction handling modes and available configuration options. 


## Callbacks

The settings mentioned below are available in version 2.x of the SQL Server transport. In version 3.x using callbacks requires the new `NServiceBus.Callbacks` NuGet package. Refer to [callbacks](/nservicebus/messaging/handling-responses-on-the-client-side.md) for more details.


### Disable callbacks

Callbacks and callback queues receivers are enabled by default. In order to disable them use the following setting:

snippet:sqlserver-config-disable-secondaries

Secondary queues use the same adaptive concurrency model as the primary queue. Secondary queues (and hence callbacks) are disabled for satellite receivers.


### Callback Receiver Max Concurrency

Changes the number of threads used for the callback receiver. The default is 1 thread.

snippet:sqlserver-CallbackReceiverMaxConcurrency


## Circuit Breaker

The Sql transport has a built in circuit breaker to handle intermittent SQL Server connectivity problems.


### Wait time

Overrides the default time to wait before triggering a circuit breaker that initiates the endpoint shutdown procedure in case of [repeated critical errors](/nservicebus/hosting/critical-errors.md).

The default value is 2 minutes.

snippet:sqlserver-TimeToWaitBeforeTriggeringCircuitBreaker


### Pause Time

Overrides the default time to pause after a failure while trying to receive a message. The setting is only available in version 2.x. The default value is 10 seconds.

snippet: sqlserver-PauseAfterReceiveFailure