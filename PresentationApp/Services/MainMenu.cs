
using Bussines.Services;
using Bussines.Models;
using Data.Contexts;



namespace PresentationApp.Services;


public class MainMenu(DataContext context)
{
    private readonly DataContext _context = context;

    public async Task ShowMenu()
    {
        
        var projectService = new ProjectService(_context);
      

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
                    await CreateProjectAsync(projectService);
                    break;
                case "2":
                    await ShowAllProjectsAsync(projectService);
                    break;
                case "3":
                    await ShowProjectByIdAsync(projectService);
                    break;
                case "4":
                    await UpdateProjectAsync(projectService);
                    break;
                case "5":
                    await DeleteProjectAsync(projectService);
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

    static async Task CreateProjectAsync(ProjectService projectService)
    {
        Console.Clear();
        Console.WriteLine("=== Skapa Nytt Projekt ===");

        var model = new ProjectModel();

        Console.Write("Ange projektnummer (P-xxxx): ");
        model.ProjectNumber = Console.ReadLine()!;

        Console.Write("Ange projekttitel: ");
        model.Title = Console.ReadLine()!;

        Console.Write("Ange startdatum (yyyy-mm-dd): ");
        model.StartDate = DateTime.Parse(Console.ReadLine()!);

        Console.Write("Ange slutdatum (yyyy-mm-dd): ");
        
        model.EndDate = DateTime.Parse(Console.ReadLine()!);

        Console.Write("Ange kundnamn: ");
        model.CustomerName = Console.ReadLine()!;

        Console.Write("Ange produktnamn: ");
        model.ProductName = Console.ReadLine()!;

        Console.Write("Ange produktpris: ");
        model.Price = decimal.Parse(Console.ReadLine()!);

        Console.Write("Ange status (Ej påbörjat, Pågående, Avslutat): ");
        model.StatusName = Console.ReadLine()!;

        Console.Write("Ange ansvarig användares förnamn: ");
        model.FirstName = Console.ReadLine()!;

        Console.Write("Ange ansvarig användares efternamn: ");
        model.LastName = Console.ReadLine()!;

        Console.Write("Ange ansvarig användares e-postadress: ");
        model.Email = Console.ReadLine()!;

        Console.Write("Ange ansvarig användares roll: ");
        model.UserRole = Console.ReadLine()!;

        var createdProject = await projectService.CreateProjectAsync(model);

        Console.WriteLine($"Projekt '{createdProject.Title}' skapades framgångsrikt med projekt-ID {createdProject.Id}!");
        Console.WriteLine("Tryck på valfri knapp för att fortsätta...");
        Console.ReadKey();
    }

    static async Task ShowAllProjectsAsync(ProjectService projectService)
    {
        Console.Clear();
        Console.WriteLine("=== Alla Projekt ===");
        Console.WriteLine("");

        var projects = await projectService.GetAllProjectsAsync();

        foreach (var project in projects)
        {
            Console.WriteLine($"Projekt-ID: {project.Id}, Titel: {project.Title}");
            Console.WriteLine($"Status: {project.Status.StatusName}, Startdatum: {project.StartDate.ToShortDateString()}, Slutdatum: {project.EndDate.ToShortDateString()}");
            Console.WriteLine($"Kund: {project.Customer.CustomerName}");
            Console.WriteLine($"Produkt: {project.Product.ProductName}, Pris: {project.Product.Price} SEK");
            Console.WriteLine($"Projektansvarig: {project.User.FirstName} {project.User.LastName}, E-post: {project.User.Email}, Roll: {project.User.UserRole}");
            Console.WriteLine("");
        }

        Console.WriteLine("Tryck på valfri knapp för att fortsätta...");
        Console.ReadKey();
    }

    static async Task ShowProjectByIdAsync(ProjectService projectService)
    {
        Console.Clear();
        Console.WriteLine("=== Visa Projekt ===");

        Console.Write("Ange projekt-ID: ");
        int id = int.Parse(Console.ReadLine()!);

        try
        {
            var project = await projectService.GetProjectByIdAsync(id);

            Console.WriteLine($"Titel: {project.Title}");
            Console.WriteLine($"Startdatum: {project.StartDate.ToShortDateString()}");
            Console.WriteLine($"Slutdatum: {project.EndDate.ToShortDateString()}");
            Console.WriteLine($"Kund: {project.Customer.CustomerName}");
            Console.WriteLine($"Produkt: {project.Product.ProductName}, Pris: {project.Product.Price} SEK");
            Console.WriteLine($"Status: {project.Status.StatusName}");
            Console.WriteLine($"Projektansvarig: {project.User.FirstName} {project.User.LastName}, E-post: {project.User.Email}, Roll: {project.User.UserRole}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine("Tryck på valfri knapp för att fortsätta...");
        Console.ReadKey();
    }

    static async Task UpdateProjectAsync(ProjectService projectService)
    {
        Console.Clear();
        Console.WriteLine("=== Uppdatera Projekt ===");

        Console.Write("Ange projekt-ID: ");
        int id = int.Parse(Console.ReadLine()!);

        var existingProject = await projectService.GetProjectByIdAsync(id);

        // Titel
        Console.WriteLine($"Nuvarande titel: {existingProject.Title}");
        Console.Write("Ny titel (lämna tom för att behålla): ");
        var newTitle = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newTitle))
            existingProject.Title = newTitle;

        // Startdatum
        Console.WriteLine($"Nuvarande startdatum: {existingProject.StartDate:yyyy-MM-dd}");
        Console.Write("Nytt startdatum (yyyy-mm-dd, lämna tom för att behålla): ");
        var newStartDateInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newStartDateInput) && DateTime.TryParse(newStartDateInput, out var newStartDate))
            existingProject.StartDate = newStartDate;

        // Slutdatum
        Console.WriteLine($"Nuvarande slutdatum: {existingProject.EndDate:yyyy-MM-dd}");
        Console.Write("Nytt slutdatum (yyyy-mm-dd, lämna tom för att behålla): ");
        var newEndDateInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newEndDateInput) && DateTime.TryParse(newEndDateInput, out var newEndDate))
            existingProject.EndDate = newEndDate;

        // Projektnummer
        Console.WriteLine($"Nuvarande projektnummer: {existingProject.ProjectNumber}");
        Console.Write("Nytt projektnummer (lämna tom för att behålla): ");
        var newProjectNumber = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newProjectNumber))
            existingProject.ProjectNumber = newProjectNumber;

        // Produktnamn
        Console.WriteLine($"Nuvarande produktnamn: {existingProject.Product.ProductName}");
        Console.Write("Nytt produktnamn (lämna tom för att behålla): ");
        var newProductName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newProductName))
            existingProject.Product.ProductName = newProductName;

        // Produktpris
        Console.WriteLine($"Nuvarande produktpris: {existingProject.Product.Price:C}");
        Console.Write("Nytt produktpris (lämna tom för att behålla): ");
        var newProductPriceInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newProductPriceInput) && decimal.TryParse(newProductPriceInput, out var newProductPrice))
            existingProject.Product.Price = newProductPrice;

        // Status
        Console.WriteLine($"Nuvarande status: {existingProject.Status.StatusName}");
        Console.Write("Ny status (lämna tom för att behålla): ");
        var newStatusName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newStatusName))
            existingProject.Status.StatusName = newStatusName;

        // Kundnamn
        Console.WriteLine($"Nuvarande kundnamn: {existingProject.Customer.CustomerName}");
        Console.Write("Nytt kundnamn (lämna tom för att behålla): ");
        var newCustomerName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newCustomerName))
            existingProject.Customer.CustomerName = newCustomerName;

        // Ansvarig användare - Förnamn
        Console.WriteLine($"Nuvarande förnamn: {existingProject.User.FirstName}");
        Console.Write("Nytt förnamn (lämna tom för att behålla): ");
        var newFirstName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newFirstName))
            existingProject.User.FirstName = newFirstName;

        // Ansvarig användare - Efternamn
        Console.WriteLine($"Nuvarande efternamn: {existingProject.User.LastName}");
        Console.Write("Nytt efternamn (lämna tom för att behålla): ");
        var newLastName = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newLastName))
            existingProject.User.LastName = newLastName;

        // Ansvarig användare - E-post
        Console.WriteLine($"Nuvarande e-postadress: {existingProject.User.Email}");
        Console.Write("Ny e-postadress (lämna tom för att behålla): ");
        var newEmail = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newEmail))
            existingProject.User.Email = newEmail;

        // Ansvarig användare - Roll
        Console.WriteLine($"Nuvarande roll: {existingProject.User.UserRole}");
        Console.Write("Ny roll (lämna tom för att behålla): ");
        var newUserRole = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(newUserRole))
            existingProject.User.UserRole = newUserRole;

        bool success = await projectService.UpdateProjectAsync(id, existingProject);

        if (success)
            Console.WriteLine("Projektet uppdaterades framgångsrikt!");
        else
            Console.WriteLine("Projektet kunde inte uppdateras.");

        Console.WriteLine("Tryck på valfri knapp för att fortsätta...");
        Console.ReadKey();
    }

    static async Task DeleteProjectAsync(ProjectService projectService)
    {
        Console.Clear();
        Console.WriteLine("=== Radera Projekt ===");

        Console.Write("Ange projekt-ID: ");
        int id = int.Parse(Console.ReadLine()!);

        bool success = await projectService.DeleteProjectAsync(id);

        if (success)
            Console.WriteLine("Projektet raderades framgångsrikt!");
        else
            Console.WriteLine("Projektet kunde inte hittas.");

        Console.WriteLine("Tryck på valfri knapp för att fortsätta...");
        Console.ReadKey();
    }
}
