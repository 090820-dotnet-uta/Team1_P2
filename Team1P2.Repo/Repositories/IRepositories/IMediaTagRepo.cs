using Team1P2.Models.Models;

namespace Team1P2.Repo.Repositories.IRepositories
{
    public interface IMediaTagRepo : IRepo<MediaTag>
    {
        void Update(MediaTag mediaTag);
    }
}
