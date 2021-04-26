using System;
using System.Collections.Generic;
using Warehouse.Entities;
using Warehouse.Repositories;

namespace Warehouse.Menu
{
    public class TypesOfGoodsRepositoryMenu
    {
        public enum OperationForTypesOfGoodsRepository
        {
            Exit,
            ShowAll,
            ShowById,
            Add,
            Update,
            Delete,
            GetTypeInfoWithMaxQuantityOfGoods,
            GetTypeInfoWithMinQuantityOfGoods,
        }
        public static void ChooseOperationForTypesOfGoodsRepository(ITypesOfGoodsRepository typesOfGoodsRepository, OperationForTypesOfGoodsRepository typesOgGoodsRepositoryOperation)
        {
            switch (typesOgGoodsRepositoryOperation)
            {
                case OperationForTypesOfGoodsRepository.ShowAll:
                    ShowAllTypesOfGoods(typesOfGoodsRepository.Get());
                    break;
                case OperationForTypesOfGoodsRepository.ShowById:
                    Console.WriteLine("Enter type of good Id: ");
                    if (int.TryParse(Console.ReadLine(), out var typeOfGoodId))
                    {
                        Console.WriteLine(typesOfGoodsRepository.Get(typeOfGoodId));
                    }
                    break;
                case OperationForTypesOfGoodsRepository.Add:
                    TypesOfGood typeOfGoodToAdd = CreateTypeOfGoodToAdd();
                    typesOfGoodsRepository.Add(typeOfGoodToAdd);
                    Console.WriteLine("New type of goods added");
                    break;
                case OperationForTypesOfGoodsRepository.Update:

                    TypesOfGood typeOfGoodToUpdate = null;
                    Console.Write("Enter type of goods Id to update: ");
                    if (int.TryParse(Console.ReadLine(), out var typeofGoodIdToUpdate))
                    {
                        typeOfGoodToUpdate = typesOfGoodsRepository.Get(typeofGoodIdToUpdate);
                    }
                    Console.Write("Enter new name of Type of goods: ");
                    typeOfGoodToUpdate.Type = Console.ReadLine();

                    typesOfGoodsRepository.Update(typeOfGoodToUpdate);
                    Console.WriteLine("Type of goods updated");
                    break;
                case OperationForTypesOfGoodsRepository.Delete:
                    Console.WriteLine("Enter type of goods Id to delete: ");
                    if (int.TryParse(Console.ReadLine(), out var typeOfGoodIdToDelete))
                    {
                        typesOfGoodsRepository.Delete(typeOfGoodIdToDelete);
                        Console.WriteLine("Type of goods deleted");
                    }
                    break;
                case OperationForTypesOfGoodsRepository.GetTypeInfoWithMaxQuantityOfGoods:
                    var maxQuantityOfGoodsType = typesOfGoodsRepository.GetTypeInfoWithMaxQuantityOfGoods();
                    Console.WriteLine($"Type with max quantity of goods: {maxQuantityOfGoodsType}");
                    break;
                case OperationForTypesOfGoodsRepository.GetTypeInfoWithMinQuantityOfGoods:
                    var minQuantityOfGoodsType = typesOfGoodsRepository.GetTypeInfoWithMinQuantityOfGoods();
                    Console.WriteLine($"Type with min quantity of goods: {minQuantityOfGoodsType}");
                    break;
                default:
                    Environment.Exit(0);
                    break;
            }
        }

        public static OperationForTypesOfGoodsRepository ShowTypesOfGoodsRepositoryMenu()
        {
            while (true)
            {
                Console.WriteLine("Choose option: ");
                Console.WriteLine("1. Show all");
                Console.WriteLine("2. Show by id");
                Console.WriteLine("3. Add type");
                Console.WriteLine("4. Update by id");
                Console.WriteLine("5. Delete by id");
                Console.WriteLine("6. Show Type Info With Max Quantity Of Goods");
                Console.WriteLine("7. Show Type Info With Min Quantity Of Goods");
                Console.WriteLine("0. Exit");

                if (int.TryParse(Console.ReadLine(), out var operationId)
                    && operationId >= 0 && operationId <= 7)
                {
                    var operation = (OperationForTypesOfGoodsRepository)operationId;
                    return operation;
                }
                Console.WriteLine("Incorrect input!");
            }
        }
        private static void ShowAllTypesOfGoods(IEnumerable<TypesOfGood> typesOfGoods)
        {
            foreach (var item in typesOfGoods)
            {
                Console.WriteLine(item.ToString());
            }
        }
        private static TypesOfGood CreateTypeOfGoodToAdd()
        {
            Console.Write("Enter type of goods Name:");
            var name = Console.ReadLine();

            var typeOfGoodToAdd = new TypesOfGood
            {
                Type = name,
            };
            return typeOfGoodToAdd;
        }
    }
}
