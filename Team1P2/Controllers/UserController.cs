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
    [HttpGet("find")]
    public async Task<ActionResult<User>> Find()
    {
            return await _repository.GetUserAsync(1);
    }

    [Produces("application/json")]
    [HttpGet("findall")]
    public async Task<ActionResult<List<User>>> FindAll()
    {
      return await _repository.GetAllUsersAsync();
    }
  }
}
