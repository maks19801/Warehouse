using System.Collections.Generic;
using Warehouse.Entities;

namespace Warehouse.Repositories
{
    public class GoodsRepository: Repository<Good>, IGoodsRepository
    {
        public IEnumerable<GoodsQuantityByType> GetAvgGoodsQuantityByType()
        {
            var results = new List<GoodsQuantityByType>();

            var commandText = $"SELECT TypeId, AVG(Quantity) as AvgQuantity FROM {TableName} group by TypeId";
            var command = GetCommand(commandText);


            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var item = new GoodsQuantityByType();
                    for(int i = 0; i < reader.FieldCount; i++)
                    {
                        item.TypeId = (int)reader[0];
                        item.AverageQuantity = (int)reader[1];
                    }
                    results.Add(item);
                }

                return results;
            }
        }

        public IEnumerable<Good> GetGoodsByPassedDays(int passedDays)
        {
            var results = new List<Good>();

            var commandText = $"SELECT * FROM {TableName} WHERE DATEDIFF(day, DeliveryDate, GETDATE()) < @passedDays";
            var command = GetCommand(commandText);
            command.Parameters.AddWithValue("PassedDays", passedDays);

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

        public IEnumerable<Good> GetGoodsBySupplier(int supplierId)
        {
            var results = new List<Good>();

            var commandText = $"SELECT * FROM {TableName} WHERE SupplierId = @supplierId";
            var command = GetCommand(commandText);
            command.Parameters.AddWithValue("SupplierId", supplierId);

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

        public IEnumerable<Good> GetGoodsByType(int typeId)
        {
            var results = new List<Good>();

            var commandText = $"SELECT * FROM {TableName} WHERE TypeId = @typeId";
            var command = GetCommand(commandText);
            command.Parameters.AddWithValue("TypeId", typeId);

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

        public Good GetMaxCostGood()
        {
            var commandText = $"SELECT * FROM {TableName} where Cost = (select max(cost) from {TableName})";
            var command = GetCommand(commandText);
            using (var reader = command.ExecuteReader())
            {
                var schema = reader.GetColumnSchema();
                if (!reader.Read()) return null;

                return Create(schema, reader);
            }
        }

        public Good GetMaxQuantityGood()
        {
            var commandText = $"SELECT * FROM {TableName} where Quantity = (select max(quantity) from {TableName})";
            var command = GetCommand(commandText);
            using (var reader = command.ExecuteReader())
            {
                var schema = reader.GetColumnSchema();
                if (!reader.Read()) return null;

                return Create(schema, reader);
            }        
        }

        public Good GetMinCostGood()
        {
            var commandText = $"SELECT * FROM {TableName} where Cost = (select min(Cost) from {TableName})";
            var command = GetCommand(commandText);
            using (var reader = command.ExecuteReader())
            {
                var schema = reader.GetColumnSchema();
                if (!reader.Read()) return null;

                return Create(schema, reader);
            }
        }

        public Good GetMinQuantityGood()
        {
            var commandText = $"SELECT * FROM {TableName} where Quantity = (select min(quantity) from {TableName})";
            var command = GetCommand(commandText);
            using (var reader = command.ExecuteReader())
            {
                var schema = reader.GetColumnSchema();
                if (!reader.Read()) return null;

                return Create(schema, reader);
            }
        }

        public Good GetTheOldestGood()
        {
            var commandText = $"SELECT * FROM {TableName} where DeliveryDate = (select min(DeliveryDate) from {TableName})";
            var command = GetCommand(commandText);
            using (var reader = command.ExecuteReader())
            {
                var schema = reader.GetColumnSchema();
                if (!reader.Read()) return null;

                return Create(schema, reader);
            }
        }
    }
}
