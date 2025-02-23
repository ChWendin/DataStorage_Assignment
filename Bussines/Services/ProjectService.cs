
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
    private readonly ITransactionService _transactionService;

    public ProjectService(
        IBaseRepository<ProjectEntity> projectRepository,
        ProjectRelatedEntitiesService relatedEntitiesService,
        ITransactionService transactionService
    )
    {
        _projectRepository = projectRepository;
        _relatedEntitiesService = relatedEntitiesService;
        _transactionService = transactionService;
    }

    public async Task<ProjectEntity> CreateProjectAsync(ProjectModel model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));

        await _transactionService.BeginTransactionAsync(); 

        try
        {
            var project = ProjectFactory.CreateProject(model);
            await _projectRepository.AddAsync(project);

            await _transactionService.CommitAsync(); 
            return project;
        }
        catch
        {
            await _transactionService.RollbackAsync(); 
            throw;
        }
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

        await _transactionService.BeginTransactionAsync(); 

        try
        {
            var existingProject = await _projectRepository.GetIncludingAsync(p => p.Id == id, p => p.Customer, p => p.Product, p => p.Status, p => p.User);
            if (existingProject == null)
                return false;

            await _relatedEntitiesService.UpdateRelatedEntitiesAsync(existingProject, updatedProject);
            await _projectRepository.UpdateAsync(existingProject);

            await _transactionService.CommitAsync(); 
            return true;
        }
        catch
        {
            await _transactionService.RollbackAsync(); 
            throw;
        }
    }



    public async Task<bool> DeleteProjectAsync(int id)
    {
        await _transactionService.BeginTransactionAsync(); // Starta transaktion

        try
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

            await _relatedEntitiesService.DeleteRelatedEntitiesAsync(project);
            await _projectRepository.RemoveAsync(project);

            await _transactionService.CommitAsync(); // Bekräfta raderingen
            return true;
        }
        catch
        {
            await _transactionService.RollbackAsync(); // Rulla tillbaka vid fel
            throw;
        }
    }
}
