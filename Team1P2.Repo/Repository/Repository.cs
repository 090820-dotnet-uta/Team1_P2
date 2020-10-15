using Team1P2.Repo.Data;
using System.Collections.Generic;
using System.Linq;
using Team1P2.Models.Models;
using Team1P2.Models.Models.Enums;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Team1P2.Repo.Repository
{
    public class Repository
    {
		private readonly BlurbDbContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
		public Repository(BlurbDbContext context)
        {
			_context = context;
        }


        /// <summary>
        /// UNUSED. This will check to see if the db is seeded
        /// in order to not have to uncomment and recomment
        /// current SeedDb method in UnitOfWork class
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool IsSeeded()
          => (_context.Users.Count() > 0 && _context.Blurbs.Count() > 0);

        /// <summary>
        /// TODO ---> DOES NOT HANDLE FOLLOWING USERS YET!!
        /// method for seeding db with SeedData dummy data
        /// </summary>
        /// <param name="context"></param>
        public void SeedDb()
        {
            SeedData seedData = new SeedData();

            //first add all the users
            foreach (var user in seedData.Users)
            {
                _context.Users.Add(user);
            }
            _context.SaveChanges();

            //next all the medias
            for (int i = 0; i < seedData.Medias.Count; i++)
            {
                _context.Medias.Add(seedData.Medias[i]);
            }
            _context.SaveChanges();

            //then add all the tags
            for (int i = 0; i < seedData.Tags.Count; i++)
            {
                _context.Tags.Add(seedData.Tags[i]);
            }
            _context.SaveChanges();

            //then use tags and medias to add all the mediatags
            for (int i = 0; i < seedData.Medias.Count; i++)
            {
                MediaTag m = new MediaTag(seedData.Tags[i], seedData.Medias[i]);
                _context.MediaTags.Add(m);
            }
            _context.SaveChanges();

            //grab the newly added users
            var u = _context.Users.ToList();

            //add the media and users to the blurbs and add the blurbs
            for (int i = 0; i < seedData.Blurbs.Count; i++)
            {
                seedData.Blurbs[i].Media = seedData.Medias[i];
                seedData.Blurbs[i].User = u[i];
                _context.Blurbs.Add(seedData.Blurbs[i]);
            }
            _context.SaveChanges();

        }


        /// <summary>
        /// Gets a user from the db given a userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<User> GetUserAsync(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        }


        /// <summary>
        /// Gets a list of all users from the db
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }


        /// <summary>
        /// Gets a blurb from the db given a blurbId
        /// </summary>
        /// <param name="blurbId"></param>
        /// <returns></returns>
        public async Task<Blurb> GetBlurbAsync(int blurbId)
        {
            return await _context.Blurbs.FirstOrDefaultAsync(b => b.BlurbId == blurbId);
        }


        /// <summary>
        /// Gets all blurbs from the db
        /// </summary>
        /// <returns></returns>
        public async Task<List<Blurb>> GetAllBlurbsAsync()
        {
            return await _context.Blurbs.ToListAsync();
        }


        /// <summary>
        /// Gets all blurbs for a specific user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<Blurb>> GetBlurbsByUserIdAsync(int userId)
        {
            return await _context.Blurbs.Where(b => b.UserId == userId).ToListAsync();
        }


        /// <summary>
        /// Gets all notes for a specific blurb
        /// </summary>
        /// <param name="blurbId"></param>
        /// <returns></returns>
        public async Task<List<Note>> GetNotesByBlurbIdAsync(int blurbId)
        {
            return await _context.Notes.Where(n => n.BlurbId == blurbId).ToListAsync();
        }


        /// <summary>
        /// Adds a media item to the db and returns it
        /// </summary>
        /// <param name="media"></param>
        /// <returns></returns>
        public async Task<Media> AddMediaToDbAsync(Media media)
        {
            _context.Add(media);
            _context.SaveChanges();
            return await _context.Medias.FirstOrDefaultAsync(x => x == media);
        }


        /// <summary>
        /// Gets a media item from the db
        /// </summary>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public async Task<Media> GetMediaAsync(int mediaId)
        {
            return await _context.Medias.FirstOrDefaultAsync(m => m.MediaId == mediaId);
        }


        /// <summary>
        /// Gets all media items from the db
        /// </summary>
        /// <returns></returns>
        public async Task<List<Media>> GetAllMediaAsync()
        {
            return await _context.Medias.ToListAsync();
        }


        /// <summary>
        /// Adds a task to the db and returns the added task.
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public async Task<Tag> AddTagToDbAsync(Tag tag)
        {
            _context.Add(tag);
            _context.SaveChanges();
            return await _context.Tags.FirstOrDefaultAsync(t => t == tag);
        }


        /// <summary>
        /// Gets a tag from the db
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public async Task<Tag> GetTagAsync(int tagId)
        {
            return await _context.Tags.FirstOrDefaultAsync(t => t.TagId == tagId);
        }


        /// <summary>
        /// Gets all tags from the db
        /// </summary>
        /// <returns></returns>
        public async Task<List<Tag>> GetAllTagsAsync()
        {
            return await _context.Tags.ToListAsync();
        }


        /// <summary>
        /// Adds a media tag relation to the db and returns it
        /// </summary>
        /// <param name="mediaTag"></param>
        /// <returns></returns>
        public async Task<MediaTag> AddMediaTagToDbAsync(MediaTag mediaTag)
        {
            _context.Add(mediaTag);
            _context.SaveChanges();
            return await _context.MediaTags.FirstOrDefaultAsync(t => t == mediaTag);
        }


        /// <summary>
        /// Gets a mediaTag item from the db
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public async Task<MediaTag> GetMediaTagAsync(int tagId)
        {
            return await _context.MediaTags.FirstOrDefaultAsync(t => t.TagId == tagId);
        }


        /// <summary>
        /// Gets all mediaTag objects from the db
        /// </summary>
        /// <returns></returns>
        public async Task<List<MediaTag>> GetAllMediaTagsAsync()
        {
            return await _context.MediaTags.ToListAsync();
        }


        /// <summary>
        /// Gets a list of tags associated with a given media object
        /// </summary>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public async Task<List<Tag>> GetTagsByMediaId(int mediaId)
        {
            var mediaTags = _context.MediaTags.Where(m => m.MediaId == mediaId).Select(x => x.TagId);
            return await _context.Tags
                .Where(t => mediaTags.Contains(t.TagId)) //Gets the list of tags where the join table contains the tagId
                .ToListAsync();
        }


        /// <summary>
        /// Gets the list of media associated with a given tag
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public async Task<List<Media>> GetMediaByTagId(int tagId)
        {
            var mediaTags = _context.MediaTags.Where(m => m.TagId == tagId).Select(x => x.MediaId);
            return await _context.Medias
                .Where(t => mediaTags.Contains(t.MediaId)) //Gets the list of media where the join table contains the mediaId
                .ToListAsync();
        }


        /// <summary>
        /// Adds the user to the db and saves changes
        /// </summary>
        /// <param name="context"></param>
        /// <param name="user"></param>
        public async Task<User> AddUserToDbAsync(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
            return await _context.Users.FirstOrDefaultAsync(u => u == user);
        }


        /// <summary>
        /// Updates a user's username and saves changes
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userId"></param>
        /// <param name="username"></param>
        public async Task<User> EditUsernameAsync(int userId, string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            user.Username = username;
            _context.Update(user);
            _context.SaveChanges();
            return await _context.Users.FirstOrDefaultAsync(u => u == user);
        }


        /// <summary>
        ///  Updates a user's screen name and saves changes
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userId"></param>
        /// <param name="screenName"></param>
        public async Task<User> EditScreenNameAsync(int userId, string screenName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            user.ScreenName = screenName;
            _context.Update(user);
            _context.SaveChanges();
            return await _context.Users.FirstOrDefaultAsync(u => u == user);
        }


        /// <summary>
        /// Updates a user's name and saves changes
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        public async Task<User> EditNameAsync(int userId, string name)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            user.Name = name;
            _context.Update(user);
            _context.SaveChanges();
            return await _context.Users.FirstOrDefaultAsync(u => u == user);
        }


        /// <summary>
        /// Updates a user's password and saves changes. Returns the edited user.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        public async Task<User> EditPasswordAsync(int userId, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            user.Password = password;
            _context.Update(user);
            _context.SaveChanges();
            return await _context.Users.FirstOrDefaultAsync(u => u == user);
        }


        /// <summary>
        /// Adds the blurb to the db and saves changes
        /// </summary>
        /// <param name="context"></param>
        /// <param name="blurb"></param>
        public async Task<Blurb> AddBlurbToDbAsync(Blurb blurb)
        {
            _context.Add(blurb);
            _context.SaveChanges();
            return await _context.Blurbs.FirstOrDefaultAsync(b => b == blurb);
        }


        /// <summary>
        /// Deletes a blurb from the db as well as all notes referencing it. Returns true upon success.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="blurbId"></param>
        public bool DeleteBlurb(int blurbId)
        {
            try
            {
                _context.Blurbs.Remove(_context.Blurbs.FirstOrDefault(b => b.BlurbId == blurbId)); //Remove the actual blurb
                _context.Notes.RemoveRange(_context.Notes.Where(n => n.BlurbId == blurbId));       //Remove all notes that reference it
                _context.SaveChanges();
                return true;
            }
            catch (InvalidOperationException e)
            {
                return false;
            }
        }


        /// <summary>
        /// Edits the score for a given blurb and saves to db. Returns the edited blurb.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="blurbId"></param>
        /// <param name="newScore"></param>
        public async Task<Blurb> EditBlurbScoreAsync(int blurbId, double newScore)
        {
            var blurb = await _context.Blurbs.FirstOrDefaultAsync(x => x.BlurbId == blurbId);
            blurb.Score = newScore;
            _context.Update(blurb);
            _context.SaveChanges();
            return await _context.Blurbs.FirstOrDefaultAsync(b => b == blurb);
        }


        /// <summary>
        /// Edits the privacy setting for a given blurb and saves to db. Returns the edited blurb.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="blurbId"></param>
        /// <param name="privacy"></param>
        public async Task<Blurb> EditBlurbPrivacyAsync(int blurbId, int privacy)
        {
            var blurb = await _context.Blurbs.FirstOrDefaultAsync(x => x.BlurbId == blurbId);
            if (privacy < Enum.GetNames(typeof(Privacy)).Length)
            {
                blurb.Privacy = (Privacy)privacy;
            }
            _context.Update(blurb);
            _context.SaveChanges();
            return await _context.Blurbs.FirstOrDefaultAsync(b => b == blurb);
        }


        /// <summary>
        /// Edits the blurb message for a given blurb and saves to db. Returns the edited blurb.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="blurbId"></param>
        /// <param name="message"></param>
        public async Task<Blurb> EditBlurbMessageAsync(int blurbId, string message)
        {
            var blurb = await _context.Blurbs.FirstOrDefaultAsync(x => x.BlurbId == blurbId);
            blurb.Message = message;
            _context.Update(blurb);
            _context.SaveChanges();
            return await _context.Blurbs.FirstOrDefaultAsync(b => b == blurb);
        }

        /// <summary>
        /// Creates an empty note and returns it
        /// </summary>
        /// <param name="context"></param>
        /// <param name="blurbId"></param>
        /// <returns></returns>
        public async Task<Note> CreateEmptyNoteAsync(int blurbId)
        {
            var blurb = await _context.Blurbs.FirstOrDefaultAsync(x => x.BlurbId == blurbId);
            Note note = new Note(blurb);
            return note;
        }


        /// <summary>
        /// Creates a note with 'noteBody' body and returns it
        /// </summary>
        /// <param name="context"></param>
        /// <param name="blurbId"></param>
        /// <param name="noteBody"></param>
        /// <returns></returns>
        public async Task<Note> CreateNoteAsync(int blurbId, string noteBody)
        {
            var blurb = await _context.Blurbs.FirstOrDefaultAsync(x => x.BlurbId == blurbId);
            Note note = new Note(blurb, noteBody);
            return note;
        }


        /// <summary>
        /// Adds a note to the db and returns it
        /// </summary>
        /// <param name="note"></param>
        /// <returns></returns>
        public async Task<Note> AddNoteToDbAsync(Note note)
        {
            _context.Add(note);
            _context.SaveChanges();
            return await _context.Notes.FirstOrDefaultAsync(n => n == note);
        }


        public async Task<bool> DeleteNoteAsync(int noteId)
        {
            try
            {
                _context.Notes.Remove(await _context.Notes.FirstOrDefaultAsync(n => n.NoteId == noteId));       //Remove all notes that reference it
                _context.SaveChanges();
                return true;
            }
            catch (InvalidOperationException e)
            {
                return false;
            }
        }


        /// <summary>
        /// Gets a note from the db by id
        /// </summary>
        /// <param name="noteId"></param>
        /// <returns></returns>
        public async Task<Note> GetNoteAsync(int noteId)
        {
            return await _context.Notes.FirstOrDefaultAsync(n => n.NoteId == noteId);
        }

        //NEED TO CHANGE THE MODELS BEFORE DOING THIS
        //public async void FollowUser(int curUserId, int toFollowId)
        //{
        //    User curUser = _context.Users.FirstOrDefault(u => u.UserId == curUserId);
           
        //}


        /// <summary>
        /// Sorts a list of blurbs by a given sort setting and returns the sorted list
        /// </summary>
        /// <param name="blurbs"></param>
        /// <param name="setting"></param>
        /// <returns></returns>
        public IQueryable<Blurb> SortBlurbs(IQueryable<Blurb> blurbs, SortSetting setting)
        {
            switch (setting)
            {
                case SortSetting.AZ:                                         //Orders alphabetically A-Z
                    blurbs = blurbs.OrderBy(x => x.Name);
                    break;
                case SortSetting.ZA:                                         //Orders alphabetically Z-A
                    blurbs = blurbs.OrderByDescending(x => x.Name);
                    break;
                case SortSetting.MostRecent:                                 //Orders by most recent
                    blurbs = blurbs.OrderByDescending(x => x.Timestamp);
                    break;
                case SortSetting.LeastRecent:                                //Orders by least recent
                    blurbs = blurbs.OrderBy(x => x.Timestamp);
                    break;
                case SortSetting.ScoreHL:                                    //Orders by rating / 10 high to low
                    blurbs = blurbs.OrderByDescending(x => x.Score);
                    break;
                case SortSetting.ScoreLH:                                    //Orders by rating / 10 low to high
                    blurbs = blurbs.OrderBy(x => x.Score);
                    break;
                default:
                    break;
            }

            return blurbs;
        }



        /// <summary>
        /// Filters blurbs by which types of blurbs (movies, games, books etc.) are being allowed
        /// </summary>
        /// <param name="blurbs"></param>
        /// <param name="typeFilters"></param>
        /// <returns></returns>
        public IQueryable<Blurb> FilterByType(IQueryable<Blurb> blurbs, Dictionary<Models.Models.Enums.Type, bool> typeFilters)
        {
            blurbs = blurbs.Where(b => typeFilters[b.Media.Type] == true);
            return blurbs;
        }


        /// <summary>
        /// Returns true if the user has permission to see the blurb, false if they do not
        /// </summary>
        /// <param name="user"></param>
        /// <param name="blurb"></param>
        /// <returns></returns>
        public bool CanSeeBlurb(User user, Blurb blurb)
        {
            if (blurb.Privacy == Privacy.Public)
            {
                return true;
            }
            else  //If the list of IDs in the following list contains this id, return true
            {
                var followingListIds = user.Following.Select(x => x.UserId);

                if (followingListIds.Contains(blurb.UserId) && blurb.Privacy == Privacy.FollowersOnly)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        /// <summary>
        /// Returns the subset of blurbs that a given user has permission to see
        /// </summary>
        /// <param name="blurbs"></param>
        /// <param name="curUser"></param>
        /// <returns></returns>
        public IQueryable<Blurb> FilterByCanSee(IQueryable<Blurb> blurbs, User curUser)
        {
            var followingListIds = curUser.Following.Select(f => f.UserId);

            blurbs = blurbs.Where(b => b.UserId == curUser.UserId ||                                 //If the blurb is the current user's, or.....
                (b.Privacy == Privacy.Public ? true                                                  //if privacy is public, return true...
                : (followingListIds.Contains(b.UserId) && b.Privacy == Privacy.FollowersOnly ? true  //else if privacy is followers and user is a follower, return true...
                : false                                                                              //else return false
                )));

            return blurbs;
        }


        /// <summary>
        /// Filters the blurb query by which users' blurbs the current user wants to see (everyone, their own, or their followers')
        /// </summary>
        /// <param name="blurbs"></param>
        /// <param name="curUser"></param>
        /// <param name="includeSelf"></param>
        /// <param name="includeFollowing"></param>
        /// <param name="includeUnfollowed"></param>
        /// <returns></returns>
        public IQueryable<Blurb> FilterByUser(IQueryable<Blurb> blurbs, User curUser, bool includeSelf, bool includeFollowing, bool includeUnfollowed)
        {
            var followingListIds = curUser.Following.Select(f => f.UserId);
            blurbs = blurbs
                .Where(b =>
                       (includeFollowing ? followingListIds.Contains(b.UserId) : false)     //If the blurb is from someone you're following and the setting includes them, return true
                    || (includeUnfollowed ? !followingListIds.Contains(b.UserId) : false)   //If the blurb is from someone you're not following and the setting includes them, return true
                    || (includeSelf ? b.UserId == curUser.UserId : false));                 //If the blurb is from you and the settings include you, return true

            return blurbs;
        }


        /// <summary>
        /// Queries and sorts the entire blurb list based on the 'querySettings' settings
        /// </summary>
        /// <param name="context"></param>
        /// <param name="curUser"></param>
        /// <param name="querySettings"></param>
        /// <returns></returns>
        public async Task<List<Blurb>> FullQuery(User curUser, SortFilterSetting querySettings)
        {
            var queriedblurbs = FilterByCanSee(_context.Blurbs, curUser);           //Filters out the items the curUser doesn't have permissions to see
            queriedblurbs = FilterByType(queriedblurbs, querySettings.TypeFilter); //Filters by the media type
            queriedblurbs = FilterByUser(queriedblurbs, curUser, querySettings.IncludeSelf, querySettings.IncludeFollowering, querySettings.IncludeUnfollowed); //Filters by the specified users

            var sortedBlurbs = SortBlurbs(queriedblurbs, querySettings.SortSetting); //Sorts by a given sort setting

            return await sortedBlurbs.ToListAsync();
        }
    }
}
