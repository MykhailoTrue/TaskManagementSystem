using Microsoft.AspNetCore.Mvc;
using TMS.Dapper.DAL.Entities;
using TMS.Dapper.DAL.Repositories.Interfaces;

namespace TMS.Dapper.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetAll() 
        {
            var users = await _unitOfWork.UserRepository.GetAllAsync();
            _unitOfWork.Commit();
            return users;
        }

        [HttpGet("{id}")]
        public async Task<User> GetById([FromRoute] int id)
        {
            var user =  await _unitOfWork.UserRepository.GetByIdAsync(id);
            _unitOfWork.Commit();
            return user;
        }

        [HttpPost]
        public async Task Create([FromBody] User user)
        {
            await _unitOfWork.UserRepository.CreateAsync(user);
            _unitOfWork.Commit();
        }

        [HttpPut]
        public async Task Update([FromBody] User user)
        {
            await _unitOfWork.UserRepository.UpdateAsync(user);
            _unitOfWork.Commit();
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute]int id)
        {
            await _unitOfWork.UserRepository.DeleteAsync(id);
            _unitOfWork.Commit();
        }
    }
}
