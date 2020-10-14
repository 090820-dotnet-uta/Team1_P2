using System;
using System.Collections.Generic;
using System.Text;

namespace Team1P2.Models.Models
{
    public class Note
    {
        public int NoteId { get; set; }
        public int BlurbId { get; set; }
        public Blurb Blurb { get; set; }
        public string NoteBody { get; set; } = "";

        //STRETCH GOALS

        //Note types will contain stuff like "music, directing, cinematography, acting, writing, story etc."
        //Only one type per note
        //public NoteType Type { get; set; } //Make this an enum later

        public Note() { }


        /// <summary>
        /// Initializes a note to a specific blurb
        /// </summary>
        /// <param name="blurb"></param>
        public Note(Blurb blurb)
        {
            Blurb = blurb;
        }
    }
}
