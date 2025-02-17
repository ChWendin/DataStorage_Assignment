using PresentationApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Bussines.Interfaces;
using Bussines.Services;



    var serviceCollection = new ServiceCollection();
    serviceCollection.AddDbContext<DataContext>(options => options.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Projects\\DataStorage_Assignment\\Data\\Databases\\Local_Database.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True"));
    serviceCollection.AddScoped<IProjectService, ProjectService>();
    serviceCollection.AddScoped<MainMenu>();

    var serviceProvider = serviceCollection.BuildServiceProvider();

var mainMenu = serviceProvider.GetRequiredService<MainMenu>();
await mainMenu.ShowMenu();