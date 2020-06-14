using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Dapper;
using TreniniDotNet.Infrastructure.Dapper;

namespace TreniniDotNet.Infrastructure.Database.Testing
{
    public sealed class DatabaseArrange
    {
        public IDatabaseContext DatabaseContext { get; }

        public DatabaseArrange(IDatabaseContext databaseContext) =>
            DatabaseContext = databaseContext;

        public void InsertOne(string tableName, object obj)
        {
            if (string.IsNullOrWhiteSpace(tableName))
            {
                throw new ArgumentNullException(nameof(tableName));
            }

            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            var props = obj.GetType().GetProperties();
            string commandText = BuildInsertCommandText(tableName, props);

            int affectedRows = Execute(commandText, obj);
            if (affectedRows != 1)
            {
                throw new InvalidOperationException($"Expected 1 row to be inserted (it was {affectedRows})");
            }
        }

        public void Insert(string tableName, params object[] objects)
        {
            int expected = objects.Length;

            var props = objects[0].GetType().GetProperties();
            string commandText = BuildInsertCommandText(tableName, props);

            int affectedRows = 0;
            foreach (var obj in objects)
            {
                affectedRows += Execute(commandText, obj);
            }

            if (affectedRows != expected)
            {
                throw new InvalidOperationException($"Expected {expected} row(s) to be inserted (it was {affectedRows})");
            }
        }

        public void InsertMany(string tableName, int count, Func<int, object> f)
        {
            object[] objects = Enumerable.Range(1, count)
                .Select(id => f(id))
                .ToArray();
            this.Insert(tableName, objects);
        }

        private int Execute(string commandText, object obj)
        {
            using var connection = DatabaseContext.NewConnection();
            return connection.Execute(commandText, obj);
        }

        private static string BuildInsertCommandText(string tableName, IEnumerable<PropertyInfo> props)
        {
            var sb = new StringBuilder();
            sb.Append($"INSERT INTO {tableName}(");
            sb.Append(string.Join(", ", props.Select(it => it.Name)));
            sb.Append(")");
            sb.Append("VALUES(");
            sb.Append(string.Join(", ", props.Select(it => $"@{it.Name}")));
            sb.Append(");");
            return sb.ToString();
        }
    }
}
