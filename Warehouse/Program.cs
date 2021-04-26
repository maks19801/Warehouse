using Warehouse.Menu;

namespace Warehouse
{
    public class Program
    {
        static void Main(string[] args)
        {
            var repositoryOption = MainMenu.ShowMenu();
            MainMenu.MainMenuOptions(repositoryOption);
        }
    }
}
