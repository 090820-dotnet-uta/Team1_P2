using System.Collections.Generic;

namespace Team1P2.Models.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; }
        public List<MediaTag> MediaTags { get; set; } = new List<MediaTag>();


        public Tag(){}


        /// <summary>
        /// constructs a tag with a given name
        /// </summary>
        /// <param name="name"></param>
        public Tag(string name)
        {
            Name = name;
        }


        /// <summary>
        /// constructs a tag with a given name and pre-existing map of tags to media item
        /// </summary>
        /// <param name="name"></param>
        /// <param tagsMap="tagsMap"></param>
        public Tag(string name, List<MediaTag> tagsMap)
        {
            Name = name;
            MediaTags = tagsMap;
        }
    }
}