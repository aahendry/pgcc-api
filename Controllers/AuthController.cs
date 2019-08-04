using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PgccApi.Models;
using PgccApi.Services;
using Microsoft.AspNetCore.Cors;

namespace PgccApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost("login")]
        public IActionResult Login([FromBody]LoginModel userParam)
        {
            var x = ModelState;
            var user = _userService.Authenticate(userParam.Username, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }
    }
}