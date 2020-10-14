using Team1P2.Models.Models;
using Team1P2.Repo.Data;
using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Team1P2.Models.Models.Enums;
using System;
using Team1P2.Repo.Repository;

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
                blurb = await repo.EditBlurbPrivacyAsync(blurb.BlurbId, Privacy.Private );
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
    }
}
