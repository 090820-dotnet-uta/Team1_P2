using System;

namespace Team1P2.Repo.Repositories.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IBlurbRepo Blurb { get; }
        IUserRepo User { get; }
        IMediaRepo Media { get; }
        IMediaTagRepo MediaTag { get; }
        INoteRepo Note { get; }
        ITagRepo Tag { get; }

        void Save();
    }
}
