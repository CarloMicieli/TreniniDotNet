using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Dapper;
using FluentAssertions;
using TreniniDotNet.Infrastructure.Dapper;

namespace TreniniDotNet.Infrastructure.Database.Testing
{
    public sealed class DatabaseAssert
    {
        public IConnectionProvider ConnectionProvider { get; }

        public DatabaseAssert(IConnectionProvider connectionProvider) =>
            ConnectionProvider = connectionProvider;

        public DatabaseAssertionBuilder RowInTable(string tableName)
        {
            return new DatabaseAssertionBuilder(ConnectionProvider, tableName);
        }

        public sealed class DatabaseAssertionBuilder
        {
            private object _primaryKeys;
            private object _values;

            private string TableName { get; }

            public DatabaseAssertionBuilder(IConnectionProvider connectionProvider, string tableName)
            {
                ConnectionProvider = connectionProvider;
                TableName = tableName;
            }

            public IConnectionProvider ConnectionProvider { get; }

            public DatabaseAssertionBuilder WithPrimaryKey(object obj)
            {
                _primaryKeys = obj;
                return this;
            }

            public DatabaseAssertionBuilder WithValues(object obj)
            {
                _values = obj;
                return this;
            }

            public DatabaseAssertionBuilder AndPrimaryKey(object obj)
            {
                _primaryKeys = obj;
                return this;
            }

            public DatabaseAssertionBuilder AndValues(object obj)
            {
                _values = obj;
                return this;
            }

            public void ShouldExists()
            {
                var selectText = BuildQueryText();

                try
                {
                    using var connection = ConnectionProvider.Create();
                    dynamic result = connection.QuerySingle(selectText, _primaryKeys);

                    IDictionary<string, object> resultValues = result;

                    if (_values != null)
                    {
                        IEnumerable<PropertyInfo> props = _values
                            .GetType()
                            .GetProperties();

                        IDictionary<string, object> expectedValues = props
                            .ToDictionary(it => it.Name, it => it.GetValue(_values));

                        // Sqlite has no boolean data type, need to change it before the assertion
                        var booleanFields = props
                            .Where(it => it.PropertyType == typeof(bool) || it.PropertyType == typeof(bool?))
                            .Select(it => it.Name)
                            .ToList();

                        if (booleanFields.Count > 0)
                        {
                            foreach (var field in booleanFields)
                            {
                                if (resultValues.TryGetValue(field, out var value))
                                {
                                    resultValues[field] = "1" == value?.ToString();
                                }
                            }
                        }

                        var uuidFields = props
                            .Where(it => it.PropertyType == typeof(Guid) || it.PropertyType == typeof(Guid?))
                            .Select(it => it.Name)
                            .ToList();

                        if (uuidFields.Count > 0)
                        {
                            foreach (var field in uuidFields)
                            {
                                if (resultValues.TryGetValue(field, out var value) &&
                                    value != null)
                                {
                                    resultValues[field] = Guid.Parse(value.ToString());
                                }
                            }
                        }

                        // Made DateTime to be UTC 

                        var dateTimeFields = props
                            .Where(it => it.PropertyType == typeof(DateTime) || it.PropertyType == typeof(DateTime?))
                            .Select(it => it.Name)
                            .ToList();

                        if (dateTimeFields.Count > 0)
                        {
                            var expectedUtcDateTimes = expectedValues
                                .Where(it => IsUtcDateTime(it.Value))
                                .Select(it => it.Key)
                                .ToHashSet();

                            foreach (var field in dateTimeFields)
                            {
                                if (resultValues.TryGetValue(field, out var value))
                                {
                                    if (!(value is null) && expectedUtcDateTimes.Contains(field))
                                    {
                                        resultValues[field] = DateTime.SpecifyKind(
                                            Convert.ToDateTime(value), DateTimeKind.Utc);
                                    }
                                }
                            }
                            
                            var expectedUnspecifiedDateTimes = expectedValues
                                .Where(it => IsUtcDateTime(it.Value) == false)
                                .Select(it => it.Key)
                                .ToHashSet();

                            foreach (var field in dateTimeFields)
                            {
                                if (resultValues.TryGetValue(field, out var value))
                                {
                                    if (!(value is null) && expectedUnspecifiedDateTimes.Contains(field))
                                    {
                                        resultValues[field] = DateTime.SpecifyKind(
                                            Convert.ToDateTime(value), DateTimeKind.Unspecified);
                                    }
                                }
                            }
                        }

                        if (expectedValues.Count > 0)
                        {
                            foreach (var (key, expected) in expectedValues)
                            {
                                if (resultValues.TryGetValue(key, out var actual))
                                {
                                    actual.Should().Be(expected, "Value for {0} is different", key);
                                }
                            }
                        }
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
                var selectText = BuildQueryText();
                using var connection = ConnectionProvider.Create();
                var results = connection.Query(selectText, _primaryKeys);

                if (results.Count() > 0)
                {
                    throw new DatabaseAssertionException($"Expected to found no result - found {results.Count()} row(s) instead");
                }
            }

            private string BuildQueryText()
            {
                var select = "";
                if (_values != null)
                {
                    select += string.Join(", ",
                        _values.GetType()
                            .GetProperties()
                            .Select(it => it.Name));
                }

                if (string.IsNullOrEmpty(select))
                {
                    select = "*";
                }

                var where = string.Join(" AND ",
                    _primaryKeys.GetType()
                        .GetProperties()
                        .Select(it => $"{it.Name} = @{it.Name}"));

                return $"SELECT {select} FROM {TableName} WHERE {where}";
            }

            private static bool IsUtcDateTime(object obj) =>
                (obj is DateTime that && that.Kind == DateTimeKind.Utc);
        }
    }
}
