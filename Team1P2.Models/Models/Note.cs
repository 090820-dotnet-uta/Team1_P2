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
        /// Initializes an empty note to a specific blurb
        /// </summary>
        /// <param name="blurb"></param>
        public Note(Blurb blurb)
        {
            Blurb = blurb;
        }


        /// <summary>
        /// Initializes a note with a specific body to a specific blurb
        /// </summary>
        /// <param name="blurb"></param>
        /// <param name="body"></param>
        public Note(Blurb blurb, string body)
        {
            Blurb = blurb;
            NoteBody = body;
        }
    }
}
