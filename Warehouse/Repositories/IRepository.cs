using System.Collections.Generic;
using Warehouse.Entities;

namespace Warehouse.Repositories
{
    public interface IRepository<T> where T:BaseEntity
    {
        IEnumerable<T> Get();
        T Get(int Id);
        void Add(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}
