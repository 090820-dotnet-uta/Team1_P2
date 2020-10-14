using System.Collections.Generic;
using Team1P2.Models.Models.Enums;

namespace Team1P2.Models.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string ScreenName { get; set; }
        public List<User> Following { get; set; } = new List<User>();

        //STRETCH GOAL IMPLEMENTATIONS
        //  public Privacy DefaultPrivacy { get; set; }


        public User() { }


        /// <summary>
        /// Default constructor for making a user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public User(string username, string password)
        {
            Username = username;
            Password = password;
            ScreenName = username; //by default, the screen name is the same as the username
        }
    }
}