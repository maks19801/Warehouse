using System;
using System.Collections.Generic;
using Warehouse.Entities;
using Warehouse.Repositories;

namespace Warehouse.Menu
{
    public class SuppliersRepositoryMenu
    {
        public enum OperationForSuppliersRepository
        {
            Exit,
            ShowAll,
            ShowById,
            Add,
            Update,
            Delete,
            GetSupplierInfoWithMaxQuantityOfGoods,
            GetSupplierInfoWithMinQuantityOfGoods
        }
        public static void ChooseOperatioForSuppliersRepository(ISuppliersRepository suppliersRepository, OperationForSuppliersRepository suppliersRepositoryOperation)
        {
            switch (suppliersRepositoryOperation)
            {
                case OperationForSuppliersRepository.ShowAll:
                    ShowAllSuppliers(suppliersRepository.Get());
                    break;
                case OperationForSuppliersRepository.ShowById:
                    Console.WriteLine("Enter supplier Id: ");
                    if (int.TryParse(Console.ReadLine(), out var supplierId))
                    {
                        Console.WriteLine(suppliersRepository.Get(supplierId));
                    }
                    break;
                case OperationForSuppliersRepository.Add:
                    Supplier supplierToAdd = CreateSupplierToAdd();
                    suppliersRepository.Add(supplierToAdd);
                    Console.WriteLine("New supplier added");
                    break;
                case OperationForSuppliersRepository.Update:

                    Supplier supplierToUpdate = null;
                    Console.Write("Enter supplier Id to update: ");
                    if (int.TryParse(Console.ReadLine(), out var supplierIdToUpdate))
                    {
                        supplierToUpdate = suppliersRepository.Get(supplierIdToUpdate);
                    }
                    Console.Write("Enter new Name: ");
                    supplierToUpdate.Name = Console.ReadLine();

                    suppliersRepository.Update(supplierToUpdate);
                    Console.WriteLine("Supplier updated");
                    break;
                case OperationForSuppliersRepository.Delete:
                    Console.WriteLine("Enter supplier Id: ");
                    if (int.TryParse(Console.ReadLine(), out var supplierIdToDelete))
                    {
                        suppliersRepository.Delete(supplierIdToDelete);
                        Console.WriteLine("Supplier deleted");
                    }
                    break;
                case OperationForSuppliersRepository.GetSupplierInfoWithMaxQuantityOfGoods:
                    var maxQuantityOfGoodsSupplier = suppliersRepository.GetSupplierInfoWithMaxQuantityOfGoods();
                    Console.WriteLine($"Supplier with max quantity of goods: {maxQuantityOfGoodsSupplier}");
                    break;
                case OperationForSuppliersRepository.GetSupplierInfoWithMinQuantityOfGoods:
                    var minQuantityOfGoodsSupplier = suppliersRepository.GetSupplierInfoWithMaxQuantityOfGoods();
                    Console.WriteLine($"Supplier with max quantity of goods: {minQuantityOfGoodsSupplier}");
                    break;
                default:
                    Environment.Exit(0);
                    break;
            }
        }


        private static Supplier CreateSupplierToAdd()
        {
            Console.Write("Enter goods Name:");
            var name = Console.ReadLine();

            var supplierToAdd = new Supplier
            {
                Name = name,
            };
            return supplierToAdd;
        }




        private static void ShowAllSuppliers(IEnumerable<Supplier> suppliers)
        {
            foreach (var item in suppliers)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public static OperationForSuppliersRepository ShowSuppliersRepositoryMenu()
        {
            while (true)
            {
                Console.WriteLine("Choose option: ");
                Console.WriteLine("1. Show all");
                Console.WriteLine("2. Show by id");
                Console.WriteLine("3. Add supplier");
                Console.WriteLine("4. Update by id");
                Console.WriteLine("5. Delete by id");
                Console.WriteLine("6. Show Supplier Info With Max Quantity Of Goods");
                Console.WriteLine("7. Show Supplier Info With Min Quantity Of Goods");
                Console.WriteLine("0. Exit");

                if (int.TryParse(Console.ReadLine(), out var operationId)
                    && operationId >= 0 && operationId <= 7)
                {
                    var operation = (OperationForSuppliersRepository)operationId;
                    return operation;
                }
                Console.WriteLine("Incorrect input!");
            }
        }
    }
}
