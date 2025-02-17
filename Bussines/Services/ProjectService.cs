
using Bussines.Models;
using Bussines.Interfaces;
using Data.Contexts;
using Data.Entities;
using Bussines.Factories;
using Microsoft.EntityFrameworkCore;

namespace Bussines.Services;

public class ProjectService(DataContext context) : IProjectService
{
    private readonly DataContext _context = context;


    public async Task<ProjectEntity> CreateProjectAsync(ProjectModel model)
    {

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


    public async Task<ProjectEntity> GetProjectByIdAsync(int id)
    {
        var project = await _context.Projects

            .Include(p => p.Customer)
            .Include(p => p.Product)
            .Include(p => p.Status)
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (project == null)
        {
            throw new Exception($"Projekt-ID {id} hittades inte.");
        }

        return project;
    }

    public async Task<bool> UpdateProjectAsync(int id, ProjectEntity updatedProject)
    {
        // Hämta det befintliga projektet från databasen
        var existingProject = await _context.Projects
            .Include(p => p.Customer)
            .Include(p => p.Product)
            .Include(p => p.Status)
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == id);

        // Kontrollera om projektet finns
        if (existingProject == null)
        {
            return false; // Projekt hittades inte
        }

        // Uppdatera fält
        existingProject.Title = updatedProject.Title;
        existingProject.StartDate = updatedProject.StartDate;
        existingProject.EndDate = updatedProject.EndDate;
        existingProject.Status.StatusName = updatedProject.Status.StatusName;
        existingProject.Product.ProductName = updatedProject.Product.ProductName;
        existingProject.Product.Price = updatedProject.Product.Price;
        existingProject.Customer.CustomerName = updatedProject.Customer.CustomerName;
        existingProject.User.FirstName = updatedProject.User.FirstName;
        existingProject.User.LastName = updatedProject.User.LastName;
        existingProject.User.Email = updatedProject.User.Email;
        existingProject.User.UserRole = updatedProject.User.UserRole;

        // Spara ändringarna i databasen
        await _context.SaveChangesAsync();

        return true; // Uppdatering lyckades
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
