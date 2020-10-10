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
    }
}
