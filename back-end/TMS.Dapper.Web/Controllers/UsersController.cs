using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TMS.Dapper.BLL.Services.Abstract;
using TMS.Dapper.Common.DTOs.Errors;
using TMS.Dapper.Common.DTOs.Users.CRUD;
using TMS.Dapper.Common.DTOs.Users.Custom;
using TMS.Dapper.DAL.Repositories.Interfaces;

namespace TMS.Dapper.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserReadDto>))]
        public async Task<ActionResult<IEnumerable<UserReadDto>>> GetAll() 
        {
            return Ok(await _userService.GetAllUsersAsync());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserReadDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserReadDto>> GetById([FromRoute] int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return user is null ? NotFound() : Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserReadDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResultDTO))]
        public async Task<ActionResult<UserReadDto>> Create([FromBody] UserCreateDto user)
        {
            var created =  await _userService.CreateUserAsync(user);
            return CreatedAtAction(nameof(Create), created);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserReadDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResultDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResultDTO))]
        public async Task<ActionResult<UserReadDto>> Update([FromRoute] int id,[FromBody] UserUpdateDto user)
        {
            var updated = await _userService.UpdateUserAsync(id, user);
            return CreatedAtAction(nameof(Update), updated);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResultDTO))]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }

        [HttpGet("projects")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserWithProjectsDTO))]
        public async Task<ActionResult<IEnumerable<UserWithProjectsDTO>>> GetUsersWithProjects()
        {
            var users = await _userService.GetUsersWithProjectsAsync();
            return Ok(users);
        }
    }
}
