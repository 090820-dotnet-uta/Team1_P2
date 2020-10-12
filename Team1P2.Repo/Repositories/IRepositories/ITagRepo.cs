using Team1P2.Models.Models;

namespace Team1P2.Repo.Repositories.IRepositories
{
    public interface ITagRepo : IRepo<Tag>
    {
        void Update(Tag tag);
    }
}
