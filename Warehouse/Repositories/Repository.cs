using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using Warehouse.Entities;

namespace Warehouse.Repositories
{
    public class Repository<T> : IRepository<T>, IDisposable where T : BaseEntity, new()
    {
        protected readonly SqlConnection Connection;
        protected readonly string TableName = $"[{typeof(T).Name}s]";
        public Repository()
        {
            var connectionString = @"Data Source = WIN-JFQFP2A7GSE; Initial Catalog = Warehouse; Integrated Security = True;";
            Connection = new SqlConnection(connectionString);
            try
            {
                Connection.Open();
                Console.WriteLine("connection is open");
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        public void Add(T entity)
        {
            var properties = typeof(T)
                .GetProperties().Where(p => p.Name != "Id");

            var propNames = properties.Select(p => p.Name);
            var columnNames = string.Join(",", propNames);
            var paramNames = string.Join(",", propNames.Select(_ => $"@{_}"));

            var commandText = $"INSERT INTO {TableName} ({columnNames}) VALUES ({paramNames})";
            var command = GetCommand(commandText);

            foreach (var property in properties)
            {
                command.Parameters.AddWithValue(property.Name, property.GetValue(entity));
            }

            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            var commandText = $"DELETE FROM {TableName} WHERE Id = @id";
            var command = GetCommand(commandText);
            command.Parameters.AddWithValue("id", id);
            command.ExecuteNonQuery();
        }

        public void Dispose()
        {
            Connection.Dispose();
        }

        public T Get(int Id)
        {
            var commandText = $"SELECT * FROM {TableName} WHERE Id = @Id";
            var command = GetCommand(commandText);
            command.Parameters.AddWithValue("Id", Id);

            using (var reader = command.ExecuteReader())
            {
                var schema = reader.GetColumnSchema();
                if (!reader.Read()) return null;

                return Create(schema, reader);
            }
        }
        public IEnumerable<T> Get()
        {
            var results = new List<T>();

            var commandText = $"SELECT * FROM {TableName}";
            var command = GetCommand(commandText);

            using (var reader = command.ExecuteReader())
            {

                var schema = reader.GetColumnSchema();
                while (reader.Read())
                {
                    results.Add(Create(schema, reader));
                }

                return results;
            }
        }
       
        public void Update(T entity)
        {
            var properties = typeof(T)
                .GetProperties();

            var propNames = properties.Where(p => p.Name != "Id")
                .Select(p => p.Name);

            var setPropString = propNames.Select(n => $"{n} = @{n}");
            var setProps = string.Join(",", setPropString);

            var commandText = $"UPDATE {TableName} SET {setProps} WHERE Id = @Id";
            var command = GetCommand(commandText);

            foreach (var property in properties)
            {
                command.Parameters.AddWithValue(property.Name, property.GetValue(entity));
            }

            command.ExecuteNonQuery();
        }
        protected SqlCommand GetCommand(string command)
        {
            return new SqlCommand(command, Connection);
        }

        protected T Create(ReadOnlyCollection<DbColumn> schema, SqlDataReader reader)
        {
            var result = new T();

            for (int colIdx = 0; colIdx < schema.Count; colIdx++)
            {
                var columnName = schema[colIdx].ColumnName;
                var columnType = schema[colIdx].DataType;
                var columnValue = reader[colIdx];

                var value = Convert.ChangeType(columnValue, columnType);

                var property = typeof(T).GetProperties()
                    .FirstOrDefault(p => p.Name == columnName);

                if (property?.PropertyType == columnType)
                {
                    property.SetValue(result, value);
                }
            }

            return result;
        }
    }
}
