using Team1P2.Models.Models;

namespace Team1P2.Repo.Repositories.IRepositories
{
    public interface INoteRepo : IRepo<Note>
    {
        void Update(Note note);
    }
}
