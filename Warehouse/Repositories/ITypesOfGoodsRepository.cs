using System;
using Warehouse.Entities;

namespace Warehouse.Repositories
{
    public interface ITypesOfGoodsRepository: IRepository<TypesOfGood>, IDisposable
    {
        public TypesOfGood GetTypeInfoWithMaxQuantityOfGoods();
        public TypesOfGood GetTypeInfoWithMinQuantityOfGoods();
    }
}
