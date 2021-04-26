using System;
using Warehouse.Entities;

namespace Warehouse.Repositories
{
    public interface ISuppliersRepository: IRepository<Supplier>, IDisposable
    {
        public Supplier GetSupplierInfoWithMaxQuantityOfGoods();
        public Supplier GetSupplierInfoWithMinQuantityOfGoods();
    }
}
