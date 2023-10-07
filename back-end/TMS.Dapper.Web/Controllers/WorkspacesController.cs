using Microsoft.AspNetCore.Mvc;
using TMS.Dapper.DAL.Entities;
using TMS.Dapper.DAL.Repositories;
using TMS.Dapper.DAL.Repositories.Interfaces;

namespace TMS.Dapper.Web.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class WorkspacesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public WorkspacesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IEnumerable<Workspace>> GetAll()
        {
            var workspaces = await _unitOfWork.WorkspaceRepository.GetAllAsync();
            _unitOfWork.Commit();
            return workspaces;
        }

        [HttpGet("{id}")]
        public async Task<Workspace> GetById([FromRoute] int id)
        {
            var workspace = await _unitOfWork.WorkspaceRepository.GetByIdAsync(id);
            _unitOfWork.Commit();
            return workspace;
        }

        [HttpPost]
        public async Task Create([FromBody] Workspace workspace)
        {
            await _unitOfWork.WorkspaceRepository.CreateAsync(workspace);
            _unitOfWork.Commit();
        }

        [HttpPut]
        public async Task Update([FromBody] Workspace workspace)
        {
            await _unitOfWork.WorkspaceRepository.UpdateAsync(workspace);
            _unitOfWork.Commit();
        }

        [HttpDelete("{id}")]
        public async Task Delete([FromRoute] int id)
        {
            await _unitOfWork.WorkspaceRepository.DeleteAsync(id);
            _unitOfWork.Commit();
        }
    }
}
