using System;
using System.Collections.Generic;
using System.Text;

namespace Team1P2.Models.Models
{
    public class FullQueryObj
    {
        public SortFilterSetting Settings { get; set; }
        public int SinceId { get; set; } = -1;
        public int Span { get; set; } = -1;


        public FullQueryObj()
        {

        }

        public FullQueryObj(SortFilterSetting settings, int sinceId, int span)
        {
            Settings = settings;
            SinceId = sinceId;
            Span = span;
        }


        public FullQueryObj(SortFilterSetting settings)
        {
            Settings = settings;
        }
    }
}
