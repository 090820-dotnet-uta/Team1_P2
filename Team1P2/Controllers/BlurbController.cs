using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        [HttpGet("find")]
        public async Task<ActionResult<Blurb>> Find()
        {
            return await _repository.GetBlurbAsync(1);
        }

        [Produces("application/json")]
        [HttpGet("findall")]
        public async Task<ActionResult<List<Blurb>>> FindAll()
        {
            return await _repository.GetAllBlurbsAsync();
        }
    }
}
