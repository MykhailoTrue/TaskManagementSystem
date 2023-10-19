using Microsoft.AspNetCore.Mvc;
using TMS.Dapper.BLL.Services.Abstract;
using TMS.Dapper.Common.DTOs.Errors;
using TMS.Dapper.Common.DTOs.ProjectCategories.CRUD;
using TMS.Dapper.DAL.Entities;

namespace TMS.Dapper.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectCategoriesController : ControllerBase
    {
        private readonly IProjectCategoryService _projectCategoryService;

        public ProjectCategoriesController(IProjectCategoryService projectCategoryService)
        {
            _projectCategoryService = projectCategoryService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProjectCategoryReadDTO>))]
        public async Task<ActionResult<IEnumerable<ProjectCategoryReadDTO>>> GetAll()
        {
            var projectCategories = await _projectCategoryService.GetAllProjectCategoriesAsync();
            return Ok(projectCategories);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectCategoryReadDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResultDTO))]
        public async Task<ActionResult<ProjectCategory>> GetById([FromRoute] int id)
        {
            var projectCategory = await _projectCategoryService.GetProjectCategoryByIdAsync(id);
            return Ok(projectCategory);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProjectCategoryReadDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResultDTO))]
        public async Task<ActionResult<ProjectCategoryReadDTO>> Create([FromBody] ProjectCategoryCreateDTO projectCategory)
        {
            var created = await _projectCategoryService.CreateProjectCategoryAsync(projectCategory);

            return CreatedAtAction(nameof(Create), created);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectCategoryReadDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResultDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResultDTO))]
        public async Task<ActionResult<ProjectCategoryReadDTO>> Update([FromRoute] int id, [FromBody] ProjectCategoryUpdateDTO projectCategory)
        {
            var updated = await _projectCategoryService.UpdateProjectCategoryAsync(id, projectCategory);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResultDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResultDTO))]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _projectCategoryService.DeleteProjectCategoryAsync(id);
            return NoContent();
        }
    }
}
