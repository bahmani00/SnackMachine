

# DDD implementation of Snack Machine

## Overview

DDD implementation of Snack Machine, coming from [design-in-practice](https://app.pluralsight.com/library/courses/domain-driven-design-in-practice/) course.

## Give a Star! :star:
If you like or are using this project please give it a star. Thanks!

## Libraries used

* **Mediator pattern** - (my custom) process requests delegator inside [Application\Seedwork](https://github.com/bahmani00/SnackMachine/tree/master/SnackMachineApp.Application/Seedwork).
* **Dapper** - to query database
* **GuardClauses** - use [GuardClauses](https://github.com/ardalis/GuardClauses)
* **FluentValidation, xUnit** - for unit testing
* **AutoMapper** - define mappings between Domain Entities and DTOs.
* (Configs to setup either Ef or nHibernate as ORM)
* **NHibernate** - .Net ORM to access data.
* **Entity Framework Core** - MS .Net ORM to access data.
* (Configs to setup either LightInject or Autofac as IoC)
* **Autofac & .Net DependencyInjection** - Dependency Injection library to wire project's dependencies into .Net built-in DI(ServiceCollection).
* **LightInject & .Net DependencyInjection** - Dependency Injection library to wire project's dependencies into .Net built-in DI(ServiceCollection).

## Getting started

1. Clone the repo.
2. Run 'database.sql' on sql server.
3. Set ConnectionString on 'App.config'
3. Run the WPF project
4. Switch to either NHibernate or Ef through app.config.
5. Switch to either LightInject or Autofac through app.config.

## Vocabularies

1. Seedwork
2. Mediator
3. UnitOfWork
4. Domain Event
5. Anemic class
6. IoC

## TODOs

* Checkout [Issues](https://github.com/bahmani00/SnackMachine/issues)
* TODOs comments in the codebase
