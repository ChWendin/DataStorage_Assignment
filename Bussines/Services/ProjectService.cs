

using Bussines.Interfaces;
using Data.Contexts;
using Data.Entities;

namespace Bussines.Services;

public class ProjectService(DataContext context) : IProjectService
{
    private readonly DataContext _context = context;


    public ProjectEntity CreateProject(ProjectEntity projectEntity) //Skapa DTO/Modell att stoppa in här istället
    {
        _context.Projects.Add(projectEntity);
        _context.SaveChanges();

        return projectEntity;
    }

    public IEnumerable<ProjectEntity> GetAllProjects()
    {
        return _context.Projects;
    }

    public ProjectEntity GetProjectById(int id)
    {
        var projectEntity = _context.Projects.SingleOrDefault(x => x.Id == id);
        return projectEntity ?? null!;
    }

    public ProjectEntity UpdateProject(ProjectEntity projectEntity)
    {
        _context.Projects.Update(projectEntity);
        _context.SaveChanges();

        return projectEntity;
    }

    public bool DeleteProjectById(int id)
    {
        var projectEntity = _context.Projects.SingleOrDefault(x => x.Id == id);
        if (projectEntity != null)
        {
            _context.Remove(projectEntity);
            _context.SaveChanges();

            return true;
        }
        else
        {
            return false;
        }
    }
}
