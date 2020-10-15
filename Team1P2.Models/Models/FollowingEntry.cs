namespace Team1P2.Models.Models
{
    public class FollowingEntry
    {
        public int FollowingEntryId { get; set; }
        public int UserId { get; set; }
        public int FollowedUserId { get; set; }
        public User FollowedUser { get; set; }


        public FollowingEntry() { }


        public FollowingEntry(int followerId, User followedUser)
        {
            UserId = followerId;
            FollowedUser = followedUser;
        }
    }
}
