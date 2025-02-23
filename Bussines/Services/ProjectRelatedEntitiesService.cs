

using Bussines.Interfaces;
using Data.Entities;
using Data.Interfaces;

namespace Bussines.Services;

public class ProjectRelatedEntitiesService
{
    private readonly IBaseRepository<StatusTypeEntity> _statusRepository;
    private readonly IBaseRepository<ProductEntity> _productRepository;
    private readonly IBaseRepository<UserEntity> _userRepository;
    private readonly IBaseRepository<CustomerEntity> _customerRepository;
    private readonly ITransactionService _transactionService;

    public ProjectRelatedEntitiesService(
        IBaseRepository<StatusTypeEntity> statusRepository,
        IBaseRepository<ProductEntity> productRepository,
        IBaseRepository<UserEntity> userRepository,
        IBaseRepository<CustomerEntity> customerRepository,
        ITransactionService transactionService
    )
    {
        _statusRepository = statusRepository;
        _productRepository = productRepository;
        _userRepository = userRepository;
        _customerRepository = customerRepository;
        _transactionService = transactionService;
    }

    public async Task UpdateRelatedEntitiesAsync(ProjectEntity existingProject, ProjectEntity updatedProject)
    {
        try
        {
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
        }
        catch
        {
            await _transactionService.RollbackAsync(); 
            throw;
        }
    }

    public async Task DeleteRelatedEntitiesAsync(ProjectEntity project)
    {
        try
        {
            if (project.Customer != null)
                await _customerRepository.RemoveAsync(project.Customer);

            if (project.Product != null)
                await _productRepository.RemoveAsync(project.Product);

            if (project.Status != null)
                await _statusRepository.RemoveAsync(project.Status);

            if (project.User != null)
                await _userRepository.RemoveAsync(project.User);
        }
        catch
        {
            await _transactionService.RollbackAsync(); // Rulla tillbaka vid fel
            throw;
        }
    }
}
