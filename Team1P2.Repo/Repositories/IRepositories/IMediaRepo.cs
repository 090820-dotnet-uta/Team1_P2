using Team1P2.Models.Models;

namespace Team1P2.Repo.Repositories.IRepositories
{
    public interface IMediaRepo : IRepo<Media>
    {
        void Update(Media media);
    }
}
