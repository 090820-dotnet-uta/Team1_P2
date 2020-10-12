using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Team1P2.Models.Models;
using Team1P2.Repo.Repositories.IRepositories;

namespace Team1P2.Controllers
{
    [Route("api/tag")]
    [ApiController]
    public class TagController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public TagController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Produces("application/json")]
        [HttpGet("find")]
        public async Task<ActionResult<Tag>> Find()
        {
            return await _unitOfWork.Tag.GetAsync(1);
        }

        [Produces("application/json")]
        [HttpGet("findall")]
        public async Task<ActionResult<List<Tag>>> FindAll()
        {
            return await _unitOfWork.Tag.GetAllAsync();
        }
    }
}
