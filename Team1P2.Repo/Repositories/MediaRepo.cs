using Team1P2.Models.Models;
using Team1P2.Repo.Data;
using Team1P2.Repo.Repositories.IRepositories;

namespace Team1P2.Repo.Repositories
{
    public class MediaRepo : Repo<Media>, IMediaRepo
    {
        private readonly BlurbDbContext _db;

        public MediaRepo(BlurbDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Media media)
        {
            throw new System.NotImplementedException();
        }
    }
}
