using System;
using Team1P2.Repo.Data;
using Team1P2.Repo.Migrations;
using Team1P2.Repo.Repositories.IRepositories;

namespace Team1P2.Repo.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlurbDbContext _db;

        public UnitOfWork(BlurbDbContext db)
        {
            _db = db;
            Blurb = new BlurbRepo(_db);
            User = new UserRepo(_db);
        }

        public IBlurbRepo Blurb { get; private set; }

        public IUserRepo User { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
