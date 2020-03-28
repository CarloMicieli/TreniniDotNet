using Dapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.Infrastracture.Dapper;

namespace TreniniDotNet.Infrastructure.Database.Testing
{
    public sealed class DatabaseAssert
    {
        public IDatabaseContext DatabaseContext { get; }

        public DatabaseAssert(IDatabaseContext databaseContext) =>
            DatabaseContext = databaseContext;

        public void RowExists(string tableName, object obj)
        {
        }

        public DatabaseAssertionBuilder RowIn(string tableName)
        {
            return new DatabaseAssertionBuilder(DatabaseContext, tableName);
        }

        public sealed class DatabaseAssertionBuilder
        {
            private object primaryKeys;
            private object values;

            private string TableName { get; }

            public DatabaseAssertionBuilder(IDatabaseContext databaseContext, string tableName)
            {
                DatabaseContext = databaseContext;
                TableName = tableName;
            }

            public IDatabaseContext DatabaseContext { get; }

            public DatabaseAssertionBuilder WithPrimaryKey(object obj)
            {
                primaryKeys = obj;
                return this;
            }

            public DatabaseAssertionBuilder WithValues(object obj)
            {
                values = obj;
                return this;
            }

            public DatabaseAssertionBuilder AndPrimaryKey(object obj)
            {
                primaryKeys = obj;
                return this;
            }

            public DatabaseAssertionBuilder AndValues(object obj)
            {
                values = obj;
                return this;
            }

            public void ShouldExists()
            {
                string selectText = BuildQueryText();

                try
                {
                    using var connection = DatabaseContext.NewConnection();
                    dynamic result = connection.QuerySingle(selectText, primaryKeys);

                    IDictionary<string, object> resultValues = result;
                    IDictionary<string, object> expectedValues = values
                        .GetType()
                        .GetProperties()
                        .ToDictionary(it => it.Name, it => it.GetValue(values));

                    if (expectedValues.Count > 0)
                    {
                        resultValues.Should().BeEquivalentTo(expectedValues);
                    }
                }
                catch (InvalidOperationException ex)
                {
                    if (ex.Message == "Sequence contains no elements")
                    {
                        throw new DatabaseAssertionException("Expected one result - found none");
                    }
                }
            }

            public void ShouldNotExists()
            {
                string selectText = BuildQueryText();
                using var connection = DatabaseContext.NewConnection();
                var results = connection.Query(selectText, primaryKeys);

                if (results.Count() > 0)
                {
                    throw new DatabaseAssertionException($"Expected to found no result - found {results.Count()} row(s) instead");
                }
            }

            private string BuildQueryText()
            {
                string select = "";
                if (values != null)
                {
                    select += string.Join(", ",
                        values.GetType()
                            .GetProperties()
                            .Select(it => it.Name));
                }

                if (string.IsNullOrEmpty(select))
                {
                    select = "*";
                }

                string where = string.Join("AND ",
                    primaryKeys.GetType()
                        .GetProperties()
                        .Select(it => $"{it.Name} = @{it.Name}"));

                return $"SELECT {select} FROM {TableName} WHERE {where}";
            }
        }
    }
}
