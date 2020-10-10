using Team1P2.Models.Models;
using Team1P2.Repo.Data;
using Team1P2.Repo.Repositories.IRepositories;

namespace Team1P2.Repo.Repositories
{
    public class BlurbRepo : Repo<Blurb>, IBlurbRepo
    {
        private readonly BlurbDbContext _db;

        public BlurbRepo(BlurbDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Blurb blurb)
        {
            throw new System.NotImplementedException();
        }
    }
}
