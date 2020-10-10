using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
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
    }
}
