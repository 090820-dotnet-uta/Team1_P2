using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Team1P2.Models.Models;
using Team1P2.Models.Models.Enums;
using Team1P2.Repo.Data;

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
      return await _context.Users.Include(u => u.FollowingEntries).FirstOrDefaultAsync(u => u.UserId == userId);
    }


    /// <summary>
    /// Gets a list of all users from the db
    /// </summary>
    /// <returns></returns>
    public async Task<List<User>> GetAllUsersAsync()
    {
      return await _context.Users.Include(u => u.FollowingEntries).ToListAsync();
    }


    /// <summary>
    /// Gets a blurb from the db given a blurbId
    /// </summary>
    /// <param name="blurbId"></param>
    /// <returns></returns>
    public async Task<Blurb> GetBlurbAsync(int blurbId)
    {
      return await _context.Blurbs.Include(b => b.Media).Include(b => b.User).Include(b => b.Notes).FirstOrDefaultAsync(b => b.BlurbId == blurbId);
    }


    /// <summary>
    /// Gets all blurbs from the db
    /// </summary>
    /// <returns></returns>
    public async Task<List<Blurb>> GetAllBlurbsAsync()
    {
      return await _context.Blurbs.Include(b => b.Media).Include(b => b.User).Include(b => b.Notes).ToListAsync();
    }


    /// <summary>
    /// Gets all blurbs for a specific user
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<List<Blurb>> GetBlurbsByUserIdAsync(int userId)
    {
      return await _context.Blurbs.Include(b => b.Media).Include(b => b.User).Where(b => b.UserId == userId).ToListAsync();
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
      return await _context.Medias.Include(x => x.MediaTags).FirstOrDefaultAsync(x => x == media);
    }


    /// <summary>
    /// Gets a media item from the db
    /// </summary>
    /// <param name="mediaId"></param>
    /// <returns></returns>
    public async Task<Media> GetMediaAsync(int mediaId)
    {
      return await _context.Medias.Include(x => x.MediaTags).FirstOrDefaultAsync(m => m.MediaId == mediaId);
    }


    /// <summary>
    /// Gets all media items from the db
    /// </summary>
    /// <returns></returns>
    public async Task<List<Media>> GetAllMediaAsync()
    {
      return await _context.Medias.Include(x => x.MediaTags).ToListAsync();
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
      return await _context.Tags.Include(x => x.MediaTags).FirstOrDefaultAsync(t => t == tag);
    }


    /// <summary>
    /// Gets a tag from the db
    /// </summary>
    /// <param name="tagId"></param>
    /// <returns></returns>
    public async Task<Tag> GetTagAsync(int tagId)
    {
      return await _context.Tags.Include(x => x.MediaTags).FirstOrDefaultAsync(t => t.TagId == tagId);
    }


    /// <summary>
    /// Gets all tags from the db
    /// </summary>
    /// <returns></returns>
    public async Task<List<Tag>> GetAllTagsAsync()
    {
      return await _context.Tags.Include(x => x.MediaTags).ToListAsync();
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
      return await _context.MediaTags.Include(m => m.Tag).Include(m => m.Media).FirstOrDefaultAsync(t => t == mediaTag);
    }


    /// <summary>
    /// Gets a mediaTag item from the db
    /// </summary>
    /// <param name="tagId"></param>
    /// <returns></returns>
    public async Task<MediaTag> GetMediaTagAsync(int tagId)
    {
      return await _context.MediaTags.Include(m => m.Tag).Include(m => m.Media).FirstOrDefaultAsync(t => t.TagId == tagId);
    }


    /// <summary>
    /// Gets all mediaTag objects from the db
    /// </summary>
    /// <returns></returns>
    public async Task<List<MediaTag>> GetAllMediaTagsAsync()
    {
      return await _context.MediaTags.Include(m => m.Tag).Include(m => m.Media).ToListAsync();
    }


    /// <summary>
    /// Gets a list of tags associated with a given media object
    /// </summary>
    /// <param name="mediaId"></param>
    /// <returns></returns>
    public async Task<List<Tag>> GetTagsByMediaId(int mediaId)
    {
      var mediaTags = _context.MediaTags.Where(m => m.MediaId == mediaId).Select(x => x.TagId);
      return await _context.Tags.Include(t => t.MediaTags)
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
      return await _context.Medias.Include(m => m.MediaTags)
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
      return await _context.Users.Include(x => x.FollowingEntries).FirstOrDefaultAsync(u => u == user);
    }

    /// <summary>
    /// mock login
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public async Task<User> LoginAsync(User user)
    {
      return await _context.Users.FirstOrDefaultAsync(u => u.Username == user.Username && u.Password == user.Password);
    }


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
    /// Updates a user's username and saves changes
    /// </summary>
    /// <param name="context"></param>
    /// <param name="userId"></param>
    /// <param name="username"></param>
    public async Task<User> EditUserAsync(User user)
    {
      //User userInDb = await _context.Users.Include(u => u.FollowingEntries).FirstOrDefaultAsync(f => f.UserId == user.UserId);

      ////Deletes all the notes in the db blurb entry that have been excluded in the blurb param
      //var userDbFollowerEntriesExcluded = userInDb.FollowingEntries.Except(user.FollowingEntries);
      //_context.FollowingEntries.RemoveRange(userDbFollowerEntriesExcluded);
      //_context.Entry(userInDb).State = EntityState.Detached;
      //_context.SaveChanges();

      //_context.Update(user);
      //_context.Entry(user).State = EntityState.Modified;
      //_context.SaveChanges();

      //return await _context.Users.Include(f => f.FollowingEntries).FirstOrDefaultAsync(u => u.UserId == user.UserId);
      var us = await _context.Users.FirstOrDefaultAsync(x => x.UserId == user.UserId);
      user.Username = user.Username;
      user.ScreenName = user.ScreenName;
      user.Name = user.Name;
      user.Password = user.Password;
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
      return await _context.Blurbs.Include(x => x.Media).FirstOrDefaultAsync(b => b == blurb);
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
      Note note = new Note(blurb.BlurbId);
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
      Note note = new Note(blurb.BlurbId, noteBody);
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


    /// <summary>
    /// Updates properties blurb object
    /// </summary>
    /// <param name="blurb"></param>
    /// <returns></returns>
    public async Task<Blurb> UpdateBlurb(Blurb blurb)
    {
      Blurb blurbInDb = await _context.Blurbs.Include(b => b.Notes).FirstOrDefaultAsync(b => b.BlurbId == blurb.BlurbId);

      //Deletes all the notes in the db blurb entry that have been excluded in the blurb param
      var blurbDbNotesExcluded = blurbInDb.Notes.Except(blurb.Notes);
      _context.Notes.RemoveRange(blurbDbNotesExcluded);
      _context.Entry(blurbInDb).State = EntityState.Detached;
      _context.SaveChanges();

      _context.Update(blurb);
      _context.Entry(blurb).State = EntityState.Modified;
      _context.SaveChanges();

      return await _context.Blurbs.Include(b => b.Media).Include(b => b.User).Include(b => b.Notes).FirstOrDefaultAsync(b => b.BlurbId == blurb.BlurbId);
    }


    /// <summary>
    /// Deletes note from db
    /// </summary>
    /// <param name="noteId"></param>
    /// <returns></returns>
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

    public async Task<FollowingEntry> FollowUser(int curUserId, int toFollowId)
    {
      var followedUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == toFollowId);
      FollowingEntry newEntry = new FollowingEntry(curUserId, followedUser);
      _context.Add(newEntry);
      _context.SaveChanges();
      return await _context.FollowingEntries.FirstOrDefaultAsync(x => x == newEntry);
    }

    public async Task<bool> UnfollowUser(int curUserId, int toUnfollowId)
    {
      try
      {
        var toRemove = await _context.FollowingEntries.FirstOrDefaultAsync(f => f.UserId == curUserId && f.FollowedUserId == toUnfollowId);
        _context.Remove(toRemove);
        _context.SaveChanges();
        return true;
      }
      catch (InvalidOperationException e)
      {
        return false;
      }
    }


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
    public IQueryable<Blurb> FilterByType(IQueryable<Blurb> blurbs, bool includeMovies, bool includeBooks, bool includeGames, bool includeTV)
    {
      blurbs = blurbs.Include(b => b.User).Include(b => b.Media)
          .Where(b =>
                 (b.Media.Type == Models.Models.Enums.Type.Book ? includeBooks :
                 (b.Media.Type == Models.Models.Enums.Type.Movie ? includeMovies :
                 (b.Media.Type == Models.Models.Enums.Type.TV ? includeTV :
                 includeGames))));

      return blurbs;
    }


    /// <summary>
    /// Queries the followerEntry list to get the list of users following this user
    /// </summary>
    /// <returns></returns>
    public async Task<List<User>> GetFollowers(int userId)
    {
      var followersIds = _context.FollowingEntries.Where(f => f.FollowedUserId == userId).Select(x => x.UserId); //Gets the userIds of all the ppl following the user
      return await _context.Users.Where(u => followersIds.Contains(u.UserId)).ToListAsync();
    }

    public async Task<List<int>> GetFollowing(int userId)
    {
      var followersIds = _context.FollowingEntries.Where(f => f.UserId == userId).Select(x => x.FollowedUserId); //Gets the userIds of all the ppl following the user
      return await _context.Users.Where(u => followersIds.Contains(u.UserId)).Select(i => i.UserId).ToListAsync();
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
        var followingListIds = user.GetFollowingList().Select(x => x.UserId);

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
      var followingListIds = curUser.GetFollowingList().Select(f => f.UserId);

      blurbs = blurbs.Include(b => b.User).Include(b => b.Media).Where(b => b.UserId == curUser.UserId ||                                 //If the blurb is the current user's, or.....
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
      var followingListIds = curUser.GetFollowingList().Select(f => f.UserId);
      blurbs = blurbs.Include(b => b.User).Include(b => b.Media)
          .Where(b =>
                 (includeFollowing && b.UserId != curUser.UserId ? followingListIds.Contains(b.UserId) : false)     //If the blurb is from someone you're following and the setting includes them, return true
              || (includeUnfollowed && b.UserId != curUser.UserId ? !followingListIds.Contains(b.UserId) : false)   //If the blurb is from someone you're not following and the setting includes them, return true
              || (includeSelf && b.UserId == curUser.UserId));                 //If the blurb is from you and the settings include you, return true

      return blurbs;
    }


    /// <summary>
    /// Queries and sorts the entire blurb list based on the 'querySettings' settings. Pass a nmber less than 1 as sinceId if you want to start at the beginning.
    /// Pass a number less than 1 as count if you want to get all items (or leave it empty), otherwise specify a number of items to get.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="curUser"></param>
    /// <param name="querySettings"></param>
    /// <returns></returns>
    public async Task<List<Blurb>> FullQuery(int userId, SortFilterSetting querySettings, int sinceId = 0, int count = 0)
    {
      var curUser = _context.Users.FirstOrDefault(u => u.UserId == userId);
      var queriedblurbs = FilterByCanSee(_context.Blurbs, curUser);          //Filters out the items the curUser doesn't have permissions to see
      queriedblurbs = FilterByType(queriedblurbs, querySettings.IncludeMovies, querySettings.IncludeBooks, querySettings.IncludeGames, querySettings.IncludeTV); //Filters by the media type
      queriedblurbs = FilterByUser(queriedblurbs, curUser, querySettings.IncludeSelf, querySettings.IncludeFollowing, querySettings.IncludeUnfollowed); //Filters by the specified users

      count = (count <= 0 ? queriedblurbs.Count() : count);

      queriedblurbs = SortBlurbs(queriedblurbs, querySettings.SortSetting); //Sorts by a given sort setting

      if (queriedblurbs.Select(x => x.UserId).Contains(sinceId)) //IF we left off somewhere, find that place, otherwise go to the start
      {
        var toList = queriedblurbs.AsEnumerable(); //VERY TIME INEFFICIENT
        return toList.SkipWhile(b => b.UserId != sinceId).Take(count).ToList();
      }
      else
      {
        return await queriedblurbs.Take(count).ToListAsync();
      }
    }
  }
}
