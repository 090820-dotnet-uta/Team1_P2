using System.Collections.Generic;
using Team1P2.Models.Models.Enums;

namespace Team1P2.Models.Models
{
    public class SortFilterSetting
    {
        public SortSetting SortSetting { get; set; }
        public bool IncludeBooks { get; set; }
        public bool IncludeMovies { get; set; }
        public bool IncludeTV { get; set; }
        public bool IncludeGames { get; set; }
        public bool IncludeSelf { get; set; }
        public bool IncludeFollowing { get; set; }
        public bool IncludeUnfollowed { get; set; }


        public SortFilterSetting()
        {

        }

        public SortFilterSetting(SortSetting sortSetting, bool includeSelf, bool includeFollowing, bool includeUnfollowed, bool includeBooks, bool includeMovies, bool includeTV, bool includeGames)
        {
            SortSetting = sortSetting;;
            IncludeSelf = includeSelf;
            IncludeFollowing = includeFollowing;
            IncludeUnfollowed = includeUnfollowed;
            IncludeGames = includeGames;
            IncludeBooks = includeBooks;
            IncludeMovies = includeMovies;
            IncludeTV = includeTV;
        }


        /// <summary>
        /// Initializes a query setting including all media types by default
        /// </summary>
        /// <param name="sortSetting"></param>
        /// <param name="includeSelf"></param>
        /// <param name="includeFollowing"></param>
        /// <param name="includeUnfollowed"></param>
        public SortFilterSetting(SortSetting sortSetting, bool includeSelf, bool includeFollowing, bool includeUnfollowed)
        {
            SortSetting = sortSetting;
            IncludeSelf = includeSelf;
            IncludeFollowing = includeFollowing;
            IncludeUnfollowed = includeUnfollowed;
            IncludeBooks = true;
            IncludeTV = true;
            IncludeGames = true;
            IncludeMovies = true;
        }
    }
}
