

using Data.Contexts;

namespace Data.Repositories
{
    public class CustomerRepository(DataContext context)
    {
        private readonly DataContext _context = context;
    }
}
