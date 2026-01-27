using Microsoft.AspNetCore.Mvc;
using InvoiceManagement.Application.Users;
using InvoiceManagement.Application.Users.Dtos;

namespace InvoiceManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserAppService _userAppService;

        public UsersController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        [HttpGet]
        public ActionResult<List<UserDto>> Get()
        {
            var users = _userAppService.GetAllUsers();
            return Ok(users);
        }

        [HttpPost("register")]
        public ActionResult<UserDto> Register([FromBody] RegisterUserDto dto)
        {
            var user = _userAppService.RegisterUser(dto);
            return Ok(user);
        }

        [HttpPut("update")]
        public ActionResult<UserDto> Update([FromBody] UpdateUserDto dto)
        {
            var user = _userAppService.UpdateUser(dto);
            return Ok(user);
        }
    }
}
