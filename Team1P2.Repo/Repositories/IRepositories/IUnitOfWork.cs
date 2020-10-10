using System;

namespace Team1P2.Repo.Repositories.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IBlurbRepo Blurb { get; }
        IUserRepo User { get; }

        void Save();
    }
}
