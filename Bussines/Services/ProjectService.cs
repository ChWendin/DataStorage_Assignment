
using Bussines.Models;
using Bussines.Interfaces;
using Data.Entities;
using Bussines.Factories;
using Data.Interfaces;

namespace Bussines.Services;

public class ProjectService : IProjectService
{
    private readonly IBaseRepository<ProjectEntity> _projectRepository;
    private readonly ProjectRelatedEntitiesService _relatedEntitiesService;
    public ProjectService(
        IBaseRepository<ProjectEntity> projectRepository,
        ProjectRelatedEntitiesService relatedEntitiesService
    )
    {
        _projectRepository = projectRepository;
        _relatedEntitiesService = relatedEntitiesService;
    }

    public async Task<ProjectEntity> CreateProjectAsync(ProjectModel model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));

        var project = ProjectFactory.CreateProject(model);
        await _projectRepository.AddAsync(project);

        return project;
    }

    public async Task<IEnumerable<ProjectEntity>> GetAllProjectsAsync()
    {
        return await _projectRepository.GetAllIncludingAsync(p => p.Customer, p => p.Product, p => p.Status, p => p.User);
    }

    public async Task<ProjectEntity?> GetProjectByIdAsync(int id)
    {
        return await _projectRepository.GetIncludingAsync(p => p.Id == id, p => p.Customer, p => p.Product, p => p.Status, p => p.User);
    }


    public async Task<bool> UpdateProjectAsync(int id, ProjectEntity updatedProject)
    {
        if (updatedProject == null)
            throw new ArgumentNullException(nameof(updatedProject));

        var existingProject = await _projectRepository.GetIncludingAsync(p => p.Id == id, p => p.Customer, p => p.Product, p => p.Status, p => p.User);
        if (existingProject == null)
            return false;

        await _relatedEntitiesService.UpdateRelatedEntitiesAsync(existingProject, updatedProject);

        await _projectRepository.UpdateAsync(existingProject);
        return true;
    }



    public async Task<bool> DeleteProjectAsync(int id)
    {
        var project = await _projectRepository.GetIncludingAsync(
            p => p.Id == id,
            p => p.Customer,
            p => p.Product,
            p => p.Status,
            p => p.User
        );

        if (project == null)
            return false;

        // Ta bort relaterade entiteter
        await _relatedEntitiesService.DeleteRelatedEntitiesAsync(project);

        // Ta bort projektet
        await _projectRepository.RemoveAsync(project);

        return true;
    }
}
