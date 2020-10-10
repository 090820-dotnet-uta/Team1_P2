using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Team1P2.Models.Models;
using Team1P2.Repo.Repositories.IRepositories;

namespace Team1P2.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Produces("application/json")]
        [HttpGet("find")]
        public async Task<ActionResult<User>> Find()
        {
            return await _unitOfWork.User.GetAsync(1);
        }

        [Produces("application/json")]
        [HttpGet("findall")]
        public async Task<ActionResult<List<User>>> FindAll()
        {
            return await _unitOfWork.User.GetAllAsync();
        }
    }
}
