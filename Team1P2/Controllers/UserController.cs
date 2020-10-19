using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Team1P2.Models.Models;
using Team1P2.Repo.Repository;

namespace Team1P2.Controllers
{

  [Route("api/user")]
  [ApiController]
  public class UserController : Controller
  {
    private readonly Repository _repository;

    public UserController(Repository repository)
    {
      _repository = repository;
    }

    [Produces("application/json")]
    [HttpGet("find/{userId}")]
    public async Task<ActionResult<User>> Find(int userId)
    {
      return await _repository.GetUserAsync(userId);
    }

    [Produces("application/json")]
    [HttpGet("find/all")]
    public async Task<ActionResult<List<User>>> FindAll()
    {
      return await _repository.GetAllUsersAsync();
    }

    [Produces("application/json")]
    [HttpGet("find/all/follower/{userId}")]
    public async Task<ActionResult<List<User>>> FindFollowers(int userId)
    {
      return await _repository.GetFollowers(userId);
    }

    [Produces("application/json")]
    [HttpGet("find/all/following/{userId}")]
    public async Task<ActionResult<List<int>>> FindFollowing(int userId)
    {
      return await _repository.GetFollowing(userId);
    }

    [Produces("application/json")]
    [HttpPost("add")]
    public async Task<ActionResult<User>> Add(User user)
    {
      return await _repository.AddUserToDbAsync(user);
    }

    [Produces("application/json")]
    [HttpPost("login")]
    public async Task<ActionResult<User>> Login(User user)
    {
      return await _repository.LoginAsync(user);
    }

    [Produces("application/json")]
    [HttpPost("follow/{toFollowId}")]
    public async Task<ActionResult<FollowingEntry>> Follow(User user, int toFollowId)
    {
      return await _repository.FollowUser(user.UserId, toFollowId);
    }

    [Produces("application/json")]
    [HttpPut("edit/user")]
    public async Task<ActionResult<User>> EditUser(User user)
    {
      return await _repository.EditUserAsync(user);
    }

    [Produces("application/json")]
    [HttpDelete("remove/follow/{currentUser}/{toRemove}")]
    public async Task<ActionResult<bool>> RemoveFollower(int currentUser, int toRemove)
    {
      return await _repository.UnfollowUser(currentUser, toRemove);
    }
  }
}
