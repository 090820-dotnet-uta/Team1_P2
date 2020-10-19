using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1P2.Models.Models;
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
    [HttpPost("find/all/user/{userId}")]
    public async Task<ActionResult<List<Blurb>>> FindAllByUser(int userId, FullQueryObj obj)
    {
            var blurbs = await _repository.FullQuery(userId, obj.Settings, obj.SinceId, obj.Span);
      return await _repository.GetBlurbsByUserIdAsync(blurbs.AsQueryable<Blurb>(), userId);
    }

    [Produces("application/json")]
    [HttpPost("add")]
    public async Task<ActionResult<Blurb>> Add(Blurb blurb)
    {
      return await _repository.AddBlurbToDbAsync(blurb);
    }

    [Produces("application/json")]
    [HttpPut("edit")]
    public async Task<ActionResult<Blurb>> Edit(Blurb blurb)
    {
      return await _repository.UpdateBlurb(blurb);
    }

    [Produces("application/json")]
    [HttpPut("edit/score/{newScore}")]
    public async Task<ActionResult<Blurb>> EditScore(Blurb blurb, double newScore)
    {
      return await _repository.EditBlurbScoreAsync(blurb.BlurbId, newScore);
    }

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


        [Produces("application/json")]
        [HttpPost("fullquery/{userid}")]
        public async Task<ActionResult<List<Blurb>>> FullQuery(int userid, FullQueryObj obj)
        {
            return await _repository.FullQuery(userid, obj.Settings, obj.SinceId, obj.Span);
        }

    }
}
