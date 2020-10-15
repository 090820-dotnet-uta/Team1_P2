using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Team1P2.Models.Models;
using Team1P2.Models.Models.Enums;
using Team1P2.Repo.Repository;

namespace Team1P2.Controllers
{
  [Route("api/blurb")]
  [ApiController]
  public class BlurbController : Controller
  {
    private readonly Repository _repository;

    public BlurbController(Repository repository)
    {
      _repository = repository;
    }

    [Produces("application/json")]
    [HttpGet("find/{blurbId}")]
    public async Task<ActionResult<Blurb>> Find(int blurbId)
    {
      return await _repository.GetBlurbAsync(blurbId);
    }

    [Produces("application/json")]
    [HttpGet("find/all")]
    public async Task<ActionResult<List<Blurb>>> FindAll()
    {
      return await _repository.GetAllBlurbsAsync();
    }

    [Produces("application/json")]
    [HttpGet("find/all/user/{userId}")]
    public async Task<ActionResult<List<Blurb>>> FindAllByUser(int userId)
    {
      return await _repository.GetBlurbsByUserIdAsync(userId);
    }

    [Produces("application/json")]
    [HttpPost("add")]
    public async Task<ActionResult<Blurb>> Add(Blurb blurb)
    {
      return await _repository.AddBlurbToDbAsync(blurb);
    }

    [Produces("application/json")]
    [HttpPut("edit/score/{newScore}")]
    public async Task<ActionResult<Blurb>> EditScore(Blurb blurb, double newScore)
    {
      return await _repository.EditBlurbScoreAsync(blurb.BlurbId, newScore);
    }

    //[Produces("application/json")]
    //[HttpPut("edit/privacy")]
    //public async Task<ActionResult<Blurb>> EditPrivacy(Blurb blurb, int privacy)
    //{
    //  return await _repository.EditBlurbPrivacyAsync(blurb.BlurbId, privacy);
    //}

    [Produces("application/json")]
    [HttpPut("edit/message")]
    public async Task<ActionResult<Blurb>> EditMessage(Blurb blurb)
    {
      return await _repository.EditBlurbMessageAsync(blurb.BlurbId, blurb.Message);
    }

    [Produces("application/json")]
    [HttpDelete("remove/{blurbId}")]
    public bool Delete(int blurbId)
    {
      return _repository.DeleteBlurb(blurbId);
    }

  }
}
