# Trenini.net

![GitHub](https://img.shields.io/github/license/CarloMicieli/dotnetcore-clean-architecture)
![GitHub last commit](https://img.shields.io/github/last-commit/CarloMicieli/dotnetcore-clean-architecture)
![.NET Core](https://github.com/CarloMicieli/dotnetcore-clean-architecture/workflows/.NET%20Core/badge.svg)
[![Coverage Status](https://coveralls.io/repos/github/CarloMicieli/dotnetcore-clean-architecture/badge.svg?branch=master)](https://coveralls.io/github/CarloMicieli/dotnetcore-clean-architecture?branch=master)

An application to manage model railway collections.

## Requirements

- `.NET Core 3.1.x`
- `Postgres SQL 12`

## Setup

### .NET Core

Follow instruction on https://dot.net or simply follow (for Ubuntu 20.04):

```
$ wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
$ sudo dpkg -i packages-microsoft-prod.deb
$ sudo apt-get update; \
  sudo apt-get install -y apt-transport-https && \
  sudo apt-get update && \
  sudo apt-get install -y dotnet-sdk-3.1
```

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

#### Database init

Create a new database (development *only*):

```
postgres=# CREATE DATABASE TreniniDb;
CREATE DATABASE
postgres=# CREATE USER tdbuser WITH ENCRYPTED PASSWORD 'tdbpass';
CREATE ROLE
postgres=# ALTER USER tdbuser CREATEDB;
GRANT
```

## Application

### Build the solution

```
$ dotnet build backend/TreniniDotNet.sln
```

### Build docker image

```
$ docker build backend/webapi.dockerfile -t treninidotnet-webapi:latest
```

NOTE: **sudo** is required on some configuration.

### Run all tests

```
$ dotnet test backend/TreniniDotNet.sln
```

### Run the web api

```
$ dotnet run --project backend/Src/Web
[18:57:52 INF] Now listening on: http://localhost:5000
[18:57:52 INF] Now listening on: https://localhost:5001
[18:57:52 INF] Application started. Press Ctrl+C to shut down.
[18:57:52 INF] Hosting environment: Production
```
