

using Data.Contexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class StatusTypeRepository(DataContext context)
    {
        private readonly DataContext _context = context;

        // C
        public async Task<StatusTypeEntity> CreateAsync(StatusTypeEntity statustype)
        {
            _context.StatusTypes.Add(statustype);
            await _context.SaveChangesAsync();
            return statustype;
        }

        // R
        public async Task<IEnumerable<StatusTypeEntity>> GetAllAsync()
        {
            return await _context.StatusTypes.ToListAsync();
        }

        // U
        public async Task<StatusTypeEntity?> UpdateAsync(StatusTypeEntity statustype)
        {
            var existingStatusType = await _context.StatusTypes.FirstOrDefaultAsync(s => s.Id == statustype.Id);
            if (existingStatusType != null)
            {
                existingStatusType.StatusName = statustype.StatusName;
                await _context.SaveChangesAsync();
                return existingStatusType;
            }
            return null;

        }

        // D
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.StatusTypes.FirstOrDefaultAsync(x => x.Id == id);
            if (entity != null)
            {
                _context.StatusTypes.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
