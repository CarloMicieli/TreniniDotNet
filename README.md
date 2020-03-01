# Trenini.Net

![.NET Core](https://github.com/CarloMicieli/TreniniDotNet/workflows/.NET%20Core/badge.svg)

A database for model railways collections (and a Restful web api).

## Getting Started

### Prerequisites

This application is using:

- .NET Core 3.1
- Postgres SQL 12

### Postgres & Database init

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

Create a new database (development *only*):

```
postgres=# CREATE DATABASE TreniniDb;
CREATE DATABASE
postgres=# CREATE USER tdbuser WITH ENCRYPTED PASSWORD 'tdbpass';
CREATE ROLE
postgres=# ALTER USER tdbuser CREATEDB;
GRANT
```

Run the migrations

```
$ dotnet ef database update --project Src/Web --context ApplicationIdentityDbContext
$ dotnet ef database update --project Src/Web --context ApplicationDbContext
```

Run the scripts to seed the database:

```
$ export PGPASSWORD='tdbpass'; psql -U tdbuser -h localhost -d TreniniDb -a -f ./_Init/brands.sql
$ export PGPASSWORD='tdbpass'; psql -U tdbuser -h localhost -d TreniniDb -a -f ./_Init/scales.sql
$ export PGPASSWORD='tdbpass'; psql -U tdbuser -h localhost -d TreniniDb -a -f ./_Init/railways.sql
```

## Run the application

```
$ dotnet run --project Src/Web
[18:57:51 INF] User profile is available. Using '/home/carlo/.aspnet/DataProtection-Keys' as key repository; keys will not be encrypted at rest.
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

* [.NET Core 3.1](http://dot.net) - Back end
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
