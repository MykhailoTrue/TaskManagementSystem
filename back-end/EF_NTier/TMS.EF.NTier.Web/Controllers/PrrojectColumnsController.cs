using Microsoft.AspNetCore.Mvc;
using TMS.EF.NTier.BLL.Services.Interfaces;
using TMS.EF.NTier.Common.DTO.Errors;
using TMS.EF.NTier.Common.DTO.ProjectColumns;

namespace TMS.EF.NTier.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrrojectColumnsController : ControllerBase
    {
        private readonly IProjectColumnService _projectColumnService;

        public PrrojectColumnsController(IProjectColumnService projectColumnService)
        {
            _projectColumnService = projectColumnService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectColumnReadDTO))]
        public async Task<ActionResult<ProjectColumnReadDTO>> GetAll()
        {
            var projectColumns = await _projectColumnService.GetAllProjectColumnsAsync();

            return Ok(projectColumns);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectColumnReadDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResultDTO))]
        public async Task<ActionResult<ProjectColumnReadDTO>> GetById([FromRoute] int id)
        {
            var projectColumn = await _projectColumnService.GetProjectColumnByIdAsync(id);
            return Ok(projectColumn);
        }

        [HttpGet("{id}/issues")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectColumnReadDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResultDTO))]
        public async Task<ActionResult<ProjectColumnReadDTO>> GetByIdWithIssues([FromRoute] int id)
        {
            var projectColumn = await _projectColumnService.GetProjectColumnWithDetailsAsync(id);
            return Ok(projectColumn);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProjectColumnReadDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResultDTO))]
        public async Task<ActionResult<ProjectColumnReadDTO>> Create([FromBody] ProjectColumnCreateDTO projectColumn)
        {
            var created = await _projectColumnService.CreateProjectColumnAsync(projectColumn);

            return CreatedAtAction(nameof(Create), created);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProjectColumnReadDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResultDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResultDTO))]
        public async Task<ActionResult<ProjectColumnReadDTO>> Update([FromRoute] int id, [FromBody] ProjectColumnUpdateDTO projectColumn)
        {
            var updated = await _projectColumnService.UpdateProjectColumnAsync(id, projectColumn);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResultDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResultDTO))]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _projectColumnService.DeleteProjectColumnAsync(id);
            return NoContent();
        }

    }
}
