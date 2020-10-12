using Team1P2.Models.Models;
using Team1P2.Repo.Data;
using Team1P2.Repo.Repositories.IRepositories;

namespace Team1P2.Repo.Repositories
{
    public class MediaTagRepo : Repo<MediaTag>, IMediaTagRepo
    {
        private readonly BlurbDbContext _db;

        public MediaTagRepo(BlurbDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(MediaTag mediaTag)
        {
            throw new System.NotImplementedException();
        }
    }
}
