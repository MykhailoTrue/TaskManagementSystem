using AutoMapper;
using TMS.Dapper.BLL.Services.Abstract;
using TMS.Dapper.Common.DTOs.Workspaces.CRUD;
using TMS.Dapper.Common.DTOs.Workspaces.Custom;
using TMS.Dapper.Common.Exceptions;
using TMS.Dapper.DAL.Entities;
using TMS.Dapper.DAL.Repositories.Interfaces;

namespace TMS.Dapper.BLL.Services
{
    public class WorkspaceService : BaseService, IWorkspaceService
    {
        public WorkspaceService(IUnitOfWork unitOfWork, IMapper mapper) 
            : base(unitOfWork, mapper)
        {
        }

        public async Task<IEnumerable<WorkspaceReadDTO>> GetAllWorkspacesAsync()
        {
            var workspaces = await _unitOfWork.WorkspaceRepository.GetAllAsync();
            _unitOfWork.Commit();
            return _mapper.Map<IEnumerable<WorkspaceReadDTO>>(workspaces);
        }

        public async Task<WorkspaceReadDTO> GetWorkspaceByIdAsync(int id)
        {
            var workspace = await GetByIdElseThrowException(id);
            _unitOfWork.Commit();

            return _mapper.Map<WorkspaceReadDTO>(workspace);
        }

        public async Task<WorkspaceWithProjectsDTO> GetWorkspaceWithProjectsAsync(int workspaceId)
        {
            var workspace = await _unitOfWork.WorkspaceRepository.GetWorkspaceWithProjectsAsync(workspaceId);
            _unitOfWork.Commit();

            if (workspace is null)
            {
                throw new NotFoundException($"Workspace with Id: {workspaceId} could not be found");
            }

            return _mapper.Map<WorkspaceWithProjectsDTO>(workspace);
        }

        public async Task<WorkspaceReadDTO> CreateWorkspaceAsync(WorkspaceCreateDTO workspace)
        {
            await CheckConstraints(workspace.AuthorId);

            var mapped = _mapper.Map<Workspace>(workspace);

            var createdId = await _unitOfWork.WorkspaceRepository.CreateAsync(mapped);
            var created = await _unitOfWork.WorkspaceRepository.GetByIdAsync(createdId);
            _unitOfWork.Commit();

            return _mapper.Map<WorkspaceReadDTO>(created);
        }

        public async Task<WorkspaceReadDTO> UpdateWorkspaceAsync(int id, WorkspaceUpdateDTO workspace)
        {
            await GetByIdElseThrowException(id);
            await CheckConstraints(workspace.AuthorId);

            var mapped = _mapper.Map<Workspace>(workspace);
            mapped.Id = id;

            await _unitOfWork.WorkspaceRepository.UpdateAsync(mapped);
            _unitOfWork.Commit();

            var updated = _unitOfWork.WorkspaceRepository.GetByIdAsync(id);
            return _mapper.Map<WorkspaceReadDTO>(updated);
        }

        public async Task DeleteWorkspaceAsync(int id)
        {
            var deletedCount = await _unitOfWork.WorkspaceRepository.DeleteAsync(id);
            _unitOfWork.Commit();

            if (deletedCount < 1)
            {
                throw new NotFoundException($"Workspace with Id: {id} could not be found");
            }
        }

        private async Task CheckConstraints(int AuthorId)
        {
            var author = await _unitOfWork.UserRepository.GetByIdAsync(AuthorId);
            if (author is null)
            {
                throw new NotFoundException($"Author with Id: {AuthorId} could not be found");
            }
        }

        private async Task<Workspace> GetByIdElseThrowException(int id)
        {
            var workspace = await _unitOfWork.WorkspaceRepository.GetByIdAsync(id);
            if (workspace is null)
            {
                throw new NotFoundException($"Workspace with Id: {id} could not be found");
            }

            return workspace;
        }
    }
}
