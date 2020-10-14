using Team1P2.Repo.Data;

namespace Team1P2.Tests
{
    public static class SeedTestDb
    {
        public static void SeedUsers(BlurbDbContext context)
        {

        }


        public static void SeedMediaItems(BlurbDbContext context)
        {

        }


        public static void SeedTags(BlurbDbContext context)
        {

        }


        public static void SeedMediaTags(BlurbDbContext context)
        {

        }


        public static void SeedBlurbs(BlurbDbContext context)
        {

        }


        public static void SeedNotes(BlurbDbContext context)
        {

        }


        public static void SeedDb(BlurbDbContext context)
        {
            SeedUsers(context);
            SeedMediaItems(context);
            SeedTags(context);
            SeedMediaTags(context);
            SeedBlurbs(context);
            SeedNotes(context);
        }
    }
}
