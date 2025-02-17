

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

    //public IEnumerable<ProjectModel> Create(IEnumerable<ProjectEntity> projectEntities)
    //{
    //    if (projectEntities == null)
    //        throw new ArgumentNullException(nameof(projectEntities));

    //    return projectEntities.Select(projectEntity => new ProjectModel
    //    {
    //        Id = projectEntity.Id,
    //        Name = projectEntity.Name,
    //        CustomerName = projectEntity.Customer?.Name ?? "Unknown Customer",
    //        ProductName = projectEntity.Product?.Name ?? "Unknown Product",
    //        StatusName = projectEntity.Status?.Name ?? "Unknown Status",
    //        UserName = projectEntity.User?.Name ?? "Unknown User"
    //    }).ToList();
    //}

}
