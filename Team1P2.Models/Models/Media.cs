using System.Collections.Generic;
using Team1P2.Models.Models.Enums;

namespace Team1P2.Models.Models
{
    public class Media
    {
        public int MediaId { get; set; }
        public Type Type { get; set; }
        public string Name { get; set; }
        public List<MediaTag> MediaTags { get; set; } = new List<MediaTag>();


        public Media() {}


        /// <summary>
        /// Constructs a media object given its type, name
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        public Media(Type type, string name)
        {
            Type = type;
            Name = name;
        }


        /// <summary>
        /// Constructs a media object given its type, name, and list of tags
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="tags"></param>
        public Media(Type type, string name, List<MediaTag> tagsMap)
        {
            Type = type;
            Name = name;
            MediaTags = tagsMap;
        }
    }
}