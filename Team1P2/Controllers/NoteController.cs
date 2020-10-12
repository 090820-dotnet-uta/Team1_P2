using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Team1P2.Models.Models;
using Team1P2.Repo.Repositories.IRepositories;

namespace Team1P2.Controllers
{
    [Route("api/note")]
    [ApiController]
    public class NoteController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public NoteController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Produces("application/json")]
        [HttpGet("find")]
        public async Task<ActionResult<Note>> Find()
        {
            return await _unitOfWork.Note.GetAsync(1);
        }

        [Produces("application/json")]
        [HttpGet("findall")]
        public async Task<ActionResult<List<Note>>> FindAll()
        {
            return await _unitOfWork.Note.GetAllAsync();
        }
    }
}
