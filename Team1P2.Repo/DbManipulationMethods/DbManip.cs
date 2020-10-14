using System.Collections.Generic;
using System.Linq;
using Team1P2.Models.Models;
using Team1P2.Models.Models.Enums;
using Team1P2.Repo.Data;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace Team1P2.Repo.DbManipulationMethods
{
    public static class DbManip
    {
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
        public static IQueryable<Blurb> FilterByType(IQueryable<Blurb> blurbs, Dictionary<Type, bool> typeFilters)
        {
            //This is one big linq query using a bunch of nested ternaries to determine the proper settings
            //Think of it as a bunch of if else statements, but I have to use ternaries since it's in a linq query
            blurbs
                .Where(b =>
                (b.Media.Type == Type.Movie ? (typeFilters[Type.Movie]) :        //If  the blurb is a movie and the filter setting includes movies, return true
                (b.Media.Type == Type.Game ? (typeFilters[Type.Game]) :          //If  the blurb is a game and the filter setting includes games, return true
                (b.Media.Type == Type.Book ? (typeFilters[Type.Book]) :          //If  the blurb is a book and the filter setting includes books, return true
                (b.Media.Type == Type.TV ? (typeFilters[Type.TV]) : false)))));  //If  the blurb is a tv show and the filter setting includes tv shows, return true

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
