

using Data.Entities;
using Bussines.Models;
using Data.Interfaces;

namespace Bussines.Factories;

public static class ProjectFactory
{
    public static ProjectEntity CreateProject(ProjectModel model)
    {
        var customer = new CustomerEntity { CustomerName = model.CustomerName };
        var product = new ProductEntity { ProductName = model.ProductName, Price = model.Price };
        var status = new StatusTypeEntity { StatusName = model.StatusName };
        var user = new UserEntity { FirstName = model.FirstName, LastName = model.LastName, Email = model.Email, UserRole = model.UserRole };

        var project = new ProjectEntity
        {
            Title = model.Title,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            Customer = customer,
            Product = product,
            Status = status,
            User = user
        };

        return project;
    }

    public static ProjectModel Create(ProjectEntity entity)
    {
        if (entity == null) return null!;

        return new ProjectModel
        {
            Title = entity.Title,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            StatusName = entity.Status?.StatusName ?? "Okänd",
            ProductName = entity.Product?.ProductName ?? "Okänd",
            Price = entity.Product?.Price ?? 0m,
            CustomerName = entity.Customer?.CustomerName ?? "Okänd",
            FirstName = entity.User?.FirstName ?? "Okänd",
            LastName = entity.User?.LastName ?? "Okänd",
            Email = entity.User?.Email ?? "Okänd",
            UserRole = entity.User?.UserRole ?? "Okänd"
        };
    }

    public static ProjectModel? MapToModel(ProjectEntity? entity)
    {
        if (entity == null) return null;

        return new ProjectModel
        {
            Id = entity.Id,
            Title = entity.Title,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            StatusName = entity.Status?.StatusName ?? string.Empty,
            ProductName = entity.Product?.ProductName ?? string.Empty,
            Price = entity.Product?.Price ?? 0,
            CustomerName = entity.Customer?.CustomerName ?? string.Empty,
            FirstName = entity.User?.FirstName ?? string.Empty,
            LastName = entity.User?.LastName ?? string.Empty,
            Email = entity.User?.Email ?? string.Empty,
            UserRole = entity.User?.UserRole ?? string.Empty
        };
    }
}