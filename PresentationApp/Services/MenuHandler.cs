

using Bussines.Interfaces;
using Bussines.Models;

namespace PresentationApp.Services
{
    public static class ProjectHandler
    {
        public static async Task CreateProjectAsync(IProjectService projectService)
        {
            Console.Clear();
            Console.WriteLine("=== Skapa Nytt Projekt ===");

            var model = new ProjectModel();

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

            Console.Write("Ange status: ");
            model.StatusName = Console.ReadLine()!;

            Console.Write("Ange ansvarig användare (förnamn): ");
            model.FirstName = Console.ReadLine()!;

            Console.Write("Ange ansvarig användare (efternamn): ");
            model.LastName = Console.ReadLine()!;

            Console.Write("Ange ansvarig användares e-postadress: ");
            model.Email = Console.ReadLine()!;

            Console.Write("Ange ansvarig användares roll: ");
            model.UserRole = Console.ReadLine()!;

            var createdProject = await projectService.CreateProjectAsync(model);

            Console.WriteLine($"Projekt '{createdProject.Title}' skapades framgångsrikt!");
            Console.WriteLine("Tryck på valfri knapp för att fortsätta...");
            Console.ReadKey();
        }

        public static async Task ShowAllProjectsAsync(IProjectService projectService)
        {
            Console.Clear();
            Console.WriteLine("=== Alla Projekt ===");
            Console.WriteLine("");
            var projects = await projectService.GetAllProjectsAsync();

            foreach (var project in projects)
            {
                Console.WriteLine($"Projekt-ID: {project.Id}, Titel: {project.Title}");
                Console.WriteLine($"Status: {project.Status.StatusName}, Start: {project.StartDate.ToShortDateString()}, Slut: {project.EndDate.ToShortDateString()}");
                Console.WriteLine($"Kund: {project.Customer.CustomerName}");
                Console.WriteLine($"Produkt: {project.Product.ProductName}, Pris: {project.Product.Price} SEK");
                Console.WriteLine($"Ansvarig: {project.User.FirstName} {project.User.LastName}, E-post: {project.User.Email}");
                Console.WriteLine("");
            }

            Console.WriteLine("Tryck på valfri knapp för att fortsätta...");
            Console.ReadKey();
        }

        public static async Task ShowProjectByIdAsync(IProjectService projectService)
        {
            Console.Clear();
            Console.Write("Ange projekt-ID: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("Felaktigt ID-format!");
                return;
            }

            var project = await projectService.GetProjectByIdAsync(id);

            if (project == null)
            {
                Console.WriteLine($"Projekt med ID {id} hittades inte.");
            }
            else
            {
                Console.WriteLine($"Projekt-ID: {project.Id}, Titel: {project.Title}");
                Console.WriteLine($"Status: {(project.Status?.StatusName ?? "Ej angivet")}, Start: {project.StartDate.ToShortDateString()}, Slut: {project.EndDate.ToShortDateString()}");
                Console.WriteLine($"Kund: {(project.Customer?.CustomerName ?? "Ej angiven")}");
                Console.WriteLine($"Produkt: {(project.Product?.ProductName ?? "Ej angiven")}, Pris: {(project.Product?.Price.ToString() ?? "Ej angivet")} SEK");
                Console.WriteLine($"Ansvarig: {(project.User?.FirstName ?? "Ej angivet")} {(project.User?.LastName ?? "Ej angivet")}, E-post: {(project.User?.Email ?? "Ej angiven")}");
            }

            Console.WriteLine("Tryck på valfri knapp för att fortsätta...");
            Console.ReadKey();
        }

        public static async Task UpdateProjectAsync(IProjectService projectService)
        {
            Console.Clear();
            Console.Write("Ange projekt-ID: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("Felaktigt ID-format!");
                Console.ReadKey();
                return;
            }

            var project = await projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                Console.WriteLine($"Projekt med ID {id} hittades inte.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Lämna fält tomt om du inte vill ändra värdet.");
            Console.Write($"Ny titel (Nuvarande: {project.Title}): ");
            var newTitle = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newTitle))
                project.Title = newTitle;

            Console.Write($"Nytt startdatum (Nuvarande: {project.StartDate.ToShortDateString()}): ");
            var newStartDate = Console.ReadLine();
            if (DateTime.TryParse(newStartDate, out var parsedStartDate))
                project.StartDate = parsedStartDate;

            Console.Write($"Nytt slutdatum (Nuvarande: {project.EndDate.ToShortDateString()}): ");
            var newEndDate = Console.ReadLine();
            if (DateTime.TryParse(newEndDate, out var parsedEndDate))
                project.EndDate = parsedEndDate;

            Console.Write($"Ny status (Nuvarande: {project.Status.StatusName}): ");
            var newStatus = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newStatus))
                project.Status.StatusName = newStatus;

            Console.Write($"Ny kund (Nuvarande: {project.Customer.CustomerName}): ");
            var newCustomer = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newCustomer))
                project.Customer.CustomerName = newCustomer;

            Console.Write($"Ny produkt (Nuvarande: {project.Product.ProductName}): ");
            var newProduct = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newProduct))
                project.Product.ProductName = newProduct;

            Console.Write($"Nytt pris (Nuvarande: {project.Product.Price} SEK): ");
            var newPrice = Console.ReadLine();
            if (decimal.TryParse(newPrice, out var parsedPrice))
                project.Product.Price = parsedPrice;

            Console.Write($"Ny ansvarig förnamn (Nuvarande: {project.User.FirstName}): ");
            var newFirstName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newFirstName))
                project.User.FirstName = newFirstName;

            Console.Write($"Ny ansvarig efternamn (Nuvarande: {project.User.LastName}): ");
            var newLastName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newLastName))
                project.User.LastName = newLastName;

            Console.Write($"Ny e-post (Nuvarande: {project.User.Email}): ");
            var newEmail = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newEmail))
                project.User.Email = newEmail;

            bool success = await projectService.UpdateProjectAsync(id, project);

            Console.WriteLine(success ? "Projekt uppdaterades!" : "Uppdatering misslyckades.");
            Console.ReadKey();
        }

        public static async Task DeleteProjectAsync(IProjectService projectService)
        {
            Console.Clear();
            Console.Write("Ange projekt-ID: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("Felaktigt ID-format!");
                return;
            }

            bool success = await projectService.DeleteProjectAsync(id);

            Console.WriteLine(success ? "Projekt raderades!" : "Projektet kunde inte hittas.");
            Console.ReadKey();
        }
    }
}
