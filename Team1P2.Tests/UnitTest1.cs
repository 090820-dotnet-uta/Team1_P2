using Team1P2.Models.Models;
using Team1P2.Repo.Data;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Team1P2.Models.Models.Enums;
using System;
using Team1P2.Repo.Repository;
using System.Xml.Linq;

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
                Repository repo = new Repository(context);

                //Arrange

                //STEP 1: SET UP THE IN-MEMORY DB
                Media media1 = new Media(Models.Models.Enums.Type.Movie, "Halloween");                  //Add the movies to the DB
                Media media2 = new Media(Models.Models.Enums.Type.Movie, "Scream");
                Media media3 = new Media(Models.Models.Enums.Type.Movie, "World War Z");
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
                sortedListAZ = repo.SortBlurbs(blurbsUnsorted.AsQueryable<Blurb>(), SortSetting.AZ).ToList();

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
                Repository repo = new Repository(context);

                //Arrange

                //STEP 1: SET UP THE IN-MEMORY DB
                Media media1 = new Media(Models.Models.Enums.Type.Movie, "Halloween");                  //Add the movies to the DB
                Media media2 = new Media(Models.Models.Enums.Type.Movie, "Scream");
                Media media3 = new Media(Models.Models.Enums.Type.Movie, "World War Z");
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
                sortedListZA = repo.SortBlurbs(blurbsUnsorted.AsQueryable<Blurb>(), SortSetting.ZA).ToList();

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
                Repository repo = new Repository(context);

                //Arrange

                //STEP 1: SET UP THE IN-MEMORY DB
                Media media1 = new Media(Models.Models.Enums.Type.Movie, "");                  //Add the movies to the DB
                Media media2 = new Media(Models.Models.Enums.Type.Movie, "");
                Media media3 = new Media(Models.Models.Enums.Type.Movie, "");
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
                sortedListMostRecent = repo.SortBlurbs(blurbsUnsorted.AsQueryable<Blurb>(), SortSetting.MostRecent).ToList();

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
                Repository repo = new Repository(context);

                //Arrange

                //STEP 1: SET UP THE IN-MEMORY DB
                Media media1 = new Media(Models.Models.Enums.Type.Movie, "");                  //Add the movies to the DB
                Media media2 = new Media(Models.Models.Enums.Type.Movie, "");
                Media media3 = new Media(Models.Models.Enums.Type.Movie, "");
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
                sortedListLeastRecent = repo.SortBlurbs(blurbsUnsorted.AsQueryable<Blurb>(), SortSetting.LeastRecent).ToList();

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
                Repository repo = new Repository(context);

                //Arrange

                //STEP 1: SET UP THE IN-MEMORY DB
                Media media1 = new Media(Models.Models.Enums.Type.Movie, "");                  //Add the movies to the DB
                Media media2 = new Media(Models.Models.Enums.Type.Movie, "");
                Media media3 = new Media(Models.Models.Enums.Type.Movie, "");
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
                sortedListScoreLH = repo.SortBlurbs(blurbsUnsorted.AsQueryable<Blurb>(), SortSetting.ScoreLH).ToList();

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
                Repository repo = new Repository(context);

                //Arrange

                //STEP 1: SET UP THE IN-MEMORY DB
                Media media1 = new Media(Models.Models.Enums.Type.Movie, "");             //Add the movies to the DB
                Media media2 = new Media(Models.Models.Enums.Type.Movie, "");
                Media media3 = new Media(Models.Models.Enums.Type.Movie, "");
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
                sortedListScoreHL = repo.SortBlurbs(blurbsUnsorted.AsQueryable<Blurb>(), SortSetting.ScoreHL).ToList();

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
                Repository repo = new Repository(context);

                //Arrange

                //STEP 1: SET UP THE IN-MEMORY DB
                Media media1 = new Media(Models.Models.Enums.Type.Movie, "");             //Add the movies to the DB
                Media media2 = new Media(Models.Models.Enums.Type.Game, "");
                Media media3 = new Media(Models.Models.Enums.Type.Book, "");
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
                Dictionary<Models.Models.Enums.Type, bool> filterDict = new Dictionary<Models.Models.Enums.Type, bool>();
                filterDict.Add(Models.Models.Enums.Type.Book, true);
                filterDict.Add(Models.Models.Enums.Type.Movie, false);
                filterDict.Add(Models.Models.Enums.Type.Game, false);
                filterDict.Add(Models.Models.Enums.Type.TV, false);

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
                blurbsFiltered = repo.FilterByType(blurbsUnfiltered.AsQueryable<Blurb>(), filterDict).ToList();

                //Assert
                Assert.Equal(blurbsFilteredComparison, blurbsFiltered);
            }
        }



        [Fact]
        public async void AddUserToDb_NewUser()
        {
            var options = new DbContextOptionsBuilder<BlurbDbContext>()
                .UseInMemoryDatabase(databaseName: "AddUser")
                .Options;

            using (var context = new BlurbDbContext(options))
            {
                Repository repo = new Repository(context);

                //Arrange
                User user = new User("skywalker13", "password13");

                //Act
                user = await repo.AddUserToDbAsync(user);

                //Assert
                Assert.True(context.Users.Contains(user));
            }
        }


        [Fact]
        public async void AddMediaToDb_NewMedia()
        {
            var options = new DbContextOptionsBuilder<BlurbDbContext>()
                .UseInMemoryDatabase(databaseName: "AddMedia")
                .Options;

            using (var context = new BlurbDbContext(options))
            {
                Repository repo = new Repository(context);

                //Arrange
                Media media = new Media(Models.Models.Enums.Type.Book, "The Way of Kings");

                //Act
                media = await repo.AddMediaToDbAsync(media);

                //Assert
                Assert.True(context.Medias.Contains(media));
            }
        }


        [Fact]
        public async void EditUsername_GoodInput()
        {
            var options = new DbContextOptionsBuilder<BlurbDbContext>()
                .UseInMemoryDatabase(databaseName: "EditUsername")
                .Options;

            using (var context = new BlurbDbContext(options))
            {
                Repository repo = new Repository(context);

                //Arrange
                User user = new User("skywalker13", "password13");
                user = await repo.AddUserToDbAsync(user);

                //Act
                user = await repo.EditUsernameAsync(user.UserId, "crono13");

                //Assert
                Assert.Equal("crono13", user.Username);
            }
        }


        [Fact]
        public async void EditScreenName_GoodInput()
        {
            var options = new DbContextOptionsBuilder<BlurbDbContext>()
                .UseInMemoryDatabase(databaseName: "EditScreenName")
                .Options;

            using (var context = new BlurbDbContext(options))
            {
                Repository repo = new Repository(context);

                //Arrange
                User user = new User("skywalker13", "password13");
                user = await repo.AddUserToDbAsync(user);

                //Act
                user = await repo.EditScreenNameAsync(user.UserId, "crono13");

                //Assert
                Assert.Equal("crono13", user.ScreenName);
            }
        }


        [Fact]
        public async void EditName_GoodInput()
        {
            var options = new DbContextOptionsBuilder<BlurbDbContext>()
                .UseInMemoryDatabase(databaseName: "EditName")
                .Options;

            using (var context = new BlurbDbContext(options))
            {
                Repository repo = new Repository(context);

                //Arrange
                User user = new User("skywalker13", "password13");
                user = await repo.AddUserToDbAsync(user);

                //Act
                user = await repo.EditNameAsync(user.UserId, "bob");

                //Assert
                Assert.Equal("bob", user.Name);
            }
        }



        [Fact]
        public async void EditPassword_GoodInput()
        {
            var options = new DbContextOptionsBuilder<BlurbDbContext>()
                .UseInMemoryDatabase(databaseName: "EditPassword")
                .Options;

            using (var context = new BlurbDbContext(options))
            {
                Repository repo = new Repository(context);

                //Arrange
                User user = new User("skywalker13", "password13");
                user = await repo.AddUserToDbAsync(user);

                //Act
                user = await repo.EditPasswordAsync(user.UserId, "strongPassword123");

                //Assert
                Assert.Equal("strongPassword123", user.Password);
            }
        }


        [Fact]
        public async void AddBlurb_GoodInput()
        {
            var options = new DbContextOptionsBuilder<BlurbDbContext>()
                .UseInMemoryDatabase(databaseName: "AddBlurb")
                .Options;

            using (var context = new BlurbDbContext(options))
            {
                Repository repo = new Repository(context);

                //Arrange
                User user = new User("skywalker13", "password13");
                Media media = new Media();
                Blurb blurb = new Blurb(user, 7.5, media);

                //Act
                blurb = await repo.AddBlurbToDbAsync(blurb);

                //Assert
                Assert.True(context.Blurbs.Contains(blurb));
            }
        }


        [Fact]
        public async void AddNote_GoodInput()
        {
            var options = new DbContextOptionsBuilder<BlurbDbContext>()
                .UseInMemoryDatabase(databaseName: "AddBlurb")
                .Options;

            using (var context = new BlurbDbContext(options))
            {
                Repository repo = new Repository(context);

                //Arrange
                User user = new User("skywalker13", "password13");
                Media media = new Media();
                Blurb blurb = new Blurb(user, 7.5, media);
                blurb = await repo.AddBlurbToDbAsync(blurb);
                Assert.True(context.Blurbs.Contains(blurb));

                Note note = await repo.CreateEmptyNoteAsync(blurb.BlurbId);

                //Act
                note = await repo.AddNoteToDbAsync(note);

                //Assert
                Assert.True(context.Notes.Contains(note)); //Make sure the context now has the added note
            }
        }


        [Fact]
        public async void DeleteBlurb_NoNotes()
        {
            var options = new DbContextOptionsBuilder<BlurbDbContext>()
                .UseInMemoryDatabase(databaseName: "DeleteNoNotes")
                .Options;

            using (var context = new BlurbDbContext(options))
            {
                Repository repo = new Repository(context);

                //Arrange
                User user = new User("skywalker13", "password13");
                Media media = new Media();
                Blurb blurb = new Blurb(user, 7.5, media);
                blurb = await repo.AddBlurbToDbAsync(blurb);                          //Add a new blurb to the db
                Assert.True(context.Blurbs.Contains(blurb));                            //Make sure we added a blurb to the db
                int blurbId = context.Blurbs.FirstOrDefault(b => b == blurb).BlurbId;   //Get the blurbId of the one we just added
                Assert.Equal(1, blurbId);                                               //Make sure we have the correct blurbId

                //Act
                bool deletedSuccessfully = repo.DeleteBlurb(blurbId);

                //Assert
                Assert.False(context.Blurbs.Contains(blurb));
            }
        }


        [Fact]
        public async void DeleteBlurb_WithNotes()
        {
            var options = new DbContextOptionsBuilder<BlurbDbContext>()
                .UseInMemoryDatabase(databaseName: "WithNoNotes")
                .Options;

            using (var context = new BlurbDbContext(options))
            {
                Repository repo = new Repository(context);

                //Arrange
                User user = new User("skywalker13", "password13");
                Media media = new Media();
                Blurb blurb = new Blurb(user, 7.5, media);
                blurb = await repo.AddBlurbToDbAsync(blurb);

                Note note = await repo.CreateNoteAsync(blurb.BlurbId, "This is a blurb");
                note = await repo.AddNoteToDbAsync(note);

                //Make sure that the note and the blurb are there before deletion
                Assert.True(context.Blurbs.Contains(blurb));
                Assert.True(context.Notes.Contains(note));

                //Act
                bool successfulDelete = repo.DeleteBlurb(blurb.BlurbId);

                //Assert
                Assert.False(context.Blurbs.Contains(blurb));
                Assert.False(context.Notes.Contains(note));
            }
        }


        [Fact]
        public async void EditBlurbScore_GoodInput()
        {
            var options = new DbContextOptionsBuilder<BlurbDbContext>()
                .UseInMemoryDatabase(databaseName: "EditBlurbScore")
                .Options;

            using (var context = new BlurbDbContext(options))
            {
                Repository repo = new Repository(context);

                //Arrange
                User user = new User("skywalker13", "password13");
                Media media = new Media();
                Blurb blurb = new Blurb(user, 7.5, media);
                blurb = await repo.AddBlurbToDbAsync(blurb);

                //Act
                blurb = await repo.EditBlurbScoreAsync(blurb.BlurbId, 5.6);
                var resultBlurb = context.Blurbs.FirstOrDefault(x => x.BlurbId == blurb.BlurbId);

                //Assert
                Assert.Equal(5.6, resultBlurb.Score);
            }
        }


        [Fact]
        public async void EditBlurbPrivacy_GoodInput()
        {
            var options = new DbContextOptionsBuilder<BlurbDbContext>()
                .UseInMemoryDatabase(databaseName: "EditBlurbPRivacy")
                .Options;

            using (var context = new BlurbDbContext(options))
            {
                Repository repo = new Repository(context);

                //Arrange
                User user = new User("skywalker13", "password13");
                Media media = new Media();
                Blurb blurb = new Blurb(user, 7.5, media) { Privacy = Privacy.Public };
                blurb = await repo.AddBlurbToDbAsync(blurb);

                //Act
                blurb = await repo.EditBlurbPrivacyAsync(blurb.BlurbId, (int)Privacy.Private );
                var resultBlurb = context.Blurbs.FirstOrDefault(x => x.BlurbId == blurb.BlurbId);

                //Assert
                Assert.Equal(Privacy.Private, resultBlurb.Privacy);
            }
        }


        [Fact]
        public async void EditBlurbMessage_GoodInput()
        {
            var options = new DbContextOptionsBuilder<BlurbDbContext>()
                .UseInMemoryDatabase(databaseName: "EditBlurbMessage")
                .Options;

            using (var context = new BlurbDbContext(options))
            {
                Repository repo = new Repository(context);

                //Arrange
                User user = new User("skywalker13", "password13");
                Media media = new Media();
                Blurb blurb = new Blurb(user, 7.5, media) { Privacy = Privacy.Public };
                blurb = await repo.AddBlurbToDbAsync(blurb); //Add a new blurb to the db

                //Act
                blurb = await repo.EditBlurbMessageAsync(blurb.BlurbId, "This is a message");
                var resultBlurb = context.Blurbs.FirstOrDefault(x => x.BlurbId == blurb.BlurbId);

                //Assert
                Assert.Equal("This is a message", resultBlurb.Message);
            }
        }


        [Fact]
        public async void GetUser_GoodInput()
        {
            var options = new DbContextOptionsBuilder<BlurbDbContext>()
                .UseInMemoryDatabase(databaseName: "GetUser")
                .Options;

            using (var context = new BlurbDbContext(options))
            {
                Repository repo = new Repository(context);

                //Arrange
                User user = new User("skywalker13", "password13");
                user = await repo.AddUserToDbAsync(user);

                //Act
                var user2 = await repo.GetUserAsync(user.UserId);

                //Assert
                Assert.Equal(user, user2);
            }
        }


        [Fact]
        public async void GetAllUsers_2Users()
        {
            var options = new DbContextOptionsBuilder<BlurbDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllUsers_2Users")
                .Options;

            using (var context = new BlurbDbContext(options))
            {
                Repository repo = new Repository(context);

                //Arrange
                User user = new User("skywalker13", "password13");
                user = await repo.AddUserToDbAsync(user);
                User user2 = new User("skywalker14", "password14");
                user2 = await repo.AddUserToDbAsync(user2);
                List<User> users = new List<User>();
                users.Add(user);
                users.Add(user2);

                //Act
                var users2 = await repo.GetAllUsersAsync();

                //Assert
                Assert.Equal(users, users2);
            }
        }


        [Fact]
        public async void GetBlurb_GoodInput()
        {
            var options = new DbContextOptionsBuilder<BlurbDbContext>()
                .UseInMemoryDatabase(databaseName: "GetBlurb")
                .Options;

            using (var context = new BlurbDbContext(options))
            {
                Repository repo = new Repository(context);

                //Arrange
                User user = new User("skywalker13", "password13");
                Media media = new Media();
                Blurb blurb = new Blurb(user, 7.5, media) { Privacy = Privacy.Public };
                blurb = await repo.AddBlurbToDbAsync(blurb); //Add a new blurb to the db

                //Act
                var blurb2 = await repo.GetBlurbAsync(blurb.BlurbId);

                //Assert
                Assert.Equal(blurb, blurb2);
            }
        }


        [Fact]
        public async void GetAllBlurbs_2Blurbs()
        {
            var options = new DbContextOptionsBuilder<BlurbDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllBlurbs")
                .Options;

            using (var context = new BlurbDbContext(options))
            {
                Repository repo = new Repository(context);

                //Arrange
                User user = new User("skywalker13", "password13");
                Media media1 = new Media();
                Media media2 = new Media();
                Blurb blurb1 = new Blurb(user, 7.5, media1) { Privacy = Privacy.Public };
                blurb1 = await repo.AddBlurbToDbAsync(blurb1); //Add a new blurb to the db
                Blurb blurb2 = new Blurb(user, 9, media2) { Privacy = Privacy.Public };
                blurb2 = await repo.AddBlurbToDbAsync(blurb2); //Add a new blurb to the db
                List<Blurb> blurbs = new List<Blurb>();
                blurbs.Add(blurb1);
                blurbs.Add(blurb2);
                
                //Act
                var blurbs2 = await repo.GetAllBlurbsAsync();

                //Assert
                Assert.Equal(blurbs, blurbs2);
            }
        }


        [Fact]
        public async void GetBlurbsByUser_2Blurbs()
        {
            var options = new DbContextOptionsBuilder<BlurbDbContext>()
                .UseInMemoryDatabase(databaseName: "GetBlurbs123")
                .Options;

            using (var context = new BlurbDbContext(options))
            {
                Repository repo = new Repository(context);

                //Arrange
                User user = new User("skywalker13", "password13");
                user = await repo.AddUserToDbAsync(user);

                User user2 = new User("jim", "password13");
                user2 = await repo.AddUserToDbAsync(user2);

                Media media1 = new Media();
                Media media2 = new Media();
                Blurb blurb1 = new Blurb(user, 7.5, media1) { Privacy = Privacy.Public };
                blurb1 = await repo.AddBlurbToDbAsync(blurb1); //Add a new blurb to the db
                Blurb blurb2 = new Blurb(user, 9, media2) { Privacy = Privacy.Public };
                blurb2 = await repo.AddBlurbToDbAsync(blurb2); //Add a new blurb to the db
                Blurb blurb3 = new Blurb(user2, 9, media2) { Privacy = Privacy.Public };
                blurb3 = await repo.AddBlurbToDbAsync(blurb3); //Add a new blurb to the db with a second user


                List<Blurb> blurbs = new List<Blurb>();
                blurbs.Add(blurb1);
                blurbs.Add(blurb2);

                //Act
                var blurbs2 = await repo.GetBlurbsByUserIdAsync(user.UserId);

                //Assert
                Assert.Equal(blurbs, blurbs2);
            }
        }


        [Fact]
        public async void GetNote_GoodInput()
        {
            var options = new DbContextOptionsBuilder<BlurbDbContext>()
                .UseInMemoryDatabase(databaseName: "GetNote")
                .Options;

            using (var context = new BlurbDbContext(options))
            {
                Repository repo = new Repository(context);

                //Arrange
                User user = new User("skywalker13", "password13");
                Media media = new Media();
                Blurb blurb = new Blurb(user, 7.5, media) { Privacy = Privacy.Public };
                blurb = await repo.AddBlurbToDbAsync(blurb); //Add a new blurb to the db
                Note note = await repo.CreateEmptyNoteAsync(blurb.BlurbId);
                note = await repo.AddNoteToDbAsync(note);

                //Act
                var note2 = await repo.GetNoteAsync(note.NoteId);

                //Assert
                Assert.Equal(note, note2);
            }
        }


        [Fact]
        public async void GetMedia_GoodInput()
        {
            var options = new DbContextOptionsBuilder<BlurbDbContext>()
                .UseInMemoryDatabase(databaseName: "GetMedia")
                .Options;

            using (var context = new BlurbDbContext(options))
            {
                Repository repo = new Repository(context);

                //Arrange
                Media media = new Media(Models.Models.Enums.Type.Book, "The Way of Kings");
                media = await repo.AddMediaToDbAsync(media);

                //Act
                var media2 = await repo.GetMediaAsync(media.MediaId);

                //Assert
                Assert.Equal(media, media2);
            }
        }


        [Fact]
        public async void GetAllMedias_2Medias()
        {
            var options = new DbContextOptionsBuilder<BlurbDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllMedias")
                .Options;

            using (var context = new BlurbDbContext(options))
            {
                Repository repo = new Repository(context);

                //Arrange
                Media media1 = new Media(Models.Models.Enums.Type.Book, "Mistborn");
                Media media2 = new Media(Models.Models.Enums.Type.Movie, "Inception");
                media1 = await repo.AddMediaToDbAsync(media1);
                media2 = await repo.AddMediaToDbAsync(media2);

                List<Media> mediaList = new List<Media>();
                mediaList.Add(media1);
                mediaList.Add(media2);

                //Act
                var mediaList2 = await repo.GetAllMediaAsync();

                //Assert
                Assert.Equal(mediaList, mediaList2);
            }
        }


        [Fact]
        public async void GetTag_GoodInput()
        {
            var options = new DbContextOptionsBuilder<BlurbDbContext>()
                .UseInMemoryDatabase(databaseName: "GetTag")
                .Options;

            using (var context = new BlurbDbContext(options))
            {
                Repository repo = new Repository(context);

                //Arrange
                Tag tag1 = new Tag("Horror");
                Tag tag2 = new Tag("Fantasy");
                tag1 = await repo.AddTagToDbAsync(tag1);
                tag2 = await repo.AddTagToDbAsync(tag2);

                //Act
                var tagTest = await repo.GetTagAsync(tag1.TagId);

                //Assert
                Assert.Equal(tag1, tagTest);
            }
        }


        [Fact]
        public async void GetAllTags_2Tags()
        {
            var options = new DbContextOptionsBuilder<BlurbDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllTags")
                .Options;

            using (var context = new BlurbDbContext(options))
            {
                Repository repo = new Repository(context);

                //Arrange
                Tag tag1 = new Tag("Horror");
                Tag tag2 = new Tag("Fantasy");
                tag1 = await repo.AddTagToDbAsync(tag1);
                tag2 = await repo.AddTagToDbAsync(tag2);

                List<Tag> tagList = new List<Tag>();
                tagList.Add(tag1);
                tagList.Add(tag2);

                //Act
                var tagList2 = await repo.GetAllTagsAsync();

                //Assert
                Assert.Equal(tagList, tagList2);
            }
        }


        [Fact]
        public async void GetMediaTag_GoodInput()
        {
            var options = new DbContextOptionsBuilder<BlurbDbContext>()
                .UseInMemoryDatabase(databaseName: "GetMediaTag")
                .Options;

            using (var context = new BlurbDbContext(options))
            {
                Repository repo = new Repository(context);

                //Arrange
                Media media1 = new Media(Models.Models.Enums.Type.Book, "Mistborn");
                Media media2 = new Media(Models.Models.Enums.Type.Movie, "Inception");
                media1 = await repo.AddMediaToDbAsync(media1);
                media2 = await repo.AddMediaToDbAsync(media2);

                Tag tag1 = new Tag("Horror");
                Tag tag2 = new Tag("Fantasy");
                tag1 = await repo.AddTagToDbAsync(tag1);
                tag2 = await repo.AddTagToDbAsync(tag2);

                MediaTag mediaTag1 = new MediaTag(tag1, media1);
                MediaTag mediaTag2 = new MediaTag(tag2, media1);
                MediaTag mediaTag3 = new MediaTag(tag1, media2);
                mediaTag1 = await repo.AddMediaTagToDbAsync(mediaTag1);
                mediaTag2 = await repo.AddMediaTagToDbAsync(mediaTag2);
                mediaTag3 = await repo.AddMediaTagToDbAsync(mediaTag3);

                List<MediaTag> mediaTagList = new List<MediaTag>();
                mediaTagList.Add(mediaTag1);
                mediaTagList.Add(mediaTag2);
                mediaTagList.Add(mediaTag3);

                //Act
                var mediaTagTest = await repo.GetMediaTagAsync(mediaTag1.MediaTagId);

                //Assert
                Assert.Equal(mediaTag1, mediaTagTest);
            }
        }


        [Fact]
        public async void GetAllMediaTags_GoodInput()
        {
            var options = new DbContextOptionsBuilder<BlurbDbContext>()
                .UseInMemoryDatabase(databaseName: "GetAllMediaTags")
                .Options;

            using (var context = new BlurbDbContext(options))
            {
                Repository repo = new Repository(context);

                //Arrange
                Media media1 = new Media(Models.Models.Enums.Type.Book, "Mistborn");
                Media media2 = new Media(Models.Models.Enums.Type.Movie, "Inception");
                media1 = await repo.AddMediaToDbAsync(media1);
                media2 = await repo.AddMediaToDbAsync(media2);

                Tag tag1 = new Tag("Horror");
                Tag tag2 = new Tag("Fantasy");
                tag1 = await repo.AddTagToDbAsync(tag1);
                tag2 = await repo.AddTagToDbAsync(tag2);

                MediaTag mediaTag1 = new MediaTag(tag1, media1);
                MediaTag mediaTag2 = new MediaTag(tag2, media1);
                MediaTag mediaTag3 = new MediaTag(tag1, media2);
                mediaTag1 = await repo.AddMediaTagToDbAsync(mediaTag1);
                mediaTag2 = await repo.AddMediaTagToDbAsync(mediaTag2);
                mediaTag3 = await repo.AddMediaTagToDbAsync(mediaTag3);

                List<MediaTag> mediaTagList = new List<MediaTag>();
                mediaTagList.Add(mediaTag1);
                mediaTagList.Add(mediaTag2);
                mediaTagList.Add(mediaTag3);

                //Act
                var mediaTagListTest = await repo.GetAllMediaTagsAsync();

                //Assert
                Assert.Equal(mediaTagList, mediaTagListTest);
            }
        }


        [Fact]
        public async void GetTagsByMediaId_GoodInput()
        {
            var options = new DbContextOptionsBuilder<BlurbDbContext>()
                .UseInMemoryDatabase(databaseName: "GetTagsByMediaId")
                .Options;

            using (var context = new BlurbDbContext(options))
            {
                Repository repo = new Repository(context);

                //Arrange
                Media media1 = new Media(Models.Models.Enums.Type.Book, "Mistborn");
                Media media2 = new Media(Models.Models.Enums.Type.Movie, "Inception");
                media1 = await repo.AddMediaToDbAsync(media1);
                media2 = await repo.AddMediaToDbAsync(media2);

                Tag tag1 = new Tag("Horror");
                Tag tag2 = new Tag("Fantasy");
                tag1 = await repo.AddTagToDbAsync(tag1);
                tag2 = await repo.AddTagToDbAsync(tag2);

                MediaTag mediaTag1 = new MediaTag(tag1, media1);
                MediaTag mediaTag2 = new MediaTag(tag2, media1);
                MediaTag mediaTag3 = new MediaTag(tag1, media2);
                mediaTag1 = await repo.AddMediaTagToDbAsync(mediaTag1);
                mediaTag2 = await repo.AddMediaTagToDbAsync(mediaTag2);
                mediaTag3 = await repo.AddMediaTagToDbAsync(mediaTag3);

                List<Media> mediaList = new List<Media>();
                mediaList.Add(media1);

                //Act
                var mediaTagListTest = await repo.GetMediaByTagId(tag2.TagId); //Get the media with tag2 in their list (just media1)

                //Assert
                Assert.Equal(mediaList, mediaTagListTest);
            }
        }


        [Fact]
        public async void GetMediaByTagId_GoodInput()
        {
            var options = new DbContextOptionsBuilder<BlurbDbContext>()
                .UseInMemoryDatabase(databaseName: "GetMediaByTagId")
                .Options;

            using (var context = new BlurbDbContext(options))
            {
                Repository repo = new Repository(context);

                //Arrange
                Media media1 = new Media(Models.Models.Enums.Type.Book, "Mistborn");
                Media media2 = new Media(Models.Models.Enums.Type.Movie, "Inception");
                media1 = await repo.AddMediaToDbAsync(media1);
                media2 = await repo.AddMediaToDbAsync(media2);

                Tag tag1 = new Tag("Horror");
                Tag tag2 = new Tag("Fantasy");
                tag1 = await repo.AddTagToDbAsync(tag1);
                tag2 = await repo.AddTagToDbAsync(tag2);

                MediaTag mediaTag1 = new MediaTag(tag1, media1);
                MediaTag mediaTag2 = new MediaTag(tag2, media1);
                MediaTag mediaTag3 = new MediaTag(tag1, media2);
                mediaTag1 = await repo.AddMediaTagToDbAsync(mediaTag1);
                mediaTag2 = await repo.AddMediaTagToDbAsync(mediaTag2);
                mediaTag3 = await repo.AddMediaTagToDbAsync(mediaTag3);

                List<Tag> tagList = new List<Tag>();
                tagList.Add(tag1);

                //Act
                var tagListTest = await repo.GetTagsByMediaId(media2.MediaId); //Get the tags with just media2 in their list (just tag1)

                //Assert
                Assert.Equal(tagList, tagListTest);
            }
        }


        [Fact]
        public async void FullQuery_AllSettings()
        {
            var options = new DbContextOptionsBuilder<BlurbDbContext>()
                .UseInMemoryDatabase(databaseName: "FullQuery_AllSettings")
                .Options;

            using (var context = new BlurbDbContext(options))
            {
                Repository repo = new Repository(context);

                //Arrange
                Dictionary<Models.Models.Enums.Type, bool> filterSettings = new Dictionary<Models.Models.Enums.Type, bool>();
                filterSettings.Add(Models.Models.Enums.Type.Movie, true);
                filterSettings.Add(Models.Models.Enums.Type.Game, false);
                filterSettings.Add(Models.Models.Enums.Type.Book, true);
                filterSettings.Add(Models.Models.Enums.Type.TV, true);
                SortFilterSetting querySettings = new SortFilterSetting(SortSetting.ScoreHL, filterSettings, false, true, true);

                if (!repo.IsSeeded()) {
                    repo.SeedDb();
                }

                User curUser = await context.Users.FirstOrDefaultAsync();

                List<Blurb> queriedList = context.Blurbs.ToList();
                queriedList = repo.FilterByCanSee(queriedList.AsQueryable<Blurb>(), curUser).ToList();
                queriedList = repo.FilterByType(queriedList.AsQueryable<Blurb>(), filterSettings).ToList();
                queriedList = repo.FilterByUser(queriedList.AsQueryable<Blurb>(), curUser, querySettings.IncludeSelf, querySettings.IncludeFollowering, querySettings.IncludeUnfollowed).ToList();
                queriedList = repo.SortBlurbs(queriedList.AsQueryable<Blurb>(), querySettings.SortSetting).ToList();

                //Act
                List<Blurb> queriedBlurbsTest = await repo.FullQuery(curUser, querySettings);

                //Assert
                Assert.Equal(queriedList, queriedBlurbsTest);
            }
        }


        //[Fact]
        //public async void FullQuery_Pagination()
        //{
        //    var options = new DbContextOptionsBuilder<BlurbDbContext>()
        //        .UseInMemoryDatabase(databaseName: "FullQuery_Pagination")
        //        .Options;

        //    using (var context = new BlurbDbContext(options))
        //    {
        //        Repository repo = new Repository(context);

        //        //Arrange
        //        Dictionary<Models.Models.Enums.Type, bool> filterSettings = new Dictionary<Models.Models.Enums.Type, bool>();
        //        filterSettings.Add(Models.Models.Enums.Type.Movie, true);
        //        filterSettings.Add(Models.Models.Enums.Type.Game, true);
        //        filterSettings.Add(Models.Models.Enums.Type.Book, true);
        //        filterSettings.Add(Models.Models.Enums.Type.TV, true);
        //        SortFilterSetting querySettings = new SortFilterSetting(SortSetting.MostRecent, filterSettings, true, true, true);

        //        if (!repo.IsSeeded())
        //        {
        //            repo.SeedDb();
        //        }

        //        User curUser = await context.Users.FirstOrDefaultAsync();

        //        List<Blurb> queriedList = context.Blurbs.ToList();
        //        queriedList = repo.FilterByCanSee(queriedList.AsQueryable<Blurb>(), curUser).ToList();
        //        queriedList = repo.FilterByType(queriedList.AsQueryable<Blurb>(), filterSettings).ToList();
        //        queriedList = repo.FilterByUser(queriedList.AsQueryable<Blurb>(), curUser, querySettings.IncludeSelf, querySettings.IncludeFollowering, querySettings.IncludeUnfollowed).ToList();
        //        queriedList = repo.SortBlurbs(queriedList.AsQueryable<Blurb>(), querySettings.SortSetting).ToList();

        //        //Act
        //        List<Blurb> queriedBlurbsTest = await repo.FullQuery(curUser, querySettings, 5);

        //        //Assert
        //        Assert.Equal(queriedList.Skip(5), queriedBlurbsTest);
        //    }
        //}
    }
}
