using Dapper;
using HCM.DBCore.Context;
using HCM.DBCore.GenericRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
namespace HCM.DBCore.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDatabaseContext _dbContext;
        private readonly string _connectionString;
        private bool disposed = false;

        public UnitOfWork(IDatabaseContext dbContext, DatabaseContext dbConnectionContext)
        {
            _dbContext = dbContext;
            _connectionString = dbConnectionContext.Database.GetConnectionString();
        }

        public Dictionary<Type, object> _repos = new(); 
        public IGenericRepsitory<TEntity> GetRepsitory<TEntity>() where TEntity : class
        {
            if(_repos == null)
            {
                _repos = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);

            if(!_repos.ContainsKey(type))
            {
                IGenericRepsitory<TEntity> repo = new GenericRespository<TEntity>(_dbContext);
                _repos.Add(type, repo);
            }

            return _repos[type] as IGenericRepsitory<TEntity>;
        }

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync(CancellationToken.None);
        }

        public List<TEntity> DapperSPListWithParams<TEntity>(string spName, DynamicParameters parameters) where TEntity : class
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<TEntity>(spName, parameters, commandTimeout: 60).ToList();
            }
        }

        public List<TEntity> DapperSPListWithoutParams<TEntity>(string spName) where TEntity : class
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<TEntity>(spName, commandTimeout: 60).ToList();
            }
        }

        public TEntity DapperSPSingleWithParams<TEntity>(string spName, DynamicParameters parameters) where TEntity : class
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<TEntity>(spName, parameters, commandTimeout: 60).FirstOrDefault();
            }
        }

        public TEntity DapperSPSingleWithoutParams<TEntity>(string spName) where TEntity : class
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<TEntity>(spName, commandTimeout: 60).FirstOrDefault();
            }
        }
    }
}
