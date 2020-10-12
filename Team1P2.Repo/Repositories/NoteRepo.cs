using Team1P2.Models.Models;
using Team1P2.Repo.Data;
using Team1P2.Repo.Repositories.IRepositories;

namespace Team1P2.Repo.Repositories
{
    public class NoteRepo : Repo<Note>, INoteRepo
    {
        private readonly BlurbDbContext _db;

        public NoteRepo(BlurbDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Note note)
        {
            throw new System.NotImplementedException();
        }
    }
}
