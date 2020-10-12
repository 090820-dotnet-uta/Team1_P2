using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Team1P2.Models.Models;
using Team1P2.Repo.Repositories.IRepositories;

namespace Team1P2.Controllers
{
    [Route("api/mediatag")]
    [ApiController]
    public class MediaTagController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MediaTagController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Produces("application/json")]
        [HttpGet("find")]
        public async Task<ActionResult<MediaTag>> Find()
        {
            return await _unitOfWork.MediaTag.GetAsync(1);
        }

        [Produces("application/json")]
        [HttpGet("findall")]
        public async Task<ActionResult<List<MediaTag>>> FindAll()
        {
            return await _unitOfWork.MediaTag.GetAllAsync();
        }
    }
}
