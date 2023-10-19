using AutoMapper;
using TMS.Dapper.BLL.Services.Abstract;
using TMS.Dapper.Common.DTOs.ProjectCategories.CRUD;
using TMS.Dapper.Common.Exceptions;
using TMS.Dapper.DAL.Entities;
using TMS.Dapper.DAL.Repositories.Interfaces;

namespace TMS.Dapper.BLL.Services
{
    public class ProjectCategoryService : BaseService, IProjectCategoryService
    {
        public ProjectCategoryService(IUnitOfWork unitOfWork, IMapper mapper) 
            : base(unitOfWork, mapper)
        {
        }

        public async Task<IEnumerable<ProjectCategoryReadDTO>> GetAllProjectCategoriesAsync()
        {
            var projectCategories = await _unitOfWork.ProjectCategoryRepository.GetAllAsync();
            _unitOfWork.Commit();

            var mapped = _mapper.Map<IEnumerable<ProjectCategoryReadDTO>>(projectCategories);
            return mapped;
        }

        public async Task<ProjectCategoryReadDTO> GetProjectCategoryByIdAsync(int id)
        {
            ProjectCategory? projectCategory = await GetByIdElseThrowException(id);
            _unitOfWork.Commit();

            var mapped = _mapper.Map<ProjectCategoryReadDTO>(projectCategory);
            return mapped;
        }

        public async Task<ProjectCategoryReadDTO> CreateProjectCategoryAsync(ProjectCategoryCreateDTO projectCategory)
        {
            await CheckConstraints(projectCategory.WorkspaceId);
            var mapped = _mapper.Map<ProjectCategory>(projectCategory);

            var createdId = await _unitOfWork.ProjectCategoryRepository.CreateAsync(mapped);
            var created = await _unitOfWork.ProjectCategoryRepository.GetByIdAsync(createdId);
            _unitOfWork.Commit();

            return _mapper.Map<ProjectCategoryReadDTO>(created);
        }

        public async Task<ProjectCategoryReadDTO> UpdateProjectCategoryAsync(int id, ProjectCategoryUpdateDTO projectCategory)
        {
            await GetByIdElseThrowException(id);
            await CheckConstraints(projectCategory.WorkspaceId);

            var mapped = _mapper.Map<ProjectCategory>(projectCategory);
            mapped.Id = id;

            await _unitOfWork.ProjectCategoryRepository.UpdateAsync(mapped);
            var updated = await _unitOfWork.ProjectCategoryRepository.GetByIdAsync(id);
            _unitOfWork.Commit();

            return _mapper.Map<ProjectCategoryReadDTO>(updated);
        }

        public async Task DeleteProjectCategoryAsync(int id)
        {
            await GetByIdElseThrowException(id);
            await _unitOfWork.ProjectCategoryRepository.DeleteAsync(id);

            _unitOfWork.Commit();
        }

        private async Task<ProjectCategory?> GetByIdElseThrowException(int id)
        {
            var projectCategory = await _unitOfWork.ProjectCategoryRepository.GetByIdAsync(id);
            if (projectCategory is null)
            {
                throw new NotFoundException($"Project Category with Id: {id} could not be found");
            }

            return projectCategory;
        }

        private async Task CheckConstraints(int workspaceId)
        {
            var workspace = await _unitOfWork.WorkspaceRepository.GetByIdAsync(workspaceId);
            if (workspace is null)
            {
                throw new NotFoundException($"Workspace with Id: {workspaceId} could not be found");
            }
        }
    }
}
