namespace Team1P2.Models.Models
{
    public class Note
    {
        public int NoteId { get; set; }
        public int BlurbId { get; set; }
        public string NoteBody { get; set; } = "";

        //STRETCH GOALS

        //Note types will contain stuff like "music, directing, cinematography, acting, writing, story etc."
        //Only one type per note
        //public NoteType Type { get; set; } //Make this an enum later

        public Note() { }


        /// <summary>
        /// Adds empty note to blurb
        /// </summary>
        /// <param name="blurbId"></param>
        public Note(int blurbId)
        {
            BlurbId = blurbId;
        }


        /// <summary>
        /// Adds note with body
        /// </summary>
        /// <param name="blurbId"></param>
        /// <param name="body"></param>
        public Note(int blurbId, string body)
        {
            BlurbId = blurbId;
            NoteBody = body;
        }
    }
}
