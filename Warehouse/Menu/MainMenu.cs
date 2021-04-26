using System;
using Warehouse.Repositories;

namespace Warehouse.Menu
{
    public class MainMenu
    {
        public enum Repository
        {
            Exit,
            GoodsRepository,
            SuppliersRepository,
            TypesOfGoodsRepository
        }

        public static void MainMenuOptions(Repository repositoryOption)
        {
            switch (repositoryOption)
            {
                case Repository.GoodsRepository:
                    {
                        using IGoodsRepository goodsRepository = new GoodsRepository();
                        var goodsRepositoryOperation = GoodsRepositoryMenu.ShowGoodsRepositoryMenu();
                        GoodsRepositoryMenu.ChooseOperationForGoodsRepository(goodsRepository, goodsRepositoryOperation);
                        break;
                    }
                case Repository.SuppliersRepository:
                    {
                        using ISuppliersRepository suppliersRepository = new SuppliersRepository();
                        var suppliersRepositoryOperation = SuppliersRepositoryMenu.ShowSuppliersRepositoryMenu();
                        SuppliersRepositoryMenu.ChooseOperatioForSuppliersRepository(suppliersRepository, suppliersRepositoryOperation);
                        break;
                    }
                case Repository.TypesOfGoodsRepository:
                    {
                        using ITypesOfGoodsRepository typesOfGoodsRepository = new TypesOfGoodsRepository();
                        var typesOgGoodsRepositoryOperation = TypesOfGoodsRepositoryMenu.ShowTypesOfGoodsRepositoryMenu();
                        TypesOfGoodsRepositoryMenu.ChooseOperationForTypesOfGoodsRepository(typesOfGoodsRepository, typesOgGoodsRepositoryOperation);
                        break;
                    }
                default:
                    Environment.Exit(0);
                    break;
            }
        }


        public static Repository ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("Choose repository You would like to use: ");
                Console.WriteLine("1. GoodsRepository");
                Console.WriteLine("2. SuppliersRepository");
                Console.WriteLine("3. TypesOfGoodsRepository");
                Console.WriteLine("0. Exit");

                if (int.TryParse(Console.ReadLine(), out var repositoryId)
                    && repositoryId >= 0 && repositoryId <= 3)
                {
                    var repository = (Repository)repositoryId;
                    return repository;
                }
                Console.WriteLine("Incorrect input!");
            }
        }
    }
}
