using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using poq_api.Business;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService UserService;

        public UsersController(IUserService userService)
        {
            UserService = userService;
        }

        // POST api/users/authenticate
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            var user = UserService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        // GET api/users/getall
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = UserService.GetAll();
            return Ok(users);
        }
    }
}
