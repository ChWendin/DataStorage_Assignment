
using PresentationApp.Interfaces;
using Bussines.Interfaces;



namespace PresentationApp.Services;


public class MainMenu : IMainMenu
{
    private readonly IProjectService _projectService;

    public MainMenu(IProjectService projectService)
    {
        _projectService = projectService;
    }

    public async Task ShowMenu()
    {
        
      
      

        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear();
            Console.WriteLine("Projektadministration");
            Console.WriteLine("---Wendin Group AB---");
            Console.WriteLine("");
            Console.WriteLine("1. Skapa nytt projekt");
            Console.WriteLine("2. Visa alla projekt");
            Console.WriteLine("3. Visa projekt via ID");
            Console.WriteLine("4. Uppdatera projekt");
            Console.WriteLine("5. Radera projekt");
            Console.WriteLine("0. Avsluta");
            Console.WriteLine("");
            Console.Write("Välj ett alternativ: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await ProjectHandler.CreateProjectAsync(_projectService);
                    break;
                case "2":
                    await ProjectHandler.ShowAllProjectsAsync(_projectService);
                    break;
                case "3":
                    await ProjectHandler.ShowProjectByIdAsync(_projectService);
                    break;
                case "4":
                    await ProjectHandler.UpdateProjectAsync(_projectService);
                    break;
                case "5":
                    await ProjectHandler.DeleteProjectAsync(_projectService);
                    break;
                case "0":
                    isRunning = false;
                    Console.WriteLine("Avslutar programmet...");
                    break;
                default:
                    Console.WriteLine("Ogiltigt val. Tryck på valfri knapp för att fortsätta...");
                    Console.ReadKey();
                    break;
            }
        }
    }


}
