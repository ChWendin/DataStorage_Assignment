
using Bussines.Models;
using Bussines.Interfaces;
using Data.Contexts;
using Data.Entities;
using Bussines.Factories;
using Microsoft.EntityFrameworkCore;
using Data.Repositories;

namespace Bussines.Services;

public class ProjectService(DataContext context) : IProjectService
{
    private readonly DataContext _context = context;


    public async Task<ProjectEntity> CreateProjectAsync(ProjectModel model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));

        var project = ProjectFactory.CreateProject(model);

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        return project;
    }

    public async Task<IEnumerable<ProjectEntity>> GetAllProjectsAsync()
    {
        return await _context.Projects
            .Include(p => p.Customer)
            .Include(p => p.Product)
            .Include(p => p.Status)
            .Include(p => p.User)

            .ToListAsync();
    }

    //public async Task<IEnumerable<ProjectModel>> GetAllProjectsAsync()
    //{
    //    var projects = await _projectRepository.GetAllIncludingAsync(p => p.Status, p => p.Customer, p => p.Product, p => p.User);
    //    return projects.Select(ProjectFactory.MapToModel)!;
    //}

    //public async Task<ProjectModel> GetProjectByIdAsync(int id)
    //{
    //    var project = await _projectRepository.GetIncludingAsync(p => p.Id == id, p => p.Status, p => p.Customer, p => p.Product, p => p.User);
    //    return ProjectFactory.MapToModel(project!)!;
    //}


    public async Task<ProjectEntity?> GetProjectByIdAsync(int id)
    {
        try
        {
            return await _context.Projects
                .Include(p => p.Customer)
                .Include(p => p.Product)
                .Include(p => p.Status)
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fel vid hämtning av projekt: {ex.Message}");
            return null;
        }
    }

    public async Task<bool> UpdateProjectAsync(int id, ProjectEntity updatedProject)
    {
        if (updatedProject == null)
            throw new ArgumentNullException(nameof(updatedProject), "Det uppdaterade projektet får inte vara null.");

        var existingProject = await _context.Projects
            .Include(p => p.Customer)
            .Include(p => p.Product)
            .Include(p => p.Status)
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (existingProject == null)
            return false;

        // Uppdatera endast om nytt värde fylls i
        if (!string.IsNullOrWhiteSpace(updatedProject.Title))
            existingProject.Title = updatedProject.Title;

        if (updatedProject.StartDate != default)
            existingProject.StartDate = updatedProject.StartDate;

        if (updatedProject.EndDate != default)
            existingProject.EndDate = updatedProject.EndDate;

        if (!string.IsNullOrWhiteSpace(updatedProject.Status.StatusName))
        {
            var status = await _context.StatusTypes.FirstOrDefaultAsync(s => s.StatusName == updatedProject.Status.StatusName);
            if (status != null)
                existingProject.Status = status;
        }

        if (!string.IsNullOrWhiteSpace(updatedProject.Customer.CustomerName))
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerName == updatedProject.Customer.CustomerName);
            if (customer != null)
                existingProject.Customer = customer;
        }

        if (!string.IsNullOrWhiteSpace(updatedProject.Product.ProductName))
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductName == updatedProject.Product.ProductName);
            if (product != null)
            {
                existingProject.Product = product;
                existingProject.Product.Price = updatedProject.Product.Price;
            }
        }

        if (!string.IsNullOrWhiteSpace(updatedProject.User.FirstName) || !string.IsNullOrWhiteSpace(updatedProject.User.LastName))
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == updatedProject.User.Email);
            if (user != null)
            {
                existingProject.User = user;
                existingProject.User.FirstName = updatedProject.User.FirstName;
                existingProject.User.LastName = updatedProject.User.LastName;
            }
        }

        await _context.SaveChangesAsync();
        return true;
    }



    public async Task<bool> DeleteProjectAsync(int id)
    {
        var project = await _context.Projects
            .Include(p => p.Customer)
            .Include(p => p.Product)
            .Include(p => p.Status)
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (project == null)
            return false;

        // Ta bort projektet
        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
        return true;
    }
}
