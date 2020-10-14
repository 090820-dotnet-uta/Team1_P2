using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Team1P2.Models.Models;
using Team1P2.Repo.Repository;

namespace Team1P2.Controllers
{
    [Route("api/note")]
    [ApiController]
    public class NoteController : Controller
    {
        private readonly Repository _repository;

        public NoteController(Repository repository)
        {
            _repository = repository;
        }

        //[Produces("application/json")]
        //[HttpGet("find")]
        //public async Task<ActionResult<Note>> Find()
        //{
        //    return await _repository.GetNoteAsync(1);
        //}

        [Produces("application/json")]
        [HttpGet("findall")]
        public async Task<ActionResult<List<Note>>> FindAllByBlurbId(int blurbId)
        {
            return await _repository.GetNotesByBlurbIdAsync(blurbId);
        }
    }
}
