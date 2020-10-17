using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Team1P2.Models.Models;
using Team1P2.Repo.Repository;

namespace Team1P2.Controllers
{
    [Route("api/tag")]
    [ApiController]
    public class TagController : Controller
    {
        private readonly Repository _repository;

        public TagController(Repository repository)
        {
            _repository = repository;
        }

        [Produces("application/json")]
        [HttpGet("find/{tagId}")]
        public async Task<ActionResult<Tag>> Find(int tagId)
        {
            return await _repository.GetTagAsync(tagId);
        }

        [Produces("application/json")]
        [HttpGet("find/all")]
        public async Task<ActionResult<List<Tag>>> FindAll()
        {
            return await _repository.GetAllTagsAsync();
        }
    }
}
