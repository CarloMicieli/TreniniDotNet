using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;

namespace TreniniDotNet.Infrastructure.Dapper.Extensions.DependencyInjection
{
    public sealed class DapperOptions
    {
        private readonly IServiceCollection services;
        private readonly List<Assembly> assemblies;

        internal DapperOptions(IServiceCollection services)
        {
            this.services = services;
            this.assemblies = new List<Assembly>();
        }

        public void UsePostgres(string connectionString)
        {
            ConfigureDatabaseOptions(connectionString);
            services.AddScoped<IConnectionProvider, NpgsqlConnectionProvider>();
        }

        public void UseSqlite(string connectionString)
        {
            ConfigureDatabaseOptions(connectionString);
            services.AddScoped<IConnectionProvider, SqliteConnectionProvider>();
        }

        public void UseSqliteInMemory(Guid id)
        {
            var connectionString = new SqliteConnectionStringBuilder($"Data Source={id}.db")
            {
                ForeignKeys = true,
                Cache = SqliteCacheMode.Shared,
                Mode = SqliteOpenMode.ReadWriteCreate
            }.ToString();
           
            UseSqlite(connectionString);
        }

        public void ScanTypeHandlersIn(Assembly assembly)
        {
            this.assemblies.Add(assembly);
        }

        public void ScanTypeHandlersIn(params Assembly[] assemblies)
        {
            if (assemblies.Length > 0)
            {
                this.assemblies.AddRange(assemblies);
            }
        }

        internal List<Assembly> Assemblies
        {
            get
            {
                return assemblies;
            }
        }

        private void ConfigureDatabaseOptions(string connectionString)
        {
            services.Configure<DatabaseOptions>(myOptions =>
            {
                myOptions.ConnectionString = connectionString;
            });
        }
    }
}
