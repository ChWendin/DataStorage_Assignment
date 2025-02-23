
using Bussines.Models;
using Bussines.Interfaces;
using Data.Contexts;
using Data.Entities;
using Bussines.Factories;
using Microsoft.EntityFrameworkCore;
using Data.Repositories;
using Data.Interfaces;

namespace Bussines.Services;

public class ProjectService(
    IBaseRepository<ProjectEntity> projectRepository,
    IBaseRepository<StatusTypeEntity> statusRepository,
    IBaseRepository<ProductEntity> productRepository,
    IBaseRepository<UserEntity> userRepository,
    IBaseRepository<CustomerEntity> customerRepository
) : IProjectService
{
    private readonly IBaseRepository<ProjectEntity> _projectRepository = projectRepository;
    private readonly IBaseRepository<StatusTypeEntity> _statusRepository = statusRepository;
    private readonly IBaseRepository<ProductEntity> _productRepository = productRepository;
    private readonly IBaseRepository<UserEntity> _userRepository = userRepository;
    private readonly IBaseRepository<CustomerEntity> _customerRepository = customerRepository;

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

        // Uppdatera endast om nytt värde är satt
        if (!string.IsNullOrWhiteSpace(updatedProject.Title))
            existingProject.Title = updatedProject.Title;

        if (updatedProject.StartDate != default)
            existingProject.StartDate = updatedProject.StartDate;

        if (updatedProject.EndDate != default)
            existingProject.EndDate = updatedProject.EndDate;

        // Uppdatera Status
        if (!string.IsNullOrWhiteSpace(updatedProject.Status.StatusName))
        {
            var status = await _statusRepository.GetAsync(s => s.StatusName == updatedProject.Status.StatusName);
            if (status != null)
                existingProject.Status = status;
        }

        // Uppdatera Product
        if (!string.IsNullOrWhiteSpace(updatedProject.Product.ProductName))
        {
            var product = await _productRepository.GetAsync(p => p.ProductName == updatedProject.Product.ProductName);
            if (product != null)
            {
                existingProject.Product = product;
                existingProject.Product.Price = updatedProject.Product.Price;
            }
        }

        // Uppdatera User
        if (!string.IsNullOrWhiteSpace(updatedProject.User.Email))
        {
            var user = await _userRepository.GetAsync(u => u.Email == updatedProject.User.Email);
            if (user != null)
            {
                existingProject.User = user;
                existingProject.User.FirstName = updatedProject.User.FirstName;
                existingProject.User.LastName = updatedProject.User.LastName;
            }
        }

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

        // Radera de relaterade entiteter, om de finns
        if (project.Customer != null)
            await _customerRepository.RemoveAsync(project.Customer);

        if (project.Product != null)
            await _productRepository.RemoveAsync(project.Product);

        if (project.Status != null)
            await _statusRepository.RemoveAsync(project.Status);

        if (project.User != null)
            await _userRepository.RemoveAsync(project.User);

        // Radera projektet sist
        await _projectRepository.RemoveAsync(project);

        return true;
    }
}
