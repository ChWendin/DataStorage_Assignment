using PresentationApp.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Bussines.Interfaces;
using Bussines.Services;
using Data.Interfaces;
using Data.Repositories;
using Data.Entities;

var serviceCollection = new ServiceCollection();

serviceCollection.AddDbContext<DataContext>(options =>
    options.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Projects\\DataStorage_Assignment\\Data\\Databases\\Local_db.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True"));

serviceCollection.AddScoped<IProjectService, ProjectService>();
serviceCollection.AddScoped<ProjectRelatedEntitiesService>();

serviceCollection.AddScoped<MainMenu>();
serviceCollection.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
serviceCollection.AddScoped<IBaseRepository<StatusTypeEntity>, BaseRepository<StatusTypeEntity>>();
serviceCollection.AddScoped<IBaseRepository<CustomerEntity>, BaseRepository<CustomerEntity>>();
serviceCollection.AddScoped<IBaseRepository<ProductEntity>, BaseRepository<ProductEntity>>();
serviceCollection.AddScoped<IBaseRepository<UserEntity>, BaseRepository<UserEntity>>();

var serviceProvider = serviceCollection.BuildServiceProvider();


var mainMenu = serviceProvider.GetRequiredService<MainMenu>();
await mainMenu.ShowMenu();