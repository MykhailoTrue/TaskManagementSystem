/*using Microsoft.AspNetCore.Mvc;
using TMS.Dapper.DAL.Entities;
using TMS.Dapper.DAL.Repositories;
using TMS.Dapper.DAL.Repositories.Interfaces;

namespace TMS.Dapper.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectCategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectCategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<ProjectCategory>> GetAll()
        {
            var projectCategories = await _unitOfWork.ProjectCategoryRepository.GetAllAsync();
            _unitOfWork.Commit();
            return projectCategories;
        }

        [HttpGet("{id}")]
        public async Task<ProjectCategory> GetById([FromRoute] int id)
        {
            var projectCategory = await _unitOfWork.ProjectCategoryRepository.GetByIdAsync(id);
            _unitOfWork.Commit();
            return projectCategory;
        }

        [HttpPost]
        public async Task Create([FromBody] ProjectCategory projectCategory)
        {
            await _unitOfWork.ProjectCategoryRepository.CreateAsync(projectCategory);
            _unitOfWork.Commit();
        }

        [HttpPut]
        public async Task Update([FromBody] ProjectCategory projectCategory)
        {
            await _unitOfWork.ProjectCategoryRepository.UpdateAsync(projectCategory);
            _unitOfWork.Commit();
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] int id)
        {
            await _unitOfWork.ProjectCategoryRepository.DeleteAsync(id);
            _unitOfWork.Commit();
        }
    }
}
*/