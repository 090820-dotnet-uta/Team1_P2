using Team1P2.Models.Models;

namespace Team1P2.Repo.Repositories.IRepositories
{
    public interface IUserRepo : IRepo<User>
    {
        void Update(User user);
    }
}
