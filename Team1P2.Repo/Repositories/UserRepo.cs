using Team1P2.Models.Models;
using Team1P2.Repo.Data;
using Team1P2.Repo.Repositories.IRepositories;

namespace Team1P2.Repo.Repositories
{
    public class UserRepo : Repo<User>, IUserRepo
    {
        private readonly BlurbDbContext _db;

        public UserRepo(BlurbDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}
