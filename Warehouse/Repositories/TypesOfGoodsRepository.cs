using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Entities;

namespace Warehouse.Repositories
{
    public class TypesOfGoodsRepository : Repository<TypesOfGood>, ITypesOfGoodsRepository
    {
        public TypesOfGood GetTypeInfoWithMaxQuantityOfGoods()
        {
            var commandText = $"select top(1) TypesOfGoods.Id, Type, Sum(Quantity) as TotalQuantity from TypesOfGoods, Goods where TypesOfGoods.Id = Goods.TypeId group by TypesOfGoods.Id, Type order By TotalQuantity desc";
            var command = GetCommand(commandText);
            using (var reader = command.ExecuteReader())
            {
                var schema = reader.GetColumnSchema();
                if (!reader.Read()) return null;

                return Create(schema, reader);
            }
        }

        public TypesOfGood GetTypeInfoWithMinQuantityOfGoods()
        {
            var commandText = $"select top(1) TypesOfGoods.Id, Type, Sum(Quantity) as TotalQuantity from TypesOfGoods, Goods where TypesOfGoods.Id = Goods.TypeId group by TypesOfGoods.Id, Type order By TotalQuantity asc";
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
