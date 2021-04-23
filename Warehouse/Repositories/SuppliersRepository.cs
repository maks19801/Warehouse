using System;
using System.Collections.Generic;
using System.Text;
using Warehouse.Entities;

namespace Warehouse.Repositories
{
    public class SuppliersRepository : Repository<Supplier>, ISuppliersRepository
    {
        public Supplier GetSupplierInfoWithMaxQuantityOfGoods()
        {
            var commandText = $"select top(1) Suppliers.Id, Suppliers.Name, Sum(Quantity) as TotalQuantity from Suppliers, Goods where Suppliers.Id = Goods.SupplierId group by Suppliers.Id, Suppliers.Name order By TotalQuantity desc";
            var command = GetCommand(commandText);
            using (var reader = command.ExecuteReader())
            {
                var schema = reader.GetColumnSchema();
                if (!reader.Read()) return null;

                return Create(schema, reader);
            }
        }

        public Supplier GetSupplierInfoWithMinQuantityOfGoods()
        {
            var commandText = $"select top(1) Suppliers.Id,Suppliers.Name, Sum(Quantity) as TotalQuantity from Suppliers, Goods where Suppliers.Id = Goods.SupplierId group by Suppliers.Id,Suppliers.Name order By TotalQuantity asc";
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
