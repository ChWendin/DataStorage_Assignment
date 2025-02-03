

namespace PresentationApp.Services;

public class MainMenu
{
    public void ShowMenu()
    {
            while (true)
        {
            Console.Clear();
            Console.WriteLine("\n--- Huvudmeny ---");
            Console.WriteLine("1. Skapa nytt projekt");
            Console.WriteLine("2. Visa samtliga projekt");
            Console.WriteLine("3. Uppdatera projekt");
            Console.WriteLine("4. Radera projekt");
            Console.WriteLine("5. Avsluta");
            Console.Write("Välj ett alternativ: ");

            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    _subMenus.AddUserMenu();
                    
                    break;
                case "2":
                    _subMenus.ShowAllUsersMenu();
                    break;
                case "3":
                    _subMenus.UpdateUserMenu();
                    break;
                case "4":
                    _subMenus.DeleteAllUsersMenu();
                    break;
                case "5":
                    Console.WriteLine("Avslutar...");
                    Console.ReadKey();
                    return;
                default:
                    Console.WriteLine("Ogiltigt val, försök igen.");
                    Console.ReadKey();
                    break;
            }


        }
    }
}
