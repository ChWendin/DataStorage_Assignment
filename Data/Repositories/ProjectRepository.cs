

using Data.Contexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class ProjectRepository(DataContext context)
    {
        private readonly DataContext _context = context;

        // C
        public async Task<ProjectEntity> CreateAsync(ProjectEntity project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        // R
        public async Task<IEnumerable<ProjectEntity>> GetAllAsync()
        {
            return await _context.Projects.ToListAsync();
        }

        // U
        public async Task<ProjectEntity?> UpdateAsync(ProjectEntity project)
        {
            var existingProject = await _context.Projects.FirstOrDefaultAsync(p => p.Id == project.Id);
            if (existingProject != null)
            {
                existingProject.Title = project.Title;
                existingProject.StartDate = project.StartDate;
                existingProject.EndDate = project.EndDate;
                await _context.SaveChangesAsync();
                return existingProject;
            }
            return null;

        }

        // D
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);
            if (entity != null)
            {
                _context.Projects.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
