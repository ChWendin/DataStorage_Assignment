using PresentationApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Bussines.Interfaces;
using Bussines.Services;
using Data.Interfaces;
using Data.Repositories;
using Data.Entities;
using Microsoft.Extensions.Logging;

var serviceCollection = new ServiceCollection();

// Registrera DataContext (DbContext) för att användas via DI
serviceCollection.AddDbContext<DataContext>(options =>
    options.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Projects\\DataStorage_Assignment\\Data\\Databases\\Local_db.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True")
    .LogTo(Console.WriteLine, LogLevel.Information));  // Logga SQL-kommandon till konsolen

// Registrera dina tjänster
serviceCollection.AddScoped<IProjectService, ProjectService>();


// Registrera MainMenu och repositories
serviceCollection.AddScoped<MainMenu>();
serviceCollection.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
serviceCollection.AddScoped<IBaseRepository<StatusTypeEntity>, BaseRepository<StatusTypeEntity>>();
serviceCollection.AddScoped<IBaseRepository<CustomerEntity>, BaseRepository<CustomerEntity>>();
serviceCollection.AddScoped<IBaseRepository<ProductEntity>, BaseRepository<ProductEntity>>();
serviceCollection.AddScoped<IBaseRepository<UserEntity>, BaseRepository<UserEntity>>();

var serviceProvider = serviceCollection.BuildServiceProvider();

// Hämta MainMenu och kör det
var mainMenu = serviceProvider.GetRequiredService<MainMenu>();
await mainMenu.ShowMenu();