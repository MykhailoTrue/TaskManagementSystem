using Microsoft.AspNetCore.Mvc;
using TMS.Dapper.BLL.Services.Abstract;
using TMS.Dapper.Common.DTOs.Errors;
using TMS.Dapper.Common.DTOs.Workspaces.CRUD;
using TMS.Dapper.Common.DTOs.Workspaces.Custom;

namespace TMS.Dapper.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class WorkspacesController : ControllerBase
    {
        private readonly IWorkspaceService _workspaceService;

        public WorkspacesController(IWorkspaceService workspaceService)
        {
            _workspaceService = workspaceService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<WorkspaceReadDTO>))]
        public async Task<ActionResult<IEnumerable<WorkspaceReadDTO>>> GetAll()
        {
            var workspaces = await _workspaceService.GetAllWorkspacesAsync();
            return Ok(workspaces);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WorkspaceReadDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResultDTO))]
        public async Task<ActionResult<WorkspaceReadDTO>> GetById([FromRoute] int id)
        {
            var workspace = await _workspaceService.GetWorkspaceByIdAsync(id);
            return Ok(workspace);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(WorkspaceReadDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResultDTO))]
        public async Task<ActionResult<WorkspaceReadDTO>> Create([FromBody] WorkspaceCreateDTO workspace)
        {
            var created = await _workspaceService.CreateWorkspaceAsync(workspace);
            return CreatedAtAction(nameof(Create), created);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WorkspaceReadDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResultDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResultDTO))]
        public async Task<ActionResult<WorkspaceReadDTO>> Update([FromRoute] int id, [FromBody] WorkspaceUpdateDTO workspace)
        {
            var updated = await _workspaceService.UpdateWorkspaceAsync(id, workspace);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResultDTO))]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            await _workspaceService.DeleteWorkspaceAsync(id);
            return NoContent();
        }

        [HttpGet("{id}/projects")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WorkspaceWithProjectsDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResultDTO))]
        public async Task<ActionResult<WorkspaceWithProjectsDTO>> GetWorkspaceWithProjects([FromRoute] int id)
        {
            var workspace = await _workspaceService.GetWorkspaceWithProjectsAsync(id);
            return Ok(workspace);
        }
    }
}
