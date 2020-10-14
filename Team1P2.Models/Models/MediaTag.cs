using System;
using System.Collections.Generic;
using System.Text;

namespace Team1P2.Models.Models
{
    /// <summary>
    /// This is the join table for genre tags and media times
    /// </summary>
    public class MediaTag
    {
        public int MediaTagId { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        public int MediaId { get; set; }
        public Media Media { get; set; }


        /// <summary>
        /// Empty constructor for MediaTag object
        /// </summary>
        public MediaTag() {}


        /// <summary>
        /// Constructs a media tag which maps a specific tag to a specific media object
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="media"></param>
        public MediaTag(Tag tag, Media media)
        {
            Tag = tag;
            Media = media;
        }
    }
}
