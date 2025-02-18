using Bussines.Models;
using Data.Entities;

namespace Bussines.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectEntity> CreateProjectAsync(ProjectModel model);
        Task<bool> DeleteProjectAsync(int id);
        Task<IEnumerable<ProjectEntity>> GetAllProjectsAsync();
        Task<ProjectEntity?> GetProjectByIdAsync(int id);
        Task<bool> UpdateProjectAsync(int id, ProjectEntity updatedProject);
    }
}