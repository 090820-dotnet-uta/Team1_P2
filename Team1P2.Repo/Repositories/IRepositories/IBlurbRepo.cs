using Team1P2.Models.Models;

namespace Team1P2.Repo.Repositories.IRepositories
{
    public interface IBlurbRepo : IRepo<Blurb>
    {
        void Update(Blurb blurb);
    }
}
