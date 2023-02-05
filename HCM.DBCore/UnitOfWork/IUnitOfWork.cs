using Dapper;
using HCM.DBCore.GenericRepository;
namespace HCM.DBCore.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepsitory<T> GetRepsitory<T>() where T : class;
        List<TEntity> DapperSPListWithParams<TEntity>(string spName, DynamicParameters parameters) where TEntity : class;
        List<TEntity> DapperSPListWithoutParams<TEntity>(string spName) where TEntity : class;
        TEntity DapperSPSingleWithParams<TEntity>(string spName, DynamicParameters parameters) where TEntity : class;
        TEntity DapperSPSingleWithoutParams<TEntity>(string spName) where TEntity : class;
        int Commit();
        Task<int> CommitAsync();
    }
}
