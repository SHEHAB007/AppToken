using AppToken.Models;
using AppToken.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppToken.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserServices userServices;
        public UsersController(IUserServices userServices)
        {
            this.userServices = userServices;
        }
        [HttpPost("auth")]
        public IActionResult Authenticate([FromBody] User userModel)
        {
            var user = userServices.Authenticate(userModel.UserName, userModel.Password);
            if(user == null)
            {
                return BadRequest(new { Message = "Username or Pasword Invalid" });
            }
            return Ok(user);
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = userServices.GetAllUsers();
            return Ok(users);
        }
    }
}
