using Team1P2.Repo.Data;
using Team1P2.Repo.DbManipulationMethods;
using Team1P2.Repo.Repositories.IRepositories;

namespace Team1P2.Repo.Repositories
{
  public class UnitOfWork : IUnitOfWork
  {
    private readonly BlurbDbContext _db;

    public UnitOfWork(BlurbDbContext db)
    {

      _db = db;

      // UNCOMMENT TO CALL SEED DB WHEN YOU START THE APP.
      // REMEMBER TO COMMENT IT BACK AND RERUN AFTER
      //DbManip.SeedDb(db);
      Blurb = new BlurbRepo(_db);
      User = new UserRepo(_db);
      Media = new MediaRepo(_db);
      MediaTag = new MediaTagRepo(_db);
      Note = new NoteRepo(_db);
      Tag = new TagRepo(_db);
    }

    public IBlurbRepo Blurb { get; private set; }

    public IUserRepo User { get; private set; }

    public IMediaRepo Media { get; private set; }

    public IMediaTagRepo MediaTag { get; private set; }

    public INoteRepo Note { get; private set; }

    public ITagRepo Tag { get; private set; }

    public void Dispose()
    {
      _db.Dispose();
    }

    public void Save()
    {
      _db.SaveChanges();
    }
  }
}
