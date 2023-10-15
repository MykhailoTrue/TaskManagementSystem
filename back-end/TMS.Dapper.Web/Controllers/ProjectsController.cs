/*using Microsoft.AspNetCore.Mvc;
using TMS.Dapper.DAL.Entities;
using TMS.Dapper.DAL.Repositories.Interfaces;

namespace TMS.Dapper.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<Project>> GetAll()
        {
            var projects = await _unitOfWork.ProjectRepository.GetAllAsync();
            _unitOfWork.Commit();
            return projects;
        }

        [HttpGet("{id}")]
        public async Task<Project> GetById([FromRoute] int id)
        {
            var project = await _unitOfWork.ProjectRepository.GetByIdAsync(id);
            _unitOfWork.Commit();
            return project;
        }

        [HttpPost]
        public async Task Create([FromBody] Project project)
        {
            await _unitOfWork.ProjectRepository.CreateAsync(project);
            _unitOfWork.Commit();
        }

        [HttpPut]
        public async Task Update([FromBody] Project project)
        {
            await _unitOfWork.ProjectRepository.UpdateAsync(project);
            _unitOfWork.Commit();
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] int id)
        {
            await _unitOfWork.ProjectRepository.DeleteAsync(id);
            _unitOfWork.Commit();
        }
    }
}
*/