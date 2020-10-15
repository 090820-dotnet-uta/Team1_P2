using System.Collections.Generic;
using System.Linq;

namespace Team1P2.Models.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string ScreenName { get; set; }
        //public List<User> Following { get; set; } = new List<User>();
        //public List<User> Followers { get; set; } = new List<User>();
        public List<FollowingEntry> FollowingEntries { get; set; } = new List<FollowingEntry>();

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


        /// <summary>
        /// Queries the followerEntry list to get the list of users this user is following
        /// </summary>
        /// <returns></returns>
        public List<User> GetFollowingList()
        {
            return FollowingEntries.Where(y => y.UserId == UserId).Select(x => x.FollowedUser).ToList();
        }
    }
}