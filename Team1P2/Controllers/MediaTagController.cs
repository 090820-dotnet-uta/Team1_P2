using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Team1P2.Models.Models;
using Team1P2.Repo.Repository;

namespace Team1P2.Controllers
{
    [Route("api/mediatag")]
    [ApiController]
    public class MediaTagController : Controller
    {
        private readonly Repository _repository;

        public MediaTagController(Repository repository)
        {
            _repository = repository;
        }

        [Produces("application/json")]
        [HttpGet("find")]
        public async Task<ActionResult<MediaTag>> Find()
        {
            return await _repository.GetMediaTagAsync(1);
        }

        [Produces("application/json")]
        [HttpGet("findall")]
        public async Task<ActionResult<List<MediaTag>>> FindAll()
        {
            return await _repository.GetAllMediaTagsAsync();
        }
    }
}
