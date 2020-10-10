using Microsoft.EntityFrameworkCore;
using Team1P2.Models.Models;

namespace Team1P2.Repo.Data
{
    public class BlurbDbContext : DbContext
    {
        public BlurbDbContext() { }

        public BlurbDbContext(DbContextOptions<BlurbDbContext> options) : base(options)
        {
        }

        public DbSet<Blurb> Blurbs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<MediaTag> MediaTags { get; set; }
    }
}
