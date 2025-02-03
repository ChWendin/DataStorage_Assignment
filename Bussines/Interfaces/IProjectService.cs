using Data.Entities;

namespace Bussines.Interfaces
{
    public interface IProjectService
    {
        ProjectEntity CreateProject(ProjectEntity projectEntity);
        bool DeleteProjectById(int id);
        IEnumerable<ProjectEntity> GetAllProjects();
        ProjectEntity GetProjectById(int id);
        ProjectEntity UpdateProject(ProjectEntity projectEntity);
    }
}