using System;
using System.Collections.Generic;
using Warehouse.Entities;
using Warehouse.Repositories;

namespace Warehouse
{
    public class Program
    {
        static void Main(string[] args)
        {
            using IGoodsRepository repository = new GoodsRepository();

            var goodToAdd = new Good
            {
                Name = "Брынза",
                TypeId = 1,
                SupplierId = 2,
                Quantity = 31,
                Cost = 24.5m,
                DeliveryDate = new DateTime(2000, 01, 01),

            };
            //repository.Add(goodToAdd);
            //repository.Delete(8);
            var result = repository.Get();


            foreach (var item in result)
            {  
                Console.WriteLine(item.ToString()); 
            }

            var goodById = repository.Get(1);
            Console.WriteLine($" Good by Id: {goodById}");

            var goodsByTypeId = repository.GetGoodsByType(1);
            foreach (var item in goodsByTypeId)
            {
                Console.WriteLine(item.ToString());
            }

            var goodsBySupplierId = repository.GetGoodsBySupplier(2);
            foreach (var item in goodsBySupplierId)
            {
                Console.WriteLine(item.ToString());
            }

            goodById.Quantity = 15;

            repository.Update(goodById);
            Console.WriteLine($" Good by Id: {goodById}");

            var goodMaxQuantity = repository.GetMaxQuantityGood();
            Console.WriteLine($" Max quantity Good: Name: {goodMaxQuantity.Name} Quantity: {goodMaxQuantity.Quantity}");

            var goodMinQuantity = repository.GetMinQuantityGood();
            Console.WriteLine($" Min quantity Good: Name: {goodMinQuantity.Name} Quantity: {goodMinQuantity.Quantity}");

            var goodMaxCost = repository.GetMaxCostGood();
            Console.WriteLine($" Max Cost Good: Name: {goodMaxCost.Name} Cost: {goodMaxCost.Cost}");

            var goodMinCost = repository.GetMinCostGood();
            Console.WriteLine($" Min Cost Good: Name: {goodMinCost.Name} Cost: {goodMinCost.Cost}");

            var theOldestGood = repository.GetTheOldestGood();
            Console.WriteLine($" The oldest Good: Name: {theOldestGood.Name} DeliveryDate: {theOldestGood.DeliveryDate}");

            var avgGoodsQuantityByType = repository.GetAvgGoodsQuantityByType();
            foreach (var item in avgGoodsQuantityByType)
            {
                Console.WriteLine(item.ToString());
            }

            IEnumerable<Good> goodsByPassedDays = repository.GetGoodsByPassedDays(16);
            foreach (var item in goodsByPassedDays)
            {
                Console.WriteLine(item.ToString());
            }

            using ISuppliersRepository suppliersRepository = new SuppliersRepository();

            var maxQuantityOfGoodsSupplier = suppliersRepository.GetSupplierInfoWithMaxQuantityOfGoods();
            Console.WriteLine($"Supplier with max quantity of goods: {maxQuantityOfGoodsSupplier}");

            var minQuantityOfGoodsSupplier = suppliersRepository.GetSupplierInfoWithMinQuantityOfGoods();
            Console.WriteLine($"Supplier with min quantity of goods: {minQuantityOfGoodsSupplier}");

            var suppliers = suppliersRepository.Get();


            foreach (var item in suppliers)
            {
                Console.WriteLine(item.ToString());
            }

            using ITypesOfGoodsRepository typesOfGoodsRepository = new TypesOfGoodsRepository();

            var typeInfoWithMaxQuantityOfGoods = typesOfGoodsRepository.GetTypeInfoWithMaxQuantityOfGoods();
            Console.WriteLine($"Type of goods with max quantity of goods: {typeInfoWithMaxQuantityOfGoods}");

            var typeInfoWithMinQuantityOfGoods = typesOfGoodsRepository.GetTypeInfoWithMinQuantityOfGoods();
            Console.WriteLine($"Type of goods with min quantity of goods: {typeInfoWithMinQuantityOfGoods}");

            var typesOfGoods = typesOfGoodsRepository.Get();


            foreach (var item in typesOfGoods)
            {
                Console.WriteLine(item.ToString());
            }

        }

    }
}
