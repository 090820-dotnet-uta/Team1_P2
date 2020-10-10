using System.Collections.Generic;
using System.Threading.Tasks;

namespace Team1P2.Repo.Repositories.IRepositories
{
    public interface IRepo<T> where T : class
    {
        T Get(int id);
        ValueTask<T> GetAsync(int id);

        ValueTask<List<T>> GetAllAsync();
    }
    
}
