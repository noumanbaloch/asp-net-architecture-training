using HCM.DBCore.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HCM.DBCore.GenericRepository
{
    public class GenericRespository<TEntity> : IGenericRepsitory<TEntity> where TEntity : class
    {
        private readonly IDatabaseContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRespository(IDatabaseContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public TEntity FindByFirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).FirstOrDefault();
        }

        public async Task<TEntity> FindByFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).FirstOrDefaultAsync();
        }

        public TEntity FindByFirstOrDefaultNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate).FirstOrDefault();
        }

        public async Task<TEntity> FindByFirstOrDefaultNoTrackingAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).ToList();
        }

        public TEntity GetById<TKey>(TKey id)
        {
            return _dbSet.Find(id);
        }

        public async Task<TEntity> GetByIdAsync<TKey>(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
