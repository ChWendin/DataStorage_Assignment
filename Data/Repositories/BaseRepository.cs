


using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _dbSet.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var entityId = entity.GetType().GetProperty("Id")?.GetValue(entity);
            if (entityId == null)
                throw new InvalidOperationException("Entity must have an Id property.");

            var existingEntity = await _dbSet.AsQueryable().FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == (int)entityId);
            if (existingEntity == null)
                throw new InvalidOperationException("Entity not found in database.");

            // uppdatering av relaterade entiteter
            if (existingEntity is ProjectEntity projectEntity && entity is ProjectEntity updateEntity)
            {
                // Uppdatera Status
                if (updateEntity.Status != null)
                {
                    var existingStatus = await _context.Set<StatusTypeEntity>()
                        .FirstOrDefaultAsync(s => s.StatusName == updateEntity.Status.StatusName);
                    if (existingStatus != null)
                    {
                        _context.Attach(existingStatus);
                        projectEntity.StatusId = existingStatus.StatusId;
                        projectEntity.Status = existingStatus;  
                    }
                }

                // Uppdatera Customer
                if (updateEntity.Customer != null)
                {
                    var existingCustomer = await _context.Set<CustomerEntity>()
                        .FirstOrDefaultAsync(c => c.CustomerName == updateEntity.Customer.CustomerName);
                    if (existingCustomer != null)
                    {
                        _context.Attach(existingCustomer);
                        projectEntity.CustomerId = existingCustomer.CustomerId;
                        projectEntity.Customer = existingCustomer;  
                    }
                }

                // Uppdatera Product
                if (updateEntity.Product != null)
                {
                    var existingProduct = await _context.Set<ProductEntity>()
                        .FirstOrDefaultAsync(p => p.ProductName == updateEntity.Product.ProductName);
                    if (existingProduct != null)
                    {
                        _context.Attach(existingProduct);
                        projectEntity.ProductId = existingProduct.ProductId;
                        projectEntity.Product = existingProduct; 
                    }
                }

                // Uppdatera User
                if (updateEntity.User != null)
                {
                    var existingUser = await _context.Set<UserEntity>()
                        .FirstOrDefaultAsync(u => u.FirstName == updateEntity.User.FirstName && u.LastName == updateEntity.User.LastName);
                    if (existingUser != null)
                    {
                        _context.Attach(existingUser);
                        projectEntity.UserId = existingUser.UserId;
                        projectEntity.User = existingUser;  
                    }
                }

                // Efter uppdatering av relaterade entiteter, uppdatera projektet
                _context.Entry(existingEntity).CurrentValues.SetValues(updateEntity);
            }

            await SaveChangesAsync();
        }

        public async Task RemoveAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _context.Entry(entity).State = EntityState.Deleted;

            await SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllIncludingAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity?> GetIncludingAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(predicate);
        }


        private async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
