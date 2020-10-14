using Team1P2.Models.Models;
using Team1P2.Repo.Data;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Team1P2.Models.Models.Enums;
using System;
using Team1P2.Repo.DbManipulationMethods;

namespace Team1P2.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void SortBlurbs_AZ()
        {
            var options = new DbContextOptionsBuilder<BlurbDbContext>()
                .UseInMemoryDatabase(databaseName: "SortBlurbs_AZ")
                .Options;

            using (var context = new BlurbDbContext(options))
            {
                //Arrange

                //STEP 1: SET UP THE IN-MEMORY DB
                Media media1 = new Media(Models.Models.Type.Movie, "Halloween");                  //Add the movies to the DB
                Media media2 = new Media(Models.Models.Type.Movie, "Scream");
                Media media3 = new Media(Models.Models.Type.Movie, "World War Z");
                context.Add(media1);
                context.Add(media2);
                context.Add(media3);
                context.SaveChanges();

                Tag tag1 = new Tag("Horror");                                       //Add the tags for movie1 to the DB
                Tag tag2 = new Tag("Thriller");
                context.Add(tag1);
                context.Add(tag2);
                context.SaveChanges();

                MediaTag mediaTag1 = new MediaTag(tag1, media1);                    //Maps the two tags to the media1 item
                MediaTag mediaTag2 = new MediaTag(tag2, media1);
                MediaTag mediaTag3 = new MediaTag(tag1, media2);                    //Maps the two tags to the media2 item
                MediaTag mediaTag4 = new MediaTag(tag2, media2);
                MediaTag mediaTag5 = new MediaTag(tag1, media3);                    //Maps the two tags to the media3 item
                MediaTag mediaTag6 = new MediaTag(tag2, media3);
                context.Add(mediaTag1);
                context.Add(mediaTag2);
                context.Add(mediaTag3);
                context.Add(mediaTag4);
                context.Add(mediaTag5);
                context.Add(mediaTag6);
                context.SaveChanges();

                //Make sure that we get what's in the DB with the proper IDs and MediaTags lists
                media1 = context.Medias.Include(m => m.MediaTags).FirstOrDefault(m => m.Name == media1.Name);
                tag1 = context.Tags.Include(t => t.MediaTags).FirstOrDefault(t => t.Name == tag1.Name);
                tag2 = context.Tags.Include(t => t.MediaTags).FirstOrDefault(t => t.Name == tag2.Name);

                //Make an empty user to pass into the blurbs
                User user = new User();
                
                Blurb blurbFirst = new Blurb(user, 7, media1);
                Blurb blurbMiddle = new Blurb(user, 8, media2);
                Blurb blurbLast = new Blurb(user, 9, media3);

                //Add the blurbs to the list out of order
                List<Blurb> blurbsUnsorted = new List<Blurb>();
                blurbsUnsorted.Add(blurbMiddle);
                blurbsUnsorted.Add(blurbFirst);
                blurbsUnsorted.Add(blurbLast);

                List<Blurb> blurbsSortedComparison = new List<Blurb>();
                blurbsSortedComparison.Add(blurbFirst);
                blurbsSortedComparison.Add(blurbMiddle);
                blurbsSortedComparison.Add(blurbLast);

                List<Blurb> sortedListAZ = new List<Blurb>(); //set up empty lists to copy the sorted ones into

                //Act
                sortedListAZ = DbManip.SortBlurbs(blurbsUnsorted.AsQueryable<Blurb>(), SortSetting.AZ).ToList();

                //Assert
                Assert.Equal(blurbsSortedComparison, sortedListAZ);
            }

        }



        [Fact]
        public void SortBlurbs_ZA()
        {
            var options = new DbContextOptionsBuilder<BlurbDbContext>()
                .UseInMemoryDatabase(databaseName: "SortBlurbs_ZA")
                .Options;

            using (var context = new BlurbDbContext(options))
            {
                //Arrange

                //STEP 1: SET UP THE IN-MEMORY DB
                Media media1 = new Media(Models.Models.Type.Movie, "Halloween");                  //Add the movies to the DB
                Media media2 = new Media(Models.Models.Type.Movie, "Scream");
                Media media3 = new Media(Models.Models.Type.Movie, "World War Z");
                context.Add(media1);
                context.Add(media2);
                context.Add(media3);
                context.SaveChanges();

                Tag tag1 = new Tag("Horror");                                       //Add the tags for movie1 to the DB
                Tag tag2 = new Tag("Thriller");
                context.Add(tag1);
                context.Add(tag2);
                context.SaveChanges();

                MediaTag mediaTag1 = new MediaTag(tag1, media1);                    //Maps the two tags to the media1 item
                MediaTag mediaTag2 = new MediaTag(tag2, media1);
                MediaTag mediaTag3 = new MediaTag(tag1, media2);                    //Maps the two tags to the media2 item
                MediaTag mediaTag4 = new MediaTag(tag2, media2);
                MediaTag mediaTag5 = new MediaTag(tag1, media3);                    //Maps the two tags to the media3 item
                MediaTag mediaTag6 = new MediaTag(tag2, media3);
                context.Add(mediaTag1);
                context.Add(mediaTag2);
                context.Add(mediaTag3);
                context.Add(mediaTag4);
                context.Add(mediaTag5);
                context.Add(mediaTag6);
                context.SaveChanges();

                //Make sure that we get what's in the DB with the proper IDs and MediaTags lists
                media1 = context.Medias.Include(m => m.MediaTags).FirstOrDefault(m => m.Name == media1.Name);
                tag1 = context.Tags.Include(t => t.MediaTags).FirstOrDefault(t => t.Name == tag1.Name);
                tag2 = context.Tags.Include(t => t.MediaTags).FirstOrDefault(t => t.Name == tag2.Name);

                //Make an empty user to pass into the blurbs
                User user = new User();

                Blurb blurbFirst = new Blurb(user, 7, media1);
                Blurb blurbMiddle = new Blurb(user, 8, media2);
                Blurb blurbLast = new Blurb(user, 9, media3);

                //Add the blurbs to the list out of order
                List<Blurb> blurbsUnsorted = new List<Blurb>();
                blurbsUnsorted.Add(blurbMiddle);
                blurbsUnsorted.Add(blurbFirst);
                blurbsUnsorted.Add(blurbLast);

                List<Blurb> blurbsSortedComparison = new List<Blurb>();
                blurbsSortedComparison.Add(blurbLast);
                blurbsSortedComparison.Add(blurbMiddle);
                blurbsSortedComparison.Add(blurbFirst);

                List<Blurb> sortedListZA = new List<Blurb>(); //set up empty lists to copy the sorted ones into

                //Act
                sortedListZA = DbManip.SortBlurbs(blurbsUnsorted.AsQueryable<Blurb>(), SortSetting.ZA).ToList();

                //Assert
                Assert.Equal(blurbsSortedComparison, sortedListZA);
            }
        }


        [Fact]
        public void SortBlurbs_MostRecent()
        {
            var options = new DbContextOptionsBuilder<BlurbDbContext>()
                .UseInMemoryDatabase(databaseName: "SortBlurbs_MostRecent")
                .Options;

            using (var context = new BlurbDbContext(options))
            {
                //Arrange

                //STEP 1: SET UP THE IN-MEMORY DB
                Media media1 = new Media(Models.Models.Type.Movie, "");                  //Add the movies to the DB
                Media media2 = new Media(Models.Models.Type.Movie, "");
                Media media3 = new Media(Models.Models.Type.Movie, "");
                context.Add(media1);
                context.Add(media2);
                context.Add(media3);
                context.SaveChanges();

                Tag tag1 = new Tag("Horror");                                       //Add the tags for movie1 to the DB
                Tag tag2 = new Tag("Thriller");
                context.Add(tag1);
                context.Add(tag2);
                context.SaveChanges();

                MediaTag mediaTag1 = new MediaTag(tag1, media1);                    //Maps the two tags to the media1 item
                MediaTag mediaTag2 = new MediaTag(tag2, media1);
                MediaTag mediaTag3 = new MediaTag(tag1, media2);                    //Maps the two tags to the media2 item
                MediaTag mediaTag4 = new MediaTag(tag2, media2);
                MediaTag mediaTag5 = new MediaTag(tag1, media3);                    //Maps the two tags to the media3 item
                MediaTag mediaTag6 = new MediaTag(tag2, media3);
                context.Add(mediaTag1);
                context.Add(mediaTag2);
                context.Add(mediaTag3);
                context.Add(mediaTag4);
                context.Add(mediaTag5);
                context.Add(mediaTag6);
                context.SaveChanges();

                //Make sure that we get what's in the DB with the proper IDs and MediaTags lists
                media1 = context.Medias.Include(m => m.MediaTags).FirstOrDefault(m => m.Name == media1.Name);
                tag1 = context.Tags.Include(t => t.MediaTags).FirstOrDefault(t => t.Name == tag1.Name);
                tag2 = context.Tags.Include(t => t.MediaTags).FirstOrDefault(t => t.Name == tag2.Name);

                //Make an empty user to pass into the blurbs
                User user = new User();

                Blurb blurbFirst = new Blurb(user, 7, media1) { BlurbId = 1, Timestamp = new DateTime(2020, 4, 10) };
                Blurb blurbMiddle = new Blurb(user, 8, media2) { BlurbId = 2, Timestamp = new DateTime(2020, 7, 20) };
                Blurb blurbLast = new Blurb(user, 6, media3) { BlurbId = 3, Timestamp = new DateTime(2020, 9, 30) };

                //Add the blurbs to the list out of order
                List<Blurb> blurbsUnsorted = new List<Blurb>();
                blurbsUnsorted.Add(blurbMiddle);
                blurbsUnsorted.Add(blurbFirst);
                blurbsUnsorted.Add(blurbLast);

                List<Blurb> blurbsSortedComparison = new List<Blurb>();
                blurbsSortedComparison.Add(blurbLast);
                blurbsSortedComparison.Add(blurbMiddle);
                blurbsSortedComparison.Add(blurbFirst);

                List<Blurb> sortedListMostRecent = new List<Blurb>(); //set up empty lists to copy the sorted ones into

                //Act
                sortedListMostRecent = DbManip.SortBlurbs(blurbsUnsorted.AsQueryable<Blurb>(), SortSetting.MostRecent).ToList();

                //Assert
                Assert.Equal(blurbsSortedComparison, sortedListMostRecent);
            }
        }



        [Fact]
        public void SortBlurbs_LeastRecent()
        {
            var options = new DbContextOptionsBuilder<BlurbDbContext>()
                .UseInMemoryDatabase(databaseName: "SortBlurbs_LeastRecent")
                .Options;

            using (var context = new BlurbDbContext(options))
            {
                //Arrange

                //STEP 1: SET UP THE IN-MEMORY DB
                Media media1 = new Media(Models.Models.Type.Movie, "");                  //Add the movies to the DB
                Media media2 = new Media(Models.Models.Type.Movie, "");
                Media media3 = new Media(Models.Models.Type.Movie, "");
                context.Add(media1);
                context.Add(media2);
                context.Add(media3);
                context.SaveChanges();

                Tag tag1 = new Tag("Horror");                                       //Add the tags for movie1 to the DB
                Tag tag2 = new Tag("Thriller");
                context.Add(tag1);
                context.Add(tag2);
                context.SaveChanges();

                MediaTag mediaTag1 = new MediaTag(tag1, media1);                    //Maps the two tags to the media1 item
                MediaTag mediaTag2 = new MediaTag(tag2, media1);
                MediaTag mediaTag3 = new MediaTag(tag1, media2);                    //Maps the two tags to the media2 item
                MediaTag mediaTag4 = new MediaTag(tag2, media2);
                MediaTag mediaTag5 = new MediaTag(tag1, media3);                    //Maps the two tags to the media3 item
                MediaTag mediaTag6 = new MediaTag(tag2, media3);
                context.Add(mediaTag1);
                context.Add(mediaTag2);
                context.Add(mediaTag3);
                context.Add(mediaTag4);
                context.Add(mediaTag5);
                context.Add(mediaTag6);
                context.SaveChanges();

                //Make sure that we get what's in the DB with the proper IDs and MediaTags lists
                media1 = context.Medias.Include(m => m.MediaTags).FirstOrDefault(m => m.Name == media1.Name);
                tag1 = context.Tags.Include(t => t.MediaTags).FirstOrDefault(t => t.Name == tag1.Name);
                tag2 = context.Tags.Include(t => t.MediaTags).FirstOrDefault(t => t.Name == tag2.Name);

                //Make an empty user to pass into the blurbs
                User user = new User();

                Blurb blurbFirst = new Blurb(user, 7, media1) { BlurbId = 1, Timestamp = new DateTime(2020, 4, 10) };
                Blurb blurbMiddle = new Blurb(user, 8, media2) { BlurbId = 2, Timestamp = new DateTime(2020, 7, 20) };
                Blurb blurbLast = new Blurb(user, 6, media3) { BlurbId = 3, Timestamp = new DateTime(2020, 9, 30) };

                //Add the blurbs to the list out of order
                List<Blurb> blurbsUnsorted = new List<Blurb>();
                blurbsUnsorted.Add(blurbMiddle);
                blurbsUnsorted.Add(blurbFirst);
                blurbsUnsorted.Add(blurbLast);

                //Add the blurbs to the comparison list in the proper sort order
                List<Blurb> blurbsSortedComparison = new List<Blurb>();
                blurbsSortedComparison.Add(blurbFirst);
                blurbsSortedComparison.Add(blurbMiddle);
                blurbsSortedComparison.Add(blurbLast);

                List<Blurb> sortedListLeastRecent = new List<Blurb>(); //set up empty lists to copy the sorted ones into

                //Act
                sortedListLeastRecent = DbManip.SortBlurbs(blurbsUnsorted.AsQueryable<Blurb>(), SortSetting.LeastRecent).ToList();

                //Assert
                Assert.Equal(blurbsSortedComparison, sortedListLeastRecent);
            }
        }



        [Fact]
        public void SortBlurbs_ScoreLH()
        {
            var options = new DbContextOptionsBuilder<BlurbDbContext>()
                .UseInMemoryDatabase(databaseName: "SortBlurbs_ScoreLH")
                .Options;

            using (var context = new BlurbDbContext(options))
            {
                //Arrange

                //STEP 1: SET UP THE IN-MEMORY DB
                Media media1 = new Media(Models.Models.Type.Movie, "");                  //Add the movies to the DB
                Media media2 = new Media(Models.Models.Type.Movie, "");
                Media media3 = new Media(Models.Models.Type.Movie, "");
                context.Add(media1);
                context.Add(media2);
                context.Add(media3);
                context.SaveChanges();

                Tag tag1 = new Tag("Horror");                                       //Add the tags for movie1 to the DB
                Tag tag2 = new Tag("Thriller");
                context.Add(tag1);
                context.Add(tag2);
                context.SaveChanges();

                MediaTag mediaTag1 = new MediaTag(tag1, media1);                    //Maps the two tags to the media1 item
                MediaTag mediaTag2 = new MediaTag(tag2, media1);
                MediaTag mediaTag3 = new MediaTag(tag1, media2);                    //Maps the two tags to the media2 item
                MediaTag mediaTag4 = new MediaTag(tag2, media2);
                MediaTag mediaTag5 = new MediaTag(tag1, media3);                    //Maps the two tags to the media3 item
                MediaTag mediaTag6 = new MediaTag(tag2, media3);
                context.Add(mediaTag1);
                context.Add(mediaTag2);
                context.Add(mediaTag3);
                context.Add(mediaTag4);
                context.Add(mediaTag5);
                context.Add(mediaTag6);
                context.SaveChanges();

                //Make sure that we get what's in the DB with the proper IDs and MediaTags lists
                media1 = context.Medias.Include(m => m.MediaTags).FirstOrDefault(m => m.Name == media1.Name);
                tag1 = context.Tags.Include(t => t.MediaTags).FirstOrDefault(t => t.Name == tag1.Name);
                tag2 = context.Tags.Include(t => t.MediaTags).FirstOrDefault(t => t.Name == tag2.Name);

                //Make an empty user to pass into the blurbs
                User user = new User();

                Blurb blurbFirst = new Blurb(user, 7, media1);
                Blurb blurbMiddle = new Blurb(user, 8, media2);
                Blurb blurbLast = new Blurb(user, 9, media3);

                //Add the blurbs to the list out of order
                List<Blurb> blurbsUnsorted = new List<Blurb>();
                blurbsUnsorted.Add(blurbMiddle);
                blurbsUnsorted.Add(blurbFirst);
                blurbsUnsorted.Add(blurbLast);

                //Add the blurbs to the comparison list in the proper sort order
                List<Blurb> blurbsSortedComparison = new List<Blurb>();
                blurbsSortedComparison.Add(blurbFirst);
                blurbsSortedComparison.Add(blurbMiddle);
                blurbsSortedComparison.Add(blurbLast);

                List<Blurb> sortedListScoreLH = new List<Blurb>(); //set up empty lists to copy the sorted ones into

                //Act
                sortedListScoreLH = DbManip.SortBlurbs(blurbsUnsorted.AsQueryable<Blurb>(), SortSetting.ScoreLH).ToList();

                //Assert
                Assert.Equal(blurbsSortedComparison, sortedListScoreLH);
            }
        }


        [Fact]
        public void SortBlurbs_ScoreHL()
        {
            var options = new DbContextOptionsBuilder<BlurbDbContext>()
                .UseInMemoryDatabase(databaseName: "SortBlurbs_ScoreHL")
                .Options;

            using (var context = new BlurbDbContext(options))
            {
                //Arrange

                //STEP 1: SET UP THE IN-MEMORY DB
                Media media1 = new Media(Models.Models.Type.Movie, "");             //Add the movies to the DB
                Media media2 = new Media(Models.Models.Type.Movie, "");
                Media media3 = new Media(Models.Models.Type.Movie, "");
                context.Add(media1);
                context.Add(media2);
                context.Add(media3);
                context.SaveChanges();

                Tag tag1 = new Tag("Horror");                                       //Add the tags for movie1 to the DB
                Tag tag2 = new Tag("Thriller");
                context.Add(tag1);
                context.Add(tag2);
                context.SaveChanges();

                MediaTag mediaTag1 = new MediaTag(tag1, media1);                    //Maps the two tags to the media1 item
                MediaTag mediaTag2 = new MediaTag(tag2, media1);
                MediaTag mediaTag3 = new MediaTag(tag1, media2);                    //Maps the two tags to the media2 item
                MediaTag mediaTag4 = new MediaTag(tag2, media2);
                MediaTag mediaTag5 = new MediaTag(tag1, media3);                    //Maps the two tags to the media3 item
                MediaTag mediaTag6 = new MediaTag(tag2, media3);
                context.Add(mediaTag1);
                context.Add(mediaTag2);
                context.Add(mediaTag3);
                context.Add(mediaTag4);
                context.Add(mediaTag5);
                context.Add(mediaTag6);
                context.SaveChanges();

                //Make sure that we get what's in the DB with the proper IDs and MediaTags lists
                media1 = context.Medias.Include(m => m.MediaTags).FirstOrDefault(m => m.Name == media1.Name);
                tag1 = context.Tags.Include(t => t.MediaTags).FirstOrDefault(t => t.Name == tag1.Name);
                tag2 = context.Tags.Include(t => t.MediaTags).FirstOrDefault(t => t.Name == tag2.Name);

                //Make an empty user to pass into the blurbs
                User user = new User();

                Blurb blurbFirst = new Blurb(user, 7, media1);
                Blurb blurbMiddle = new Blurb(user, 8, media2);
                Blurb blurbLast = new Blurb(user, 9, media3);

                //Add the blurbs to the list out of order
                List<Blurb> blurbsUnsorted = new List<Blurb>();
                blurbsUnsorted.Add(blurbMiddle);
                blurbsUnsorted.Add(blurbFirst);
                blurbsUnsorted.Add(blurbLast);

                //Add the blurbs to the comparison list in the proper sort order
                List<Blurb> blurbsSortedComparison = new List<Blurb>();
                blurbsSortedComparison.Add(blurbLast);
                blurbsSortedComparison.Add(blurbMiddle);
                blurbsSortedComparison.Add(blurbFirst);

                List<Blurb> sortedListScoreHL = new List<Blurb>(); //set up empty lists to copy the sorted ones into

                //Act
                sortedListScoreHL = DbManip.SortBlurbs(blurbsUnsorted.AsQueryable<Blurb>(), SortSetting.ScoreHL).ToList();

                //Assert
                Assert.Equal(blurbsSortedComparison, sortedListScoreHL);
            }
        }


        [Fact]
        public void FilterByType_BooksOnly()
        {
            var options = new DbContextOptionsBuilder<BlurbDbContext>()
                .UseInMemoryDatabase(databaseName: "Filter_BooksOnly")
                .Options;

            using (var context = new BlurbDbContext(options))
            {
                //Arrange

                //STEP 1: SET UP THE IN-MEMORY DB
                Media media1 = new Media(Models.Models.Type.Movie, "");             //Add the movies to the DB
                Media media2 = new Media(Models.Models.Type.Game, "");
                Media media3 = new Media(Models.Models.Type.Book, "");
                context.Add(media1);
                context.Add(media2);
                context.Add(media3);
                context.SaveChanges();

                Tag tag1 = new Tag("Horror");                                       //Add the tags for movie1 to the DB
                Tag tag2 = new Tag("Thriller");
                context.Add(tag1);
                context.Add(tag2);
                context.SaveChanges();

                //Make an empty user to pass into the blurbs
                User user = new User();

                Blurb movie = new Blurb(user, 7, media1) { BlurbId = 1 };
                Blurb game = new Blurb(user, 8, media2) { BlurbId = 2 };
                Blurb book = new Blurb(user, 9, media3) { BlurbId = 3 };

                //STEP2: ARRANGE EVERYTHING
                Dictionary<Models.Models.Type, bool> filterDict = new Dictionary<Models.Models.Type, bool>();
                filterDict.Add(Models.Models.Type.Book, true);
                filterDict.Add(Models.Models.Type.Movie, false);
                filterDict.Add(Models.Models.Type.Game, false);
                filterDict.Add(Models.Models.Type.TV, false);

                //Add the blurbs to the list
                List<Blurb> blurbsUnfiltered = new List<Blurb>();
                blurbsUnfiltered.Add(game);
                blurbsUnfiltered.Add(movie);
                blurbsUnfiltered.Add(book);

                //the proper list should only contain the book item since those are the filter settings
                List<Blurb> blurbsFilteredComparison = new List<Blurb>();
                blurbsFilteredComparison.Add(book);

                List<Blurb> blurbsFiltered = new List<Blurb>(); //set up empty lists to copy the sorted ones into

                //Act
                blurbsFiltered = DbManip.FilterByType(blurbsUnfiltered.AsQueryable<Blurb>(), filterDict).ToList();

                //Assert
                Assert.Equal(blurbsFilteredComparison, blurbsFiltered);
            }
        }
    }
}
