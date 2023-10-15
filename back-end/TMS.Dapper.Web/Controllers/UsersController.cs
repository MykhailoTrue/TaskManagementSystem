using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TMS.Dapper.BLL.Services.Abstract;
using TMS.Dapper.Common.DTOs.Users.CRUD;
using TMS.Dapper.Common.DTOs.Users.Custom;
using TMS.Dapper.DAL.Entities;
using TMS.Dapper.DAL.Repositories.Interfaces;

namespace TMS.Dapper.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserReadDto>> Create([FromBody] UserCreateDto user)
        {
            if (user.BirthDate > DateTime.Now)
            {
                return BadRequest();
            }

            var created =  await _userService.CreateUserAsync(user);
            return CreatedAtAction(nameof(Create), created);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserReadDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<ActionResult<UserReadDto>> Update([FromRoute] int id,[FromBody] UserUpdateDto user)
        {
            try
            {
                var updated = await _userService.UpdateUserAsync(id, user);
                if (updated is null)
                {
                    return NotFound();
                }

                return CreatedAtAction(nameof(Update), updated);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var isDeleted = await _userService.DeleteUserAsync(id);
            if (isDeleted)
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpGet("with-projects")]
        public async Task<IEnumerable<UserWithProjectsDTO>> GetUsersWithProjects()
        {
            var res = await _unitOfWork.UserRepository.GetUsersWithProjectsAsync();
            _unitOfWork.Commit();
            
            var values =  _mapper.Map<IEnumerable<User>, IEnumerable<UserWithProjectsDTO>>(res);
            return values;
        }
    }
}
