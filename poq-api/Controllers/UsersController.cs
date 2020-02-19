using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using poq_api.Business;

namespace poq_api.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IAppLogger<UsersController> _logger;
        public UsersController(IUserService userService, IAppLogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        // POST api/users/authenticate
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]AuthenticateModel model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);
            _logger.LogInformation($"Call user authenticate....");
            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        // GET api/users/getall
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}
