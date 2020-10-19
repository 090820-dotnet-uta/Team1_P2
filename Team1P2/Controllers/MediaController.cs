using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Team1P2.Models.Models;
using Team1P2.Repo.Repository;

namespace Team1P2.Controllers
{
    [Route("api/media")]
    [ApiController]
    public class MediaController : Controller
    {
        private readonly Repository _repository;

        public MediaController(Repository repository)
        {
            _repository = repository;
        }

        [Produces("application/json")]
        [HttpGet("find/{mediaId}")]
        public async Task<ActionResult<Media>> Find(int mediaId)
        {
            return await _repository.GetMediaAsync(mediaId);
        }

        [Produces("application/json")]
        [HttpGet("find/all")]
        public async Task<ActionResult<List<Media>>> FindAll()
        {
            return await _repository.GetAllMediaAsync();
        }

        [Produces("application/json")]
        [HttpGet("add")]
        public async Task<ActionResult<Media>> Add(Media media)
        {
            return await _repository.AddMediaToDbAsync(media);
        }
    }
}
