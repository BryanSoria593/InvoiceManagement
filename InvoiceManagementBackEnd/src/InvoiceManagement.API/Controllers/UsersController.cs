using Microsoft.AspNetCore.Mvc;
using InvoiceManagement.Application.Users;
using InvoiceManagement.Application.Users.Dtos;
using InvoiceManagement.Application.Common.Dtos;

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

        [HttpGet("paged")]
        public ActionResult<PagedResultDto<UserDto>> GetPaged(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? filter = null)
        {
            var result = _userAppService.GetUsers(pageNumber, pageSize, filter);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<UserDto> GetById(int id)
        {
            var user = _userAppService.GetUserById(id);
            if (user == null)
                return NotFound();
            return Ok(user);
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
            try
            {
                var user = _userAppService.RegisterUser(dto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("update")]
        public ActionResult<UserDto> Update([FromBody] UpdateUserDto dto)
        {
            try
            {
                var user = _userAppService.UpdateUser(dto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

    }
}
