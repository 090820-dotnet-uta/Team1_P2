using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Team1P2.Repo.Data;
using Team1P2.Repo.Repositories.IRepositories;

namespace Team1P2.Repo.Repositories
{
    public class Repo<T> : IRepo<T> where T : class
    {
        private readonly BlurbDbContext _db;
        internal DbSet<T> dbSet;

        public Repo(BlurbDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }
        public ValueTask<T> GetAsync(int id)
        {
            return dbSet.FindAsync(id);
        }

        public async ValueTask<List<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }
    }
}
