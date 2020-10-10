using System.Collections.Generic;

namespace Team1P2.Models.Models
{
    public class Media
    {
        public int MediaId { get; set; }
        public Type Type { get; set; }
        public string Name { get; set; }
        public List<MediaTag> MediaTags { get; set; }
    }
}