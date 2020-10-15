using System.Collections.Generic;
using Team1P2.Models.Models.Enums;

namespace Team1P2.Models.Models
{
    public class SortFilterSetting
    {
        public SortSetting SortSetting { get; set; }
        public Dictionary<Type, bool> TypeFilter { get; set; }
        public bool IncludeSelf { get; set; }
        public bool IncludeFollowering { get; set; }
        public bool IncludeUnfollowed { get; set; }


        public SortFilterSetting(SortSetting sortSetting, Dictionary<Type, bool> typeFilter, bool includeSelf, bool includeFollowing, bool includeUnfollowed)
        {
            SortSetting = sortSetting;
            TypeFilter = typeFilter;
            IncludeSelf = includeSelf;
            IncludeFollowering = includeFollowing;
            IncludeUnfollowed = includeUnfollowed;
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
            IncludeFollowering = includeFollowing;
            IncludeUnfollowed = includeUnfollowed;
            TypeFilter = new Dictionary<Type, bool>();
            TypeFilter.Add(Type.Book, true);
            TypeFilter.Add(Type.Movie, true);
            TypeFilter.Add(Type.Game, true);
            TypeFilter.Add(Type.TV, true);
        }
    }
}
