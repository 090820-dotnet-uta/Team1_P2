using System;
using Team1P2.Models.Models.Enums;

namespace Team1P2.Models.Models
{
    public class Blurb
    {
        public int BlurbId { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public double Score { get; set; }
        public Privacy Privacy { get; set; } = Privacy.Public;
        public string Name { get; set; }
        public int MediaId { get; set; }
        public Media Media { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now; //Default timestamp is when the blurb object is made


        public Blurb() { }


        /// <summary>
        /// Constructs a new blurb, given the user, score, and media added
        /// </summary>
        /// <param name="user"></param>
        /// <param name="score"></param>
        /// <param name="media"></param>
        public Blurb(User user, double score, Media media)
        {
            User = user;
            Score = score;
            Name = media.Name;
            Media = media;
            Timestamp = DateTime.Now;
        }


        /// <summary>
        /// Constructs a new blurb, given teh user, score, media, and blurb name added
        /// </summary>
        /// <param name="user"></param>
        /// <param name="score"></param>
        /// <param name="media"></param>
        /// <param name="name"></param>
        public Blurb(User user, double score, Media media, string name)
        {
            User = user;
            Score = score;
            Name = name;
            Media = media;
            Timestamp = DateTime.Now;
        }
    }
}
