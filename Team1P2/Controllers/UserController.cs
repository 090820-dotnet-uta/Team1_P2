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
    [HttpGet("find/{id}")]
    public async Task<ActionResult<User>> Find(int id)
    {
      return await _repository.GetUserAsync(id);
    }

    [Produces("application/json")]
    [HttpGet("find/all")]
    public async Task<ActionResult<List<User>>> FindAll()
    {
      return await _repository.GetAllUsersAsync();
    }

    [Produces("application/json")]
    [HttpPost("add")]
    public async Task<ActionResult<User>> Add(User user)
    {
      return await _repository.AddUserToDbAsync(user);
    }

    [Produces("application/json")]
    [HttpPut("edit/username")]
    public async Task<ActionResult<User>> EditUsername(User user)
    {
      return await _repository.EditUsernameAsync(user.UserId, user.Username);
    }

    [Produces("application/json")]
    [HttpPut("edit/screenname")]
    public async Task<ActionResult<User>> EditScreenName(User user)
    {
      return await _repository.EditScreenNameAsync(user.UserId, user.ScreenName);
    }

    [Produces("application/json")]
    [HttpPut("edit/name")]
    public async Task<ActionResult<User>> EditName(User user)
    {
      return await _repository.EditNameAsync(user.UserId, user.Name);
    }

    [Produces("application/json")]
    [HttpPut("edit/password")]
    public async Task<ActionResult<User>> EditPassword(User user)
    {
      return await _repository.EditPasswordAsync(user.UserId, user.Password);
    }

  }
}
