using Team1P2.Models.Models;
using Team1P2.Repo.Data;
using Team1P2.Repo.Repositories.IRepositories;

namespace Team1P2.Repo.Repositories
{
    public class TagRepo : Repo<Tag>, ITagRepo
    {
        private readonly BlurbDbContext _db;

        public TagRepo(BlurbDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Tag tag)
        {
            throw new System.NotImplementedException();
        }
    }
}
