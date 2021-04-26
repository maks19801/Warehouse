using System;
using System.Collections.Generic;
using Warehouse.Entities;
using Warehouse.Repositories;

namespace Warehouse.Menu
{
    public class GoodsRepositoryMenu
    {
        public enum FieldOfGood
        {
            Exit,
            Name,
            TypeId,
            SupplierId,
            Quantity,
            Cost,
            DeliveryDate
        }
        public enum OperationForGoodsRepository
        {
            Exit,
            ShowAll,
            ShowById,
            Add,
            Update,
            Delete,
            ShowMaxQuantityGood,
            ShowMinQuantityGood,
            ShowMaxCostGood,
            ShowMinCostGood,
            ShowGoodsByType,
            ShowGoodsBySupplier,
            ShowGoodsByPassedDays,
            ShowGoodsByPassedMaxDays,
            ShowAverageGoodsQuantityByType
        }
        public static void ChooseOperationForGoodsRepository(IGoodsRepository goodsRepository, OperationForGoodsRepository goodsRepositoryOperation)
        {
            switch (goodsRepositoryOperation)
            {
                case OperationForGoodsRepository.ShowAll:
                    ShowAllGoods(goodsRepository.Get());
                    break;
                case OperationForGoodsRepository.ShowById:
                    Console.WriteLine("Enter good Id: ");
                    if (int.TryParse(Console.ReadLine(), out var getGoodId))
                    {
                        Console.WriteLine(goodsRepository.Get(getGoodId));
                    }
                    break;
                case OperationForGoodsRepository.Add:
                    Good goodToAdd = CreateGoodToAdd();
                    goodsRepository.Add(goodToAdd);
                    Console.WriteLine("New good added");
                    break;
                case OperationForGoodsRepository.Update:

                    Good goodToUpdate = null;
                    Console.Write("Enter good Id to update: ");
                    if (int.TryParse(Console.ReadLine(), out var getGoodIdToUpdate))
                    {
                        goodToUpdate = goodsRepository.Get(getGoodIdToUpdate);
                    }
                    var fieldToUpdate = ShowGoodUpdateMenu();
                    ChooseFieldToUpdate(goodToUpdate, fieldToUpdate);

                    goodsRepository.Update(goodToUpdate);
                    Console.WriteLine("Good updated");
                    break;
                case OperationForGoodsRepository.Delete:
                    Console.WriteLine("Enter good Id: ");
                    if (int.TryParse(Console.ReadLine(), out var deleteGoodId))
                    {
                        goodsRepository.Delete(deleteGoodId);
                        Console.WriteLine("Good deleted");
                    }
                    break;
                case OperationForGoodsRepository.ShowMaxQuantityGood:
                    var goodMaxQuantity = goodsRepository.GetMaxQuantityGood();
                    Console.WriteLine($" Max quantity Good: Name: {goodMaxQuantity.Name} Quantity: {goodMaxQuantity.Quantity}");
                    break;
                case OperationForGoodsRepository.ShowMinQuantityGood:
                    var goodMinQuantity = goodsRepository.GetMaxQuantityGood();
                    Console.WriteLine($" Max quantity Good: Name: {goodMinQuantity.Name} Quantity: {goodMinQuantity.Quantity}");
                    break;
                case OperationForGoodsRepository.ShowMaxCostGood:
                    var goodMaxCost = goodsRepository.GetMaxQuantityGood();
                    Console.WriteLine($" Max quantity Good: Name: {goodMaxCost.Name} Quantity: {goodMaxCost.Cost}");
                    break;
                case OperationForGoodsRepository.ShowMinCostGood:
                    var goodMinCost = goodsRepository.GetMaxQuantityGood();
                    Console.WriteLine($" Max quantity Good: Name: {goodMinCost.Name} Quantity: {goodMinCost.Cost}");
                    break;
                case OperationForGoodsRepository.ShowGoodsByType:
                    IEnumerable<Good> goodsByTypeId = null;
                    Console.Write("Enter type Id: ");
                    if (int.TryParse(Console.ReadLine(), out var typeId))
                    {
                        goodsByTypeId = goodsRepository.GetGoodsByType(typeId);
                    }
                    ShowAllGoods(goodsByTypeId);
                    break;
                case OperationForGoodsRepository.ShowGoodsBySupplier:
                    IEnumerable<Good> goodsBySupplier = null;
                    Console.Write("Enter supplier Id: ");
                    if (int.TryParse(Console.ReadLine(), out var supplierId))
                    {
                        goodsBySupplier = goodsRepository.GetGoodsBySupplier(supplierId);
                    }
                    ShowAllGoods(goodsBySupplier);
                    break;
                case OperationForGoodsRepository.ShowGoodsByPassedDays:
                    IEnumerable<Good> goodsByPassedDays = null;
                    Console.Write("Enter number of days: ");
                    if (int.TryParse(Console.ReadLine(), out var numberOfDays))
                    {
                        goodsByPassedDays = goodsRepository.GetGoodsByPassedDays(numberOfDays);
                    }
                    ShowAllGoods(goodsByPassedDays);
                    break;
                case OperationForGoodsRepository.ShowGoodsByPassedMaxDays:
                    var theOldestGood = goodsRepository.GetTheOldestGood();
                    Console.WriteLine($" The oldest Good: Name: {theOldestGood.Name} DeliveryDate: {theOldestGood.DeliveryDate}");
                    break;
                case OperationForGoodsRepository.ShowAverageGoodsQuantityByType:
                    var avgGoodsQuantityByType = goodsRepository.GetAvgGoodsQuantityByType();
                    foreach (var item in avgGoodsQuantityByType)
                    {
                        Console.WriteLine(item.ToString());
                    }
                    break;
                default:
                    Environment.Exit(0);
                    break;
            }
        }

        private static void ChooseFieldToUpdate(Good goodToUpdate, FieldOfGood fieldToUpdate)
        {
            switch (fieldToUpdate)
            {
                case FieldOfGood.Name:
                    Console.Write("Enter new Name: ");
                    goodToUpdate.Name = Console.ReadLine();
                    break;
                case FieldOfGood.TypeId:
                    Console.Write("Enter new TypeId: ");
                    goodToUpdate.TypeId = int.Parse(Console.ReadLine());
                    break;
                case FieldOfGood.SupplierId:
                    Console.Write("Enter new SupplierId: ");
                    goodToUpdate.SupplierId = int.Parse(Console.ReadLine());
                    break;
                case FieldOfGood.Quantity:
                    Console.Write("Enter new Quantity: ");
                    goodToUpdate.Quantity = int.Parse(Console.ReadLine());
                    break;
                case FieldOfGood.Cost:
                    Console.Write("Enter new Cost: ");
                    goodToUpdate.Cost = decimal.Parse(Console.ReadLine());
                    break;
                case FieldOfGood.DeliveryDate:
                    Console.Write("Enter new DeliveryDate in format dd-mm-yyyy: ");
                    goodToUpdate.DeliveryDate = DateTime.Parse(Console.ReadLine());
                    break;
                default:
                    Environment.Exit(0);
                    break;
            }
        }

        private static FieldOfGood ShowGoodUpdateMenu()
        {
            while (true)
            {
                Console.WriteLine("Choose field You would like to update: ");
                Console.WriteLine("1. Name");
                Console.WriteLine("2. TypeId");
                Console.WriteLine("3. SupplierId");
                Console.WriteLine("4. Quantity");
                Console.WriteLine("5. Cost");
                Console.WriteLine("6. DeliveryDate");
                Console.WriteLine("0. Exit");

                if (int.TryParse(Console.ReadLine(), out var fieldOfGood)
                    && fieldOfGood >= 0 && fieldOfGood <= 6)
                {
                    var fieldOfGoodToUpdate = (FieldOfGood)fieldOfGood;
                    return fieldOfGoodToUpdate;
                }
                Console.WriteLine("Incorrect input!");
            }
        }

        private static Good CreateGoodToAdd()
        {
            Console.Write("Enter goods Name:");
            var name = Console.ReadLine();
            Console.Write("Enter goods TypeId:");
            var typeId = int.Parse(Console.ReadLine());
            Console.Write("Enter goods SupplierId:");
            var supplierId = int.Parse(Console.ReadLine());
            Console.Write("Enter goods Quantity:");
            var quantity = int.Parse(Console.ReadLine());
            Console.Write("Enter goods Cost:");
            var cost = decimal.Parse(Console.ReadLine());
            Console.Write("Enter goods Date in format dd-mm-yyyy:");
            var date = DateTime.Parse(Console.ReadLine());
            var goodToAdd = new Good
            {
                Name = name,
                TypeId = typeId,
                SupplierId = supplierId,
                Quantity = quantity,
                Cost = cost,
                DeliveryDate = date,

            };
            return goodToAdd;
        }

        private static void ShowAllGoods(IEnumerable<Good> goods)
        {
            foreach (var item in goods)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public static OperationForGoodsRepository ShowGoodsRepositoryMenu()
        {
            while (true)
            {
                Console.WriteLine("Choose option: ");
                Console.WriteLine("1. Show all");
                Console.WriteLine("2. Show by id");
                Console.WriteLine("3. Add good");
                Console.WriteLine("4. Update by id");
                Console.WriteLine("5. Delete by id");
                Console.WriteLine("6. Show Max quantity good");
                Console.WriteLine("7. Show Min quantity good");
                Console.WriteLine("8. Show Max cost good");
                Console.WriteLine("9. Show Min cost good");
                Console.WriteLine("10. Show goods by their type");
                Console.WriteLine("11. Show goods by their supplier");
                Console.WriteLine("12. Show goods by passed days from delivery");
                Console.WriteLine("13. Show goods by passed max days from delivery");
                Console.WriteLine("14. Show average goods quantity by type");
                Console.WriteLine("0. Exit");

                if (int.TryParse(Console.ReadLine(), out var operationId)
                    && operationId >= 0 && operationId <= 14)
                {
                    var operation = (OperationForGoodsRepository)operationId;
                    return operation;
                }
                Console.WriteLine("Incorrect input!");
            }
        }
    }
}
