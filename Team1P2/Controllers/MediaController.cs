using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Team1P2.Models.Models;
using Team1P2.Repo.Repositories.IRepositories;

namespace Team1P2.Controllers
{
    [Route("api/media")]
    [ApiController]
    public class MediaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MediaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Produces("application/json")]
        [HttpGet("find")]
        public async Task<ActionResult<Media>> Find()
        {
            return await _unitOfWork.Media.GetAsync(1);
        }

        [Produces("application/json")]
        [HttpGet("findall")]
        public async Task<ActionResult<List<Media>>> FindAll()
        {
            return await _unitOfWork.Media.GetAllAsync();
        }
    }
}
