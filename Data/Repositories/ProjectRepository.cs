

using Data.Contexts;

namespace Data.Repositories
{
    public class ProjectRepository(DataContext context)
    {
        private readonly DataContext _context = context;
    }
}
