﻿using System.Linq.Expressions;

namespace Data.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression);
        Task RemoveAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
    }
}