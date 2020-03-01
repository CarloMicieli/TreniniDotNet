# Trenini.Net

![.NET Core](https://github.com/CarloMicieli/TreniniDotNet/workflows/.NET%20Core/badge.svg)

A database for model railways collections (and a Restful web api).

## Getting Started

### Prerequisites

This application is using:

- .NET Core 3.1
- Postgres SQL 12

### Postgres 

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

Create a new database:

```
postgres=# CREATE DATABASE TreniniDb;
CREATE DATABASE
postgres=# CREATE USER tdbuser WITH ENCRYPTED PASSWORD 'tdbpass';
CREATE ROLE
postgres=# GRANT ALL PRIVILEGES ON DATABASE TreniniDb to tdbuser;
GRANT
```


## Running the tests

```
dotnet test
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
