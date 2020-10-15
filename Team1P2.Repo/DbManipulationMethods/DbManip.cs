using System.Collections.Generic;
using System.Linq;
using Team1P2.Models.Models;
using Team1P2.Models.Models.Enums;
using Team1P2.Repo.Data;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace Team1P2.Repo.DbManipulationMethods
{
  public static class DbManip
  {
        public static User GetUser(BlurbDbContext context, int userId)
        {
            return context.Users.FirstOrDefault(u => u.UserId == userId);
        }


        public static List<User> GetAllUsers(BlurbDbContext context)
        {
            return context.Users.ToList();
        }


        public static Blurb GetBlurb(BlurbDbContext context, int blurbId)
        {
            return context.Blurbs.FirstOrDefault(b => b.BlurbId == blurbId);
        }


        public static List<Blurb> GetAllBlurbs(BlurbDbContext context)
        {
            return context.Blurbs.ToList();
        }


        public static List<Blurb> GetBlurbsByUserId(BlurbDbContext context, int userId)
        {
            return context.Blurbs.Where(b => b.UserId == userId).ToList();
        }


        public static List<Note> GetNotesByBlurbId(BlurbDbContext context, int blurbId)
        {
            return context.Notes.Where(n => n.BlurbId == blurbId).ToList();
        }
        

        public static Media GetMedia(BlurbDbContext context, int mediaId)
        {
            return context.Medias.FirstOrDefault(m => m.MediaId == mediaId);
        }


        public static List<Media> GetAllMedia(BlurbDbContext context)
        {
            return context.Medias.ToList();
        }


        public static Tag GetTag(BlurbDbContext context, int tagId)
        {
            return context.Tags.FirstOrDefault(t => t.TagId == tagId);
        }


        //public static Tag GetTagsByMediaId(BlurbDbContext context, int mediaId)
        //{
            
        //}


        /// <summary>
        /// Adds the user to the db and saves changes
        /// </summary>
        /// <param name="context"></param>
        /// <param name="user"></param>
        public static User AddUserToDb(BlurbDbContext context, User user)
        {
            context.Add(user);
            context.SaveChanges();
            return context.Users.FirstOrDefault(u => u == user);
        }



        /// <summary>
        /// Updates a user's username and saves changes
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userId"></param>
        /// <param name="username"></param>
        public static User EditUsername(BlurbDbContext context, int userId, string username)
        {
            var user = context.Users.FirstOrDefault(x => x.UserId == userId);
            user.Username = username;
            context.Update(user);
            context.SaveChanges();
            return context.Users.FirstOrDefault(u => u == user);
        }


        /// <summary>
        ///  Updates a user's screen name and saves changes
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userId"></param>
        /// <param name="screenName"></param>
        public static User EditScreenName(BlurbDbContext context, int userId, string screenName)
        {
            var user = context.Users.FirstOrDefault(x => x.UserId == userId);
            user.ScreenName = screenName;
            context.Update(user);
            context.SaveChanges();
            return context.Users.FirstOrDefault(u => u == user);
        }


        /// <summary>
        /// Updates a user's name and saves changes
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        public static User EditName(BlurbDbContext context, int userId, string name)
        {
            var user = context.Users.FirstOrDefault(x => x.UserId == userId);
            user.Name = name;
            context.Update(user);
            context.SaveChanges();
            return context.Users.FirstOrDefault(u => u == user);
        }


        /// <summary>
        /// Updates a user's password and saves changes. Returns the edited user.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        public static User EditPassword(BlurbDbContext context, int userId, string password)
        {
            var user = context.Users.FirstOrDefault(x => x.UserId == userId);
            user.Password = password;
            context.Update(user);
            context.SaveChanges();
            return context.Users.FirstOrDefault(u => u == user);
        }


        /// <summary>
        /// Adds the blurb to the db and saves changes
        /// </summary>
        /// <param name="context"></param>
        /// <param name="blurb"></param>
        public static Blurb AddBlurbToDb(BlurbDbContext context, Blurb blurb)
        {
            context.Add(blurb);
            context.SaveChanges();
            return context.Blurbs.FirstOrDefault(b => b == blurb);
        }


        /// <summary>
        /// Deletes a blurb from the db as well as all notes referencing it. Returns true upon success.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="blurbId"></param>
        public static bool DeleteBlurb(BlurbDbContext context, int blurbId)
        {
            try
            {
                context.Blurbs.Remove(context.Blurbs.FirstOrDefault(b => b.BlurbId == blurbId)); //Remove the actual blurb
                context.Notes.RemoveRange(context.Notes.Where(n => n.BlurbId == blurbId));       //Remove all notes that reference it
                context.SaveChanges();
                return true;
            }
            catch (InvalidOperationException e)
            {
                return false;
            }
        }


        /// <summary>
        /// Edits the score for a given blurb and saves to db. Returns the edited blurb.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="blurbId"></param>
        /// <param name="newScore"></param>
        public static Blurb EditBlurbScore(BlurbDbContext context, int blurbId, double newScore)
        {
            var blurb = context.Blurbs.FirstOrDefault(x => x.BlurbId == blurbId);
            blurb.Score = newScore;
            context.Update(blurb);
            context.SaveChanges();
            return context.Blurbs.FirstOrDefault(b => b == blurb);
        }


        /// <summary>
        /// Edits the privacy setting for a given blurb and saves to db. Returns the edited blurb.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="blurbId"></param>
        /// <param name="privacy"></param>
        public static Blurb EditBlurbPrivacy(BlurbDbContext context, int blurbId, Privacy privacy)
        {
            var blurb = context.Blurbs.FirstOrDefault(x => x.BlurbId == blurbId);
            blurb.Privacy = privacy;
            context.Update(blurb);
            context.SaveChanges();
            return context.Blurbs.FirstOrDefault(b => b == blurb);
        }


        /// <summary>
        /// Edits the blurb message for a given blurb and saves to db. Returns the edited blurb.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="blurbId"></param>
        /// <param name="message"></param>
        public static Blurb EditBlurbMessage(BlurbDbContext context, int blurbId, string message)
        {
            var blurb = context.Blurbs.FirstOrDefault(x => x.BlurbId == blurbId);
            blurb.Message = message;
            context.Update(blurb);
            context.SaveChanges();
            return context.Blurbs.FirstOrDefault(b => b == blurb);
        }

        /// <summary>
        /// Creates an empty note and returns it
        /// </summary>
        /// <param name="context"></param>
        /// <param name="blurbId"></param>
        /// <returns></returns>
        public static Note CreateEmptyNote(BlurbDbContext context, int blurbId)
        {
            var blurb = context.Blurbs.FirstOrDefault(x => x.BlurbId == blurbId);
            Note note = new Note(blurb);
            return note;
        }


        /// <summary>
        /// Creates a note with 'noteBody' body and returns it
        /// </summary>
        /// <param name="context"></param>
        /// <param name="blurbId"></param>
        /// <param name="noteBody"></param>
        /// <returns></returns>
        public static Note CreateNote(BlurbDbContext context, int blurbId, string noteBody)
        {
            var blurb = context.Blurbs.FirstOrDefault(x => x.BlurbId == blurbId);
            Note note = new Note(blurb, noteBody);
            return note;
        }


        public static Note AddNoteToDb(BlurbDbContext context, Note note)
        {
            context.Add(note);
            context.SaveChanges();
            return context.Notes.FirstOrDefault(n => n == note);
        }


        public static bool DeleteNote(BlurbDbContext context, int noteId)
        {
            try
            {
                context.Notes.Remove(context.Notes.FirstOrDefault(n => n.NoteId == noteId));       //Remove all notes that reference it
                context.SaveChanges();
                return true;
            }
            catch (InvalidOperationException e)
            {
                return false;
            }
        }


        public static void FollowUser(BlurbDbContext context, int curUserId, int toFollowId)
        {
            //User curUser = context.Users.FirstOrDefault(u => u.UserId == curUserId);

        }


        /// <summary>
        /// Sorts a list of blurbs by a given sort setting and returns the sorted list
        /// </summary>
        /// <param name="blurbs"></param>
        /// <param name="setting"></param>
        /// <returns></returns>
        public static IQueryable<Blurb> SortBlurbs(IQueryable<Blurb> blurbs, SortSetting setting)
        {
            switch (setting)
            {
                case SortSetting.AZ:                                         //Orders alphabetically A-Z
                    blurbs = blurbs.OrderBy(x => x.Name);
                    break;
                case SortSetting.ZA:                                         //Orders alphabetically Z-A
                    blurbs = blurbs.OrderByDescending(x => x.Name);
                    break;
                case SortSetting.MostRecent:                                 //Orders by most recent
                    blurbs = blurbs.OrderByDescending(x => x.Timestamp);
                    break;
                case SortSetting.LeastRecent:                                //Orders by least recent
                    blurbs = blurbs.OrderBy(x => x.Timestamp);
                    break;
                case SortSetting.ScoreHL:                                    //Orders by rating / 10 high to low
                    blurbs = blurbs.OrderByDescending(x => x.Score);
                    break;
                case SortSetting.ScoreLH:                                    //Orders by rating / 10 low to high
                    blurbs = blurbs.OrderBy(x => x.Score);
                    break;
                default:
                    break;
            }

            return blurbs;
        }



        /// <summary>
        /// Filters blurbs by which types of blurbs (movies, games, books etc.) are being allowed
        /// </summary>
        /// <param name="blurbs"></param>
        /// <param name="typeFilters"></param>
        /// <returns></returns>
        public static IQueryable<Blurb> FilterByType(IQueryable<Blurb> blurbs, Dictionary<Models.Models.Enums.Type, bool> typeFilters)
        {
            blurbs = blurbs.Where(b => typeFilters[b.Media.Type] == true);
            return blurbs;
        }


        /// <summary>
        /// Returns true if the user has permission to see the blurb, false if they do not
        /// </summary>
        /// <param name="user"></param>
        /// <param name="blurb"></param>
        /// <returns></returns>
        public static bool CanSeeBlurb(User user, Blurb blurb)
        {
            if (blurb.Privacy == Privacy.Public)
            {
                return true;
            }
            else  //If the list of IDs in the following list contains this id, return true
            {
                var followingListIds = user.Following.Select(x => x.UserId);

                if (followingListIds.Contains(blurb.UserId) && blurb.Privacy == Privacy.FollowersOnly)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        /// <summary>
        /// Returns the subset of blurbs that a given user has permission to see
        /// </summary>
        /// <param name="blurbs"></param>
        /// <param name="curUser"></param>
        /// <returns></returns>
        public static IQueryable<Blurb> FilterByCanSee(IQueryable<Blurb> blurbs, User curUser)
        {
            blurbs = blurbs.Where(b => b.UserId == curUser.UserId || CanSeeBlurb(curUser, b));
            return blurbs;
        }


        /// <summary>
        /// Filters the blurb query by which users' blurbs the current user wants to see (everyone, their own, or their followers')
        /// </summary>
        /// <param name="blurbs"></param>
        /// <param name="curUser"></param>
        /// <param name="includeSelf"></param>
        /// <param name="includeFollowing"></param>
        /// <param name="includeUnfollowed"></param>
        /// <returns></returns>
        public static IQueryable<Blurb> FilterByUser(IQueryable<Blurb> blurbs, User curUser, bool includeSelf, bool includeFollowing, bool includeUnfollowed)
        {
            var followingListIds = curUser.Following.Select(f => f.UserId);
            blurbs = blurbs
                .Where(b =>
                       (includeFollowing ? followingListIds.Contains(b.UserId) : false)     //If the blurb is from someone you're following and the setting includes them, return true
                    || (includeUnfollowed ? !followingListIds.Contains(b.UserId) : false)   //If the blurb is from someone you're not following and the setting includes them, return true
                    || (includeSelf ? b.UserId == curUser.UserId : false));                 //If the blurb is from you and the settings include you, return true

            return blurbs;
        }


        /// <summary>
        /// Queries and sorts the entire blurb list based on the 'querySettings' settings
        /// </summary>
        /// <param name="context"></param>
        /// <param name="curUser"></param>
        /// <param name="querySettings"></param>
        /// <returns></returns>
        public static List<Blurb> FullQuery(BlurbDbContext context, User curUser, SortFilterSetting querySettings)
        {
            var queriedblurbs = FilterByCanSee(context.Blurbs, curUser);           //Filters out the items the curUser doesn't have permissions to see
            queriedblurbs = FilterByType(queriedblurbs, querySettings.TypeFilter); //Filters by the media type
            queriedblurbs = FilterByUser(queriedblurbs, curUser, querySettings.IncludeSelf, querySettings.IncludeFollowering, querySettings.IncludeUnfollowed); //Filters by the specified users

            var sortedBlurbs = SortBlurbs(queriedblurbs, querySettings.SortSetting); //Sorts by a given sort setting

            return sortedBlurbs.ToList();
        }
    }
}
