using Microsoft.AspNetCore.Mvc;
using TMS.Dapper.BLL.Services.Abstract;
using TMS.Dapper.Common.DTOs.Errors;
using TMS.Dapper.Common.DTOs.Projects.CRUD;
using TMS.Dapper.Common.DTOs.Projects.Custom;
using TMS.Dapper.Common.DTOs.Users.CRUD;
using TMS.Dapper.DAL.Entities;

namespace TMS.Dapper.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProjectReadDTO>))]
        public async Task<ActionResult<IEnumerable<ProjectReadDTO>>> GetAll()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectReadDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResultDTO))]
        public async Task<ProjectReadDTO> GetById([FromRoute] int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            return project;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProjectReadDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResultDTO))]
        public async Task<ActionResult<UserReadDto>> Create([FromBody] ProjectCreateDTO project)
        {
            var created = await _projectService.CreateProjectAsync(project);
            return CreatedAtAction(nameof(Create), created);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectReadDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResultDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResultDTO))]
        public async Task<ActionResult<ProjectReadDTO>> Update([FromRoute] int id, [FromBody] ProjectUpdateDTO project)
        {
            var updated = await _projectService.UpdateProjectAsync(id, project);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _projectService.DeleteProjectAsync(id);
            return NoContent();
        }

        [HttpGet("{id}/members")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectWithMembersDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResultDTO))]
        public async Task<ActionResult<ProjectWithMembersDTO>>  GetProjectWithMembersById([FromRoute] int id)
        {
            var project = await _projectService.GetProjectWithMembersAsync(id);
            return Ok(project);
        }
    }
}
