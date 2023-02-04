using HCM.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HCM.DBCore.Context
{
    public class DatabaseContext: DbContext,  IDatabaseContext
    {
        public DatabaseContext(DbContextOptions options) : base (options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UserEntity> UserEntity { get; set; }

    }
}
