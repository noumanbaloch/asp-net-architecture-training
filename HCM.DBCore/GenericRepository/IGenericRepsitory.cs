using System.Linq.Expressions;

namespace HCM.DBCore.GenericRepository
{
    public interface IGenericRepsitory<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        TEntity GetById<TKey>(TKey id);
        Task<TEntity> GetByIdAsync<TKey>(TKey id);
        TEntity FindByFirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FindByFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        TEntity FindByFirstOrDefaultNoTracking(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FindByFirstOrDefaultNoTrackingAsync(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();







    }
}