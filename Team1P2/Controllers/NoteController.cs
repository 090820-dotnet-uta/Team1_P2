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
        //[HttpGet("find/{noteId}")]
        //public async Task<ActionResult<Note>> Find(int noteId)
        //{
        //    return await _repository.GetNoteAsync(noteId);
        //}

        [Produces("application/json")]
        [HttpGet("find/all/{blurbId}")]
        public async Task<ActionResult<List<Note>>> FindAllByBlurbId(int blurbId)
        {
            return await _repository.GetNotesByBlurbIdAsync(blurbId);
        }

        [Produces("application/json")]
        [HttpPost("add/{blurbId}")]
        public async Task<ActionResult<Note>> Add(Note note, int blurbId)
        {
            var n = await _repository.CreateNoteAsync(blurbId, note.NoteBody);
            return await _repository.AddNoteToDbAsync(n);
        }

        [Produces("application/json")]
        [HttpDelete("remove/{noteId}")]
        public async Task<bool> Delete(int noteId)
        {
            return await _repository.DeleteNoteAsync(noteId);
        }
    }
}
