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
    [HttpPost("add")]
    public async Task<ActionResult<User>> Add(User user)
    {
      return await _repository.AddUserToDbAsync(user);
    }

    [Produces("application/json")]
    [HttpPut("edit/user")]
    public async Task<ActionResult<User>> EditUser(User user)
    {
      return await _repository.EditUserAsync(user.UserId, user.Username, user.ScreenName, user.Name, user.Password);
    }

  }
}
