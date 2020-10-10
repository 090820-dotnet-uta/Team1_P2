using System.Collections.Generic;

namespace Team1P2.Models.Models
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; }
        public List<MediaTag> MediaTags { get; set; }
    }
}