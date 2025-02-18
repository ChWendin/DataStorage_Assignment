

using Data.Entities;
using Bussines.Models;

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

}
