# Trenini.Net

![.NET Core](https://github.com/CarloMicieli/TreniniDotNet/workflows/.NET%20Core/badge.svg)

![Logo](logo.png)

An application to manage model railway collections.

## Use cases

* Create a new **brand**;
* Create a new **railway**
* Create a new **scale**
* Create a new **catalog item**, with one or more **rolling stock** included
* Find all **brands**, with paginated results
* Find all **railways**, with paginated results
* Find all **scales**, with paginated results
* Find a **brand**, with its SEO friendly ("slug") identifier
* Find a **railway**, with its SEO friendly ("slug") identifier
* Find a **scale**, with its SEO friendly ("slug") identifier
* Find a **catalog item**, with its SEO friendly ("slug") identifier

## Project layout

* `Common` contains the interfaces and utilities classes that don't have a clear place in other projects.
* `Domain` contains the domain model, with all factories required to create new values. Domain objects are immutable, and the factories are using the `Validation` type from **lang ext** to validate the input. 
* `Application` contains the application services, and the **use cases** implementation. At this layer, we abstract the database access and depend on this abstraction. Business logic is cleanly separated from infrastructure details.
* `Infrastructure` is the place where **use cases** are getting their persistence requirement satisfied. The database access is using plain old sql and dapper.
* `Web` is the top layer - use cases are orchestrated using the IMediatr library and everything is wired together via the built-in dependency injection.

Tests project are in the `Tests` directory.

* `TestHelpers` has the main goal to provide test data. It is getting boring to make the same values for tests over and over again.
* `Common.UnitTests` contains unit tests for the `Common` project.
* `Domain.UnitTests` contains unit tests for the `Domain` project. The more relevant tests here are the ones validating domain object constraints and factories.
* `Application.UnitTests` contains unit tests for the `Application` project. Here we are testing the use cases, running the real use case handlers and services - only persistence is running against an in-memory implementation (just plain .NET collections).
* `IntegrationTests` - this project contains integration tests, here the application runs inside a fake web container - the persistence code is running against a "real" database (SQLite - with a database file stored on disk). The integration tests have the main goal to validate web apis.

## Setup

### Requirements

- `.NET Core 3.1.2`
- `Postgres SQL 12`

### Setup

#### Postgres

Add the PostgreSQL 12 repository

```
$ wget --quiet -O - https://www.postgresql.org/media/keys/ACCC4CF8.asc | sudo apt-key add -
$ echo "deb http://apt.postgresql.org/pub/repos/apt/ `lsb_release -cs`-pgdg main" |sudo tee  /etc/apt/sources.list.d/pgdg.list
```

Install both server and client:

```
$ sudo apt update
$ sudo apt -y install postgresql-12 postgresql-client-12
```

### Database init

Create a new database (development *only*):

```
postgres=# CREATE DATABASE TreniniDb;
CREATE DATABASE
postgres=# CREATE USER tdbuser WITH ENCRYPTED PASSWORD 'tdbpass';
CREATE ROLE
postgres=# ALTER USER tdbuser CREATEDB;
GRANT
```

When the application is running with `ASPNETCORE_ENVIRONMENT=Development` mode, both database migration and seeding will run (the first time).

## Run the application

```
$ dotnet run --project Src/Web
[18:57:52 INF] Now listening on: http://localhost:5000
[18:57:52 INF] Now listening on: https://localhost:5001
[18:57:52 INF] Application started. Press Ctrl+C to shut down.
[18:57:52 INF] Hosting environment: Production
[18:57:52 INF] Content root path: /home/carlo/Projects/TreniniDotNet/Src/Web
```

The Swagger documentation page is at [https://localhost:5001/swagger](https://localhost:5001/swagger).

## Running the tests

This command will run all tests (unit tests and integration tests):

```
$ dotnet test
```

## Built With

* [.NET Core 3.1.2](http://dot.net) - Back end
* [Angular](https://www.angular.io/) - Front end 

## Contributing

Please read [CONTRIBUTING.md](https://gist.github.com/PurpleBooth/b24679402957c63ec426) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/CarloMicieli/TreniniDotNet/tags). 

## Authors

* **Carlo Micieli** - *Initial work* - [CarloMicieli](https://github.com/CarloMicieli)

See also the list of [contributors](https://github.com/CarloMicieli/TreniniDotNet/contributors) who participated in this project.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* Hat tip to anyone whose code was used
* Inspiration
* etc
